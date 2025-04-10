import React from 'react'
import { Link } from 'react-router-dom'

const NotFound = () => {
  return (
    <div className='w-full h-full flex items-center justify-center text-center'>
        <h1 className='text-3xl'>Error 404 </h1>
        <Link className='text-lg' to='/'>Voltar para o ínicio</Link>
    </div>
  )
}

export default NotFound