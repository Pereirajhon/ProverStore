import { UseAuthContext } from '@/context/AuthProvider'
import { useLogin } from '../../api/apiAuth'
import { useMutation } from '@tanstack/react-query'
import {useNavigate} from 'react-router-dom'
import {useForm} from 'react-hook-form'
import {z} from 'zod'
import {zodResolver} from '@hookform/resolvers/zod'
import { ButtonGoogle } from '../buttonGoogle'
import { Input } from '../ui/input'
import { Button } from '../ui/button'
import { Label } from '../ui/label'

export const FormLogin = () => {
  const navigate = useNavigate()
  const logar = useLogin()
  const authFormSchema = z.object({
    email: z.string({required_error: 'Campo obrigatório!'})
    .email('Digite um email válido !').
    min(6, 'Mínimo de 6 caracteres')
    .max(40,'Limite máximo de 40 caracteres'),
    senha: z.
    string({
      required_error: 'Campo Obrigatório !'
    })
    .min(6, 'Minimo de caracter para o Campo é de 6')
    .max(20, 'Máximo de Caracteres é de 20')

  })

  type AuthFormSchema = z.infer<typeof authFormSchema>
  
    
  const {mutateAsync, isError, error, isPending} = useMutation({
    mutationFn: (data: AuthFormSchema) => logar(data),
    onSuccess:() => navigate('/')
  })

  const {register, handleSubmit, formState: {errors, isSubmitting} } = useForm<AuthFormSchema>({
    resolver: zodResolver(authFormSchema)
  })
  
  const onHandleSubmit = handleSubmit(async(data: AuthFormSchema) => {
    try{
      
      mutateAsync(data)

    }catch(e){
      alert(e)

    }
    
  })


  return (
    <div className='flex flex-col gap-4 p-4 '>
        
      <h1 className='text-3xl font-bold text-primary text-center my-4'>
        Entre em ProverStore
      </h1>
      {isError && (
        <div>
          <p className='w-full h-6 text-center text-sm text-red-800'>
            {error?.message}
          </p>
        </div>

      )}   
                  
      <div className="relative mt-5">
        <div className="absolute inset-0 flex items-center">
          <span className="w-full border-t"></span>
        </div>
        <div className="relative flex justify-center ">
          <span className="bg-background px-2 text-muted-foreground">ou</span>
        </div>
      </div>
      <form onSubmit={onHandleSubmit}
        className='w-full flex flex-col gap-8 mt-4'>
        <div className='flex flex-col gap-4'>
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
        </div>

        <Button disabled={isSubmitting || isPending}
          className='text-lg'
          >
          Entrar
        </Button>
           
      </form>
    </div>
  )
}
