import { UseAuthContext } from '@/context/AuthProvider'
import { Navigate, Outlet } from 'react-router-dom'

export const RotasProtegidas = ({role}: {role: string}) => {
    const auth = UseAuthContext().auth
    console.log(auth)
    if(!auth?.token && role.includes('User')){
        return <Navigate to='/auth' />
    }

    if(!auth.usuario?.role?.includes('Admin') && role === 'Admin' ) {
        return <Navigate to='/not-found' replace />
    }

    return(
    <> 
        <Outlet /> 
    </>)
}