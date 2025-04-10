
import { api } from "../Services/Api"
import { IProduto, ProdutoForm } from "../Types/ProdutoType"

export const produtoAdicionar = async (produto: ProdutoForm) => {
    
    try{
        const res = await api.post('/produtos', produto)

        console.log(res.data)
        return res.data

    }catch(e){
        console.error(e)
    }
}

export const produtoEditar = async(produto: ProdutoForm, id: string) => {
    console.log(produto)
    const res = await api.put(`produtos/${id}`, {...produto, id} )
    console.log(res.data)
    return res.data
}

export const produtoDelete = async(id : string) => {
    const res = await api.delete(`produtos/${id}`)
    return res.data
}

export const produtoGetId = async(id: string) => {
    try{
        const res = await api.get(`produtos/${id}`)
        console.log(res.data?.data)
        return res.data

    }catch(e){
        console.error(e)
    }
} 

export const searchProdutos = async(query: string ) => {
    try{
        const response = await api.get(`produtos/search?query=${query}`)

        console.log(response.data)
        return response.data?.data

    }catch(e){
        console.log(e)
    }
}