
import { valorEmReais } from '@/Services/moeda'
import { IProduto } from '@/Types/ProdutoType'
import { checkoutCompraApi } from '@/api/checkoutApi'
import { produtoGetId } from '@/api/produtoApi'
import { Button } from '@/components/ui/button'
import { UseAuthContext } from '@/context/AuthProvider'
import { useQuery } from '@tanstack/react-query'
import { HeartIcon, MinusIcon, PlusIcon } from 'lucide-react'
import React, { useCallback, useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { v4 as uuid } from 'uuid'


const ProdutoPage = () => {
  const {auth:{usuario}} = UseAuthContext()
  const {id} = useParams()

  const [quantidade, setQuantidade] = useState(1)

  const {data: produto} = useQuery<IProduto>({
    queryKey: ['produto', id], 
    queryFn: async() => await produtoGetId(id!)
  })

  const handleComprarProduto = () => {

    const carrinhoId = uuid()
    checkoutCompraApi({
      clienteId: usuario?.id as string,
      carrinhoId: carrinhoId, 
      carrinhoItem: [{
        produtoId: String(id),
        quantidade: quantidade,
        totalProduto: quantidade * produto?.valor!
      }]
    })
  }

  const handleIncrementeQuantidade = () => {
    if(quantidade >= produto?.estoque!){
      setQuantidade(prev => prev + 1)
    }
  }

  const handleDecrementeQuantidade = () => {
    if(quantidade > 0){
      setQuantidade(prev => prev -= 1)
    }
  }

  return(
    <div className=' w-full py-12 px-12 flex gap-9 items-stretch h-[586px]'>
      <div className='w-1/2 h-full bg-[#f2f0ea] rounded-xl flex items-center justify-center'> 
        <img className='m-auto object-contain' src={produto?.imagemProduto} />
      </div>
      <div className='w-1/2 max-w-full py-4 h-full flex flex-col' >
        <h2 className='font-bold text-3xl text-gray-950'>{produto?.modelo}</h2>
        <span className='text-gray-500 text-sm'>{produto?.marca}</span>
        <strong className='text-2xl my-8 '>
          {valorEmReais(produto?.valor!)}
        </strong>
        <div className='max-w-full mt-2 mb-4 h-18 text-gray-950'>
          <h2 className='font-semibold text-lg'>Descrição </h2>
          <p className='text-gray-800 text-sm break-words'>
            {produto?.descricao}ppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp -poh ij8iij8iuj iuj989u7h9hj9j iuhi8uh8u jbiuyg8yg
          </p>
        </div>
        <div className='self-start text-left flex items-center gap-4 font-semibold mt-auto'>
          Quantidade :
            <div className='rounded-full flex items-center border w-36 h-14'>
              <span onClick={handleDecrementeQuantidade} className='flex justify-center items-center h-full w-full '>
                {<MinusIcon size='20' color='#292220' />} 
              </span> 
             <input readOnly type="tel" value={quantidade} className='w-12 h-full text-base mx-auto text-center' 
             /> 
              <span onClick={handleIncrementeQuantidade} className='p-2 h-full w-full flex items-center justify-center'>
                {<PlusIcon size='20' color='#292220'/>}
              </span>
            </div>
        </div>
        <div className='mt-auto flex gap-3'>
          <Button 
          
            className='py-6 w-full text-lg bg-black rounded-xl'
            onClick={handleComprarProduto}>
            Comprar
          </Button>
          <button className='rounded-xl py-3 px-4 bg-[#f2f0ea]'>
            <HeartIcon />
          </button>
        </div>
        
      </div>
    </div>
  )
}

export default ProdutoPage