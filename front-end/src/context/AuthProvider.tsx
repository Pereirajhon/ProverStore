
import React,{useContext, createContext, useState, useEffect} from 'react'

type AuthProviderProps = {
    children : React.ReactNode
}

type Usuario = {
    id: string,
    email: string,
    role?: string
}

interface IAuthContext {
    auth: {usuario?: Usuario, token?: string} ,
    setAuth: (auth : IAuthContext['auth']) => void ,
}

export const AuthContext = createContext<IAuthContext>({} as IAuthContext)  ;

export const AuthContextProvider = ({children}: AuthProviderProps) => {
    const token = JSON.parse(window.localStorage.getItem('token')!)
    const initialState = {usuario : token?.usuario, token: token?.token } || {}
    
    const [auth, setAuth] = useState(initialState)
    

  return (
    <AuthContext.Provider value={{ auth, setAuth } as unknown as IAuthContext }>
        {children}
    </AuthContext.Provider>
  )
}

export const UseAuthContext = () => {

    return useContext(AuthContext)
}