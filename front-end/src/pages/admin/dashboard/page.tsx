
import { TabelaProdutos } from '../components/tabelaProdutos'
import { AddProduto } from '../components/addProduto'

const DashboardPage = () => {
  

  return (
    <main className='px-10 py-6'>
     <div>
      <AddProduto/>
      </div>
      <TabelaProdutos />
    </main>
  )
}

export default DashboardPage