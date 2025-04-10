import {PlusCircle} from 'lucide-react'
import { produtoAdicionar } from '@/api/produtoApi' 
import { useMutation } from '@tanstack/react-query'
import { useForm, } from 'react-hook-form'
import {string, z} from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import { useQueryClient} from '@tanstack/react-query'
import {v4 as uuid} from 'uuid'
import { Dialog, DialogClose, DialogContent, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'

import { Textarea } from '@/components/ui/textarea'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'

export const AddProduto = () => {
  const queryClient = useQueryClient()
  const id = uuid();
  const ProdutosSchema = z.object({
    id: string(),
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
      .max(70000,'Valor máximo atingido'),
  })

  type ProdutosForm = z.infer<typeof ProdutosSchema>

  const {mutateAsync: addProduto, error} = useMutation({
    mutationKey : ['produtos'],
    mutationFn: (data: ProdutosForm) => produtoAdicionar(data),

    onSuccess(_, variables){
      queryClient.getQueryData(['produtos'])

      queryClient.setQueryData(['produtos'], (data: any) => {

        return [...data,{
          categoria: variables.categoria,
          modelo: variables.modelo,
          valor: variables.valor,
          ativo: variables.ativo,
          estoque: variables.estoque,
          descricao: variables.descricao,
          imagemProduto: variables.imagemProduto,
          marca: variables.marca,
          id: variables.id
        }]
      })
    },
  })

  const {handleSubmit, register, formState:{errors}} = useForm<ProdutosForm>({
    resolver: zodResolver(ProdutosSchema),
    defaultValues: {
      id: id,

    }
  })

  const onSubmit = handleSubmit(async(data : ProdutosForm) => {
  
    addProduto({...data, id: id})

    DialogClose
    alert('Produto adicionado com sucesso !')
  })

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button className='gap-2'> 
          <PlusCircle/> Novo Produto
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[588px]">
        <DialogHeader>
          <DialogTitle >Adicionar Produto</DialogTitle>
          
          {error && (
            <p className='text-700 p-2'>{error.message}</p>
          )}
          
        </DialogHeader>
        
        <div className="grid gap-4 py-6">
        <form onSubmit={onSubmit} className='space-x-4'>
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
          <Button onClick={onSubmit} type="submit">Adicionar</Button>
          
        </DialogFooter>
      </DialogContent>
    </Dialog>
  
  )
}
