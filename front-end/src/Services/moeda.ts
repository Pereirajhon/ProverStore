
export const valorEmReais = (valor : number) => {
    return new Intl.NumberFormat("pt-BR", {
        currency: "BRL",
        style: 'currency'
    }).format(valor)
}