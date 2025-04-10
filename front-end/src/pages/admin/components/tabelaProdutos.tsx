import { useProdutoHook } from '@/hook/useProdutoHook' 
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator } from '@/components/ui/dropdown-menu'
import { Table, 
  TableBody, 
  TableCell, 
  TableHead, 
  TableHeader, 
  TableRow 
} from '@/components/ui/table'
import { Ellipsis } from 'lucide-react'
import { DropdownMenuComponent } from './dropdownMenuComponent'

export const TabelaProdutos = () => {
 const {data, error, isError} = useProdutoHook()

  console.log(data)
  console.log(error)

  return (
    <Table>
      <TableHeader>
          <TableRow>
            <TableHead>Imagem</TableHead>
            <TableHead>Categoria</TableHead>
            <TableHead>Modelo</TableHead>
            <TableHead>Valor</TableHead>
            <TableHead>Quant. em Estoque</TableHead>
            <TableHead>Ações</TableHead>
          </TableRow>
      </TableHeader>
      <TableBody>
        { data && data.map((produto) => (
          <TableRow key={produto.id}>
            <TableCell>
              <img className='w-[136px] max-w-full object-cover' src={produto.imagemProduto} alt=''/>
            </TableCell>
            <TableCell>{produto.categoria}</TableCell>
            <TableCell>{produto.modelo}</TableCell>
            <TableCell>{produto.valor}</TableCell>
            <TableCell>{produto.estoque}</TableCell>
             
            <TableCell>
              <DropdownMenuComponent produtoId={produto.id}>
                <Ellipsis />
              </DropdownMenuComponent>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}

