
import { useEffect } from 'react'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { produtoEditar, produtoGetId } from '@/api/produtoApi'
import { IProduto, ProdutoForm } from '@/Types/ProdutoType'
import { Dialog, DialogContent, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'
import { DialogClose } from '@radix-ui/react-dialog'
import { Input } from '@/components/ui/input'
import { Label } from '@radix-ui/react-label'
import {z} from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import { useForm } from 'react-hook-form'
import { Textarea } from '@/components/ui/textarea' 
import { Button } from '@/components/ui/button'

type UpdateProps = {
  id: string,
  isOpenModalUpdate : boolean,
  setIsOpenModalUpdate: (isOpenModalUpdate: boolean) => boolean
}

export const UpdateProduto = ({isOpenModalUpdate, setIsOpenModalUpdate , id}: UpdateProps) => {

  const queryCliente = useQueryClient()

  const ProdutoSchema = z.object({
    modelo: z.string().nonempty('Campo obrigatório !').max(30).min(3),
    marca: z.string().nonempty('Campo obrigatório !').max(22).min(1),
    categoria: z.string().nonempty('Campo obrigatório !').max(22).min(1),
    descricao: z.string().nonempty('Campo obrigatório !')
      .max(140, 'A quantidade máxima de caracteres é 140 ')
      .min(10, 'A quantidade mínima de caracteres é 10'),
    imagemProduto: z.string().nonempty('Campo obrigatório !')
      .url('Neste campo deve ser inserido uma URL') ,
    ativo: z.boolean(),
    estoque: z.coerce.number()
      .min(6,'Estoque baixo demais')
      .max(50, 'limite máximo de Estoque é 50') ,
    valor: z.coerce.number()
      .min(10, 'Valor mínimo é 10')
      .max(70000,'Valor máximo atingido')
  })

  type ProdutoFormType = z.infer<typeof ProdutoSchema>

  const {data: produtoData} = useQuery<IProduto>({
    queryKey: ['produtos', id],
    queryFn: () => produtoGetId(id),
    enabled: isOpenModalUpdate,
  })

  const {mutateAsync, error} = useMutation({
    mutationKey: ['produtos'],
    mutationFn: (produto: ProdutoFormType) => produtoEditar(produto, id),
    onSuccess: () => {
      queryCliente.invalidateQueries({
        queryKey: ['produtos'],        
      })

      setIsOpenModalUpdate(false)
    }
  })


  const {handleSubmit, register, reset, formState:{errors}} = useForm<ProdutoFormType>({
    resolver: zodResolver(ProdutoSchema),
    defaultValues: {
      ativo: produtoData?.ativo,
      valor: produtoData?.valor,
      categoria: produtoData?.categoria,
      modelo : produtoData?.modelo,
      imagemProduto: produtoData?.imagemProduto,
      descricao: produtoData?.descricao,
      estoque: produtoData?.estoque,
      marca: produtoData?.marca,
    }
  })

  useEffect(() => {
    if (produtoData) {
      reset(produtoData)
    }
  }, [produtoData, reset])

  const onSubmit = handleSubmit(async(data : ProdutoFormType) => {
    mutateAsync(data)
    
    reset
  })

  return (
    <Dialog onOpenChange={setIsOpenModalUpdate} open={isOpenModalUpdate}>      
      <DialogContent className="sm:max-w-[605px]">
        <DialogHeader>
          <DialogTitle>Atualizar Produto</DialogTitle>
          
          {error && (
            <p className='text-700 p-2'>{error.message}</p>
          )}
          
        </DialogHeader>
        
        <div className="grid gap-4 py-6">
        <form onSubmit={onSubmit} className=''>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Marca
            </Label>
            <Input
              {...register('marca')}
              id="marca"
              name='marca'
              className="col-span-3"
            />
            {errors.marca && (
              <p>{errors.marca?.message}</p>
            )}
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Categoria
            </Label>
            <Input
              {...register('categoria')}
              id="Categoria"
              name='categoria'
              className="col-span-3"
            />
            {errors.categoria?.message && (
              <p>{errors.categoria?.message}</p>
            )}
          </div>

          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Modelo
            </Label>
            <Input
              {...register('modelo')}
              id="Modelo"
              name='modelo'
              className="col-span-3"
            />
            {errors.modelo?.message && (
              <p>{errors.modelo?.message}</p>
            )}
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Imagem do Produto
            </Label>
            <Input
              type='url'
              {...register('imagemProduto')}
              id="imagemProduto"
              name='imagemProduto'
              className="col-span-3"
            />
            {errors.imagemProduto?.message && (
              <p>{errors.imagemProduto?.message}</p>
            )}
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Valor
            </Label>
            <Input
              {...register('valor')}
              id="Valor"
              name='valor'
              className="col-span-3"
            />
            {errors.valor?.message && (
              <p className='text-red-700'>{errors.valor?.message}</p>
            )}
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Estoque
            </Label>
            <Input
              {...register('estoque')}
              id="Estoque"
              name='estoque'
              className="col-span-3"
            />
            {errors?.estoque && (
              <p>{errors.estoque?.message}</p>
            )}
          </div>
          
          <div className="grid grid-cols-4 items-center gap-4">
            <Label className="text-right">
              Descrição
            </Label>
            <Textarea
              {...register('descricao')}
              id="Descricao"
              name='descricao'
              className="col-span-3"
            >
            </Textarea>
            {errors.descricao?.message && (
              <p className='p-2 text-red-700'>{errors.descricao?.message}</p>
            )}
          </div>
        </form>
          
        </div>
        <DialogFooter>
          <Button variant="secondary" onClick={() => DialogClose}>
            Cancelar
          </Button>
          <Button onClick={onSubmit} type="submit">Atualizar</Button>
          
        </DialogFooter>
      </DialogContent>
    </Dialog>
  
  )
}
