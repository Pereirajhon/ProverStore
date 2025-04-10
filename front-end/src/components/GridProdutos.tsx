
import { ProdutoCard } from './ProdutoCard'
import { useProdutoHook } from '../hook/useProdutoHook'

export const GridProdutos = () => {

   const {data, error, isError} = useProdutoHook()

   console.log(data)
    return (
        <>
            <h1 className='text-3xl pb-6 font-bold'>Produtos</h1>    
            <div className=''>  
                <ul className='flex flex-row gap-4 '>
                    {data && 
                        data.map((produto) => (
                            <li key={produto.id}>
                                <ProdutoCard produto={produto} />
                            </li>
                        ))
                    }
                </ul>
                
            </div>
        </>
    )
}
