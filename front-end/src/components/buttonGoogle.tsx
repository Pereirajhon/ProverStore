import { api } from '@/Services/Api'
import React from 'react'
import {GoogleLogin, CredentialResponse } from '@react-oauth/google'

export const ButtonGoogle = () => {

  const responseGoogle = async (response:CredentialResponse) => {
  
    if(response && response.credential){
      try{
      const res = await api.post('/conta/google', {
        googleToken: response.credential
      })

      console.log(res?.data)

      }catch(error){
        console.error(error)
      }
      
    }else{
      console.error('erro ao fazer login')
    }
    
  }
  const responseError = (error: any) => {
    console.error(error)
  }


  return (
    <>
      <GoogleLogin 
        onSuccess={responseGoogle}
        onError={() => responseError}

      />
    </>
      
  )
}
