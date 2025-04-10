import {Plus, Minus, TrashIcon, ShoppingCartIcon} from 'lucide-react'
import { useCarrinhoStore } from '@/Store/CarrinhoStore' 
import { TableCell, TableHead, TableHeader, TableRow, Table, TableBody } from '@/components/ui/table'
import { valorEmReais } from '@/Services/moeda'
import { Link } from 'react-router-dom'
import { Button } from '@/components/ui/button'

const CarrinhoPage = () => {

  const {carrinho} = useCarrinhoStore((state) => state)
  
  const {
    incrementarQuantidade, 
    decrementarQuantidade,
    calcularTotalCarrinho, 
    removerItemDoCarrinho,
    esvaziarCarrinho
  } = useCarrinhoStore((state) => state)

  console.log(carrinho)

  return (
    <main className='px-12 w-full'>
      <h1 className='text-2xl py-8 font-bold '> Carrinho de compras </h1>
      {carrinho.carrinhoItems.length === 0 ? (
        <div className='flex flex-col justify-center items-center gap-4 h-full mt-14 '>
          <h1 className='text-3xl text-neutral-800 font-bold'>
            O seu carrinho está vazio!
          </h1>
          <p className='text-gray-600'>
            Você ainda não possui itens no seu carrinho.
          </p>
          <Link to='/'>
            <Button className='text-lg p-6 rounded-xl flex gap-2'>
              <ShoppingCartIcon/> Continuar comprando
            </Button>
          </Link>
         
        </div>
        
      ):(
        <>
        
        <Table className=''>
            <TableHeader>
              <TableRow>
                <TableHead> Imagem </TableHead>
                <TableHead>Produto</TableHead>
                <TableHead>Quantidade</TableHead>
                <TableHead>Valor</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
                {carrinho && carrinho.carrinhoItems.map(item => (
                  <TableRow key={item.item.id} className='font-medium text-lg'>
                      <TableCell>
                        <img className='w-[136px]' 
                        src={item.item.imagemProduto} 
                        /> 
                        </TableCell>
                      <TableCell>{item.item.modelo}</TableCell>
                      <TableCell className=''>
                        <div className='w-full h-full flex flex-col gap-4 '>
                          <div className='flex items-center text-lg gap-2'>
                            <button
                              className='p-1 border'
                              onClick={() => decrementarQuantidade(item.item)}
                              >
                                {<Minus size={16} />}
                              </button>
                                {item.quantidade}
                              <button
                              className='p-1 border'
                              onClick={() => incrementarQuantidade(item.item)}
                              >
                                {<Plus size={16} />}
                            </button>
                          </div>
                          <button className='flex' onClick={() => removerItemDoCarrinho(item.item.id)}>
                            <span className='text-red-800 text-sm flex font-bold items-center gap-1'> 
                              <TrashIcon className='fill-red-800' size='15'/>  Remover
                            </span>
                          </button>
                        </div>

                      </TableCell>
                      <TableCell className='text-xl font-semibold'>
                        {valorEmReais(item.valor)}
                      </TableCell>
                  </TableRow>
                ))}

            </TableBody>
        </Table>
        </>
      )}
        
    </main>
  )
}

export default CarrinhoPage 