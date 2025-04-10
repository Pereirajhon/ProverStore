import { api } from '@/Services/Api'
import { Navbar } from '@/components/Navbar'
import { isAxiosError } from 'axios'
import React, { useLayoutEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'

export  function AppLayout () {
    
    const navigate = useNavigate()
    useLayoutEffect(() => {
      const interceptorId = api.interceptors.response.use(
        (response) => response,
        (error) => {
          if (isAxiosError(error)) {
            const status = error.response?.status
            const code = error.response?.data.code
  
            if (status === 401 && code === 'UNAUTHORIZED') {
              navigate('/auth', {
                replace: true,
              })
            }
            if(status === 403) {
              navigate('/not-found')
            }
          }
  
          return Promise.reject(error)
        },
      )
  
      return () => {
        api.interceptors.response.eject(interceptorId)
      }
    }, [navigate])
    

  return (
    <>
      <Navbar/>
      <Outlet />
    </>
  )
}
