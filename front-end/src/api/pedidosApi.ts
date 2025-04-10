import { api } from '@/Services/Api'

export const pedidosPorClienteApi = async (clienteId : string) => {
    try {
        const res = await api.get(`/pedidos/${clienteId}`)
        return res.data  

    } catch (error) {
        console.error(error)
    }

}

export const todosPedidos = async() => {
    const res = await api.get('/pedidos')

    return res.data
}

export const pedidosCliente = async(clienteId: string) => {
    const res = await api.get(`pedidos/${clienteId}`)

    return res.data
}

