
import { IProduto } from '../Types/ProdutoType'
import React, {useEffect, useState } from 'react'
import { useCarrinhoStore } from '../Store/CarrinhoStore'
import { Heart, HeartIcon, ShoppingCartIcon } from 'lucide-react'
import { useFavoritosHook } from '../hook/useFavoritosHook'
import { UseAuthContext } from '@/context/AuthProvider'
import { Link } from 'react-router-dom'
import { valorEmReais } from '@/Services/moeda'

type ProdutoCardProps = {
  produto : IProduto,
 // key : string
}

export const ProdutoCard = ({produto}: ProdutoCardProps) => {

  const [favoritado, setFavoritado] = useState(false)
  const {auth} = UseAuthContext()!
  const {usuario} = auth

  const {addAoCarrinho, isProdutoCarrinho, removerItemDoCarrinho} = useCarrinhoStore((state =>
    state
  ));
  const isItem = isProdutoCarrinho(produto.id)

  const {desFavoritar, favoritar} = useFavoritosHook()

 const handleFavoritar = () => {

  if(favoritado){
    desFavoritar.mutateAsync({clienteId: usuario?.id!, produtoId: produto.id })
  } else{
    favoritar.mutateAsync({clienteId: usuario?.id!, produtoId: produto.id})
  }
}

const handleAddCarrinho = () => {

  if(isItem){
    removerItemDoCarrinho(produto.id)
  } else{
    addAoCarrinho(produto)
  }

}

useEffect(() => {

  const isFavoritado = produto.clienteIdsFavorito.some(cliente => cliente === usuario?.id!)
  setFavoritado(isFavoritado)
 
},[usuario, produto])

  return (
    <>
        <div className='w-64 bg-card flex flex-col rounded-2xl p-2 shadow-md shadow-zinc-400 hover:shadow-lg hover:shadow-zinc-500'>
          <div className='relative w-full h-60 flex justify-center items-center rounded-xl'>
            <div className='absolute top-0 right-1 flex gap-2'>
              <button onClick={handleAddCarrinho}>
                <ShoppingCartIcon className={`${isItem ? 'fill-green-500' : ''} `} /> 
                
              </button>
              <button 
                className='bg-transparent p-1 rounded-full' 
                onClick={handleFavoritar}
              >
                {favoritado ? 
                (<HeartIcon className='fill-black' />) :
                (<HeartIcon className='' />) 
                }
              </button>
            </div>
              
            <Link to={`produto/${produto.id}`}>
              <img 
              className='w-32 h-36 object-container'
              src={produto.imagemProduto} 
              />
            </Link>
          </div>
            
          <div className='flex flex-col py-2 px-1 gap-2 h-32'>
            <span className='text-sm text-zinc-600 -mb-1'>
              {produto.marca}
            </span>

            <Link className='hover:underline' to={`produto/${produto.id}`}>
              <h3 className='text-lg font-semibold text-zinc-950 leading-5 '>
                {produto.modelo}
              </h3>
            </Link>

            <strong className='text-2xl text-black font-semibold mt-auto'>
              {valorEmReais(produto.valor)}
            </strong>

          </div>  
        </div>
    </>
    
       
  )
}
