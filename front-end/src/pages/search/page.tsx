import { IProduto } from '@/Types/ProdutoType'
import { searchProdutos } from '@/api/produtoApi'
import { ProdutoCard } from '@/components/ProdutoCard'
import { useSearch } from '@/hook/useSearch'
import { useQuery } from '@tanstack/react-query'
import React from 'react'

const SearchPage = () => {
  const query = useSearch()
  const search = query.get('q')

  const {data: busca} = useQuery<IProduto[]>({
    queryKey: ['produtos', query],
    queryFn:async() => await searchProdutos(search!)
  })

  console.log(busca)

  return (
    <div>
        <h1>Busca </h1>
        <div className='flex'>
          {busca?.length === 0 ? (
            <h1 className='text-xl font-semibold text-center'>Produto não encontrado. tente outro tema para busca</h1>
          ): (
            <>
              <ul>
                {busca?.map(produto => (
                  <li key={produto.id}>
                    <ProdutoCard produto={produto}/>
                  </li>
                ))}
              </ul>
            </>
          )}
        </div>
    </div>
  )
}

export default SearchPage