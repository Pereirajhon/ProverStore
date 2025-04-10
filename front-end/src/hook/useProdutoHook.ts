import {api} from '@/Services/Api'
import {useQuery} from '@tanstack/react-query'
import { IProduto } from '@/Types/ProdutoType' 

export const useProdutoHook = () => {
  
  const {data, isError, error} = useQuery<IProduto[]>({
    queryKey:['produtos'], 
    queryFn: async() => {
      const res = await api.get('/produtos')
      return res.data
    }
  })

    
   
  return {
    data,
    isError,
    error
    
  }
  
}
