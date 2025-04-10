export interface IProduto{
    id: string,
    modelo: string,
    categoria: string,
    marca: string,
    descricao: string,
    imagemProduto: string,
    estoque: number,
    valor: number,
    favoritados: number,
    ativo: boolean,
    clienteIdsFavorito: string[]
}

export type ProdutoForm = {
    modelo: string,
    categoria: string,
    marca: string,
    descricao: string,
    imagemProduto: string,
    estoque: number,
    valor: number,
    ativo: boolean,
}
