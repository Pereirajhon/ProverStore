import { api } from '@/Services/Api' 
import { Carrinho } from '@/Types/CarrinhoType'
import React from 'react'

type CheckoutCompra= {
  clienteId : string,
  carrinhoId : string,
  carrinhoItem: {
    quantidade: number, 
    produtoId: string,
    totalProduto: number
  }[]
}

export const carrinhocheckoutApi = async(carrinho: Carrinho ) => {
  try{
    
    const res = await api.post('/checkout', {
      carrinho
    })
    console.log(res.data)
    return res.data

  }catch(e: any){
    console.error(e)
  }
  
}

export const checkoutCompraApi = async(checkout: CheckoutCompra ) => {
  try{
    
    const res = await api.post('/checkout', {
      checkout
    })
    console.log(res.data)
    if(res.data.data){
      window.location.href = res.data
    }

  }catch(e: any){
    console.error(e)
  }
  
}
