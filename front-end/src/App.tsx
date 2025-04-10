
import {RouterProvider } from 'react-router-dom'
import { AuthContextProvider } from './context/AuthProvider'
import { rotas } from './Rotas'
import { Toaster } from './components/ui/sonner'


function App() {


  return (
    <>
      <AuthContextProvider >
        <RouterProvider router={rotas} />
        <Toaster/>
      </AuthContextProvider>
    </>
    
  )
}

export default App

