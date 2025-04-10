import { api } from "@/Services/Api";
import { IProduto } from "@/Types/ProdutoType";

type Favorito = {
    produtoId : string,
    clienteId : string
}

export const obterProdutosFavoritados = async(clienteId : string):Promise<IProduto[]> => {
    const res = await api.get(`favoritos/${clienteId}`)
    console.log(res.data)
    return res.data?.data
}



export const favoritarApi= async(favorito: Favorito) => {
    try{
        const res = await api.post(`/favoritos`, {
            ClienteId: favorito.clienteId,
            ProdutoId : favorito.produtoId
        })
        
        console.log(res.data)
        
        return res.data
        
    }catch(e){
        console.log(e)
    } 
    

}

export const desFavoritarApi = async(favorito: Favorito) => {
    const res = await api.delete(`/favoritos/${favorito.clienteId}`,{data: favorito})
    
    console.log(res.data)
    return res.data
}