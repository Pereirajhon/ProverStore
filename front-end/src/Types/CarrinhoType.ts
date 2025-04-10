import { IProduto } from "./ProdutoType"

export type Carrinho = {
    clienteId: string,
    items: CarrinhoItem[],
    totalCarrinho: number
}

type CarrinhoItem = {
    produto : IProduto,
    quantidade: number,
    valor: number
}