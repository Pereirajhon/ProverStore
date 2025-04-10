import { cadastrar } from '../../api/apiAuth'
import { useMutation } from '@tanstack/react-query'
import {useNavigate} from 'react-router-dom'
import {useForm} from 'react-hook-form'
import {z} from 'zod'
import {zodResolver} from '@hookform/resolvers/zod'
import { Input } from '../ui/input'
import { Label } from '../ui/label'
import { Button } from '../ui/button'

const FormCadastro = () => {
    const navigate = useNavigate()  
    const cadastroFormSchema = z.object({
        nome: z.string({required_error: 'Campo obrigatório!'})
          .min(2, 'Mínimo de 2 caracteres')
          .max(28,'Limite máximo de 28 caracteres'),
        email: z.string({required_error: 'Campo obrigatório!'})
          .email('Digite um email válido !')
          .min(6, 'Mínimo de 6 caracteres')
          .max(28,'Limite máximo de 28 caracteres'),
        senha: z.string({
          required_error: 'Campo Obrigatório !'
        })
          .min(6, 'Minimo de caracter para o Campo é de 6')
          .max(20, 'Máximo de Caracteres é de 20'),
        confirmeSenha: z.string({
            required_error: 'Campo Obrigatório !'
          })
        }).refine((data) => data.senha === data.confirmeSenha, {
        message: "As senhas não são iguais",
        path: ["confirmeSenha"]
      });
    
      type CadastroFormSchema = z.infer<typeof cadastroFormSchema>
      
      const {mutateAsync, isError, isPending} = useMutation({
        mutationFn: (data: CadastroFormSchema) => cadastrar(data),
        onSuccess: () => {
          navigate('/auth', {replace: true})
        }    
        
      })
    
      const {register, handleSubmit, formState: {errors, isSubmitting} } = useForm<CadastroFormSchema>({
        resolver: zodResolver(cadastroFormSchema)
      })
    
      const onHandleSubmit = handleSubmit(async(data: CadastroFormSchema) => {
      
        mutateAsync(data)

      })
      

  return (
    <div className='flex flex-col p-4 gap-4'>
      <h1 className='text-3xl text-black font-bold text-center my-4'>
        Cadastrar-se em ProverStore
      </h1>
      {isError && (
        <span className='text-center text-sm text-red-800'>
          O email ou senha do usuário pode estar incorreto
        </span>
      )}   
                  
      <Button variant={'outline'} className='w-full'>      
        Continue com Google
      </Button>

      <div className="relative my-5">
        <div className="absolute inset-0 flex items-center">
          <span className="w-full border-t"></span>
        </div>
          <div className="relative flex justify-center">
            <span className="bg-background px-2 text-muted-foreground">ou</span>
          </div>
        </div>

      <form onSubmit={onHandleSubmit}
        className='m-auto flex flex-col gap-8 py-4 w-full'
      >
        <div className='flex flex-col gap-4'>
          <div className='flex flex-col space-y-1.5'>
            <Label className='' htmlFor='Nome'>
              Nome
            </Label>
            <Input
              className=''
              {...register('nome')}
              name='nome'
              type='text'
              placeholder='Nome de usuário'
            />
            {errors?.nome && (
              <span className='text-red-700 text-xs'>
                {errors?.nome?.message}
              </span>
            )}
            
          </div>
        
          <div className='space-y-1'>
            <Label htmlFor='Email'>
              Email
            </Label>
            <Input
              className='shadow-sm'
              {...register('email')}
              name='email'
              type='email'
              placeholder='Email'
            />
            {errors?.email && (
            <p className='text-red-700 text-xs'>
              {errors?.email?.message}
            </p>
            )}
          </div>

          <div className='space-y-1'>
            <Label htmlFor='Senha'>
              Senha
            </Label>
            <Input
              className='shadow-sm'
              {...register('senha')}
              name='senha'
              type='password'
              placeholder='Senha'
              
            />
            {errors?.senha && (
            <p className='text-red-700 text-xs'>
              {errors?.senha?.message}
            </p>
            )}
          
          </div>
      
          <div className='space-y-1'>
            <Label htmlFor=''>Confirme a senha</Label> 
            <Input 
              {...register('confirmeSenha')}
              name='confirmeSenha'
              type='password'
              placeholder='Confirme sua Senha'
              className='shadow-sm'
            />
            {errors?.confirmeSenha && (
              <p className='text-xs text-red-700 '>
                {errors?.confirmeSenha?.message}
              </p>
            )}

          </div> 
        </div>
        
        <Button className='' type='submit' disabled={isSubmitting || isPending}>
          Cadastro
        </Button>
      </form>
    </div>
  )
}

export default FormCadastro