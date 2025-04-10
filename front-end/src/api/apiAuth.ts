import { api } from '@/Services/Api' 
import React, { useEffect } from 'react'
import { UseAuthContext } from '@/context/AuthProvider'

type LoginType = {
    email: string,
    senha: string
}

interface IJwt{
    aud: string,
    exp: number,
    iat : number,
    iss: string,
    nameid?: string,
    nbf: number,
    role?: string
    unique_name?: string
  }

type CadastroType = {
    email: string, 
    senha : string,
    confirmeSenha: string
}


//const {setAuth} = UseAuthContext()
export const useLogin = () => {

   // const {setAuth, auth} = UseAuthContext()

    const logar = async(data: LoginType) => { 
        
        try{
            const response = await api.post('/conta/entrar', {
                Email: data.email, Senha: data.senha
            })
            console.log(response.data.data)

            if(response.data){
                console.log(response.data)
                
                window.localStorage.setItem('token', JSON.stringify(response.data?.data))
            
            } 
            
        }catch (error: any) {
            if (error.response && error.response.data && error.response.data.errors) {
            console.error("Erro ao fazer login:", error.response.data.errors);
            throw error.response.data.errors
            } else {
            console.error("Erro ao fazer login:", error.message);
            throw error.message
            }
        }

    }

    return logar
}

export const cadastrar = async({email, senha, confirmeSenha}:CadastroType ) => {

    const response = await api.post('/conta/cadastro',{
        Email : email, Senha: senha, ConfirmaSenha: confirmeSenha
    })
    console.log(response.data?.data)
    
    if(response.data){
        console.log(response.data)
        window.localStorage.setItem('token', response.data)
    }
    if(response.status !== 200){
        console.log("Ocorreu um erro ao se cadastrar")
        if(response.data?.errors){
            console.log(response.data?.errors)
        }
    }
    
}

export const logoutApi = async() => {
    const response = await api.post('conta/logout')
    return response.data?.mensagem
    
}
