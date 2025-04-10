import { valorEmReais } from '@/Services/moeda'
import { IProduto } from '@/Types/ProdutoType'
import { obterProdutosFavoritados } from '@/api/favoritoApi'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { UseAuthContext } from '@/context/AuthProvider'
import { useQuery } from '@tanstack/react-query'
import React from 'react'
import { useParams } from 'react-router-dom'

const FavoritosPage = () => {
  const {usuario_id} = useParams()
  const i = useParams()
  console.log(i)
  const {data} = useQuery<IProduto[]>({
    queryKey: ['produtos'],
    queryFn: async() => await obterProdutosFavoritados(usuario_id!)
  })
  console.log(data)

  return (
    <main className='px-8 py-4 gap-8 ' >
      <h1 className='font-bold text-neutral-900 py-6 text-3xl'>Favoritos</h1>
      <Table className='bg-background'> {/*
        <TableHeader>
          <TableRow>
            <TableHead>Image</TableHead>
            <TableHead>Modelo</TableHead>
            <TableHead>Valor</TableHead>
          </TableRow>
  </TableHeader> */}
        <TableBody>
            {data && data.map(favoritado => (
            <TableRow className='hover:bg-background' key={favoritado.id}>  
              <TableCell>
                <img 
                className='w-28 h-32' 
                src={favoritado.imagemProduto} 
                alt='imagem do produto' 
                />
                
              </TableCell>
              <TableCell className='flex flex-col items-start'>
                <span className='text-gray-600 text-sm'>{favoritado.marca}</span>
                <h3 className='text-xl text-gray-950 font-semibold'>
                  {favoritado.modelo}
                </h3>
              </TableCell>
              <TableCell>
                <strong className='text-2xl '>
                  {valorEmReais(favoritado.valor)}
                </strong>
              </TableCell>
            </TableRow>  
            ))}
          
        </TableBody>
      </Table>
      

    </main>
  )
}

export default FavoritosPage