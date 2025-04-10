

import { IProduto } from '../Types/ProdutoType'
import { create } from 'zustand'
import { createJSONStorage, persist } from 'zustand/middleware'

type Carrinho = {
    item: IProduto,
    quantidade: number,
    valor: number
}

type State = {
    
    carrinho: {
        carrinhoItems: Carrinho[],
        totalCarrinho: number
    }
}

type Action = {
    addAoCarrinho: (item: IProduto) => void,
    isProdutoCarrinho: (itemId: string) => boolean,
    decrementarQuantidade: (item: IProduto) => void,
    incrementarQuantidade: (item: IProduto) => void,
    removerItemDoCarrinho: (itemId: string) => void,
    esvaziarCarrinho: () => void,
    calcularTotalCarrinho: () => void
}

export const useCarrinhoStore = create<State & Action>()(
    persist(
        (set, get) => ({
            carrinho: {
                carrinhoItems: [],
                totalCarrinho: 0
            },
            isProdutoCarrinho: (itemId: string) =>{
                return get().carrinho.carrinhoItems.some(produtoCart => produtoCart.item.id === itemId) 
                
            },
            addAoCarrinho: (item) => set((state) => {
                const { carrinho } = state;
                const itemExiste = carrinho.carrinhoItems.findIndex(i => i.item.id === item.id);

                if (itemExiste < 0 && item.ativo) {
                    const novoItem: Carrinho = {
                        item,
                        quantidade: 1,
                        valor: item.valor
                    };

                    const novoTotalCarrinho = carrinho.totalCarrinho + novoItem.valor;
                    const novoCarrinho = {
                        ...carrinho,
                        carrinhoItems: [...carrinho.carrinhoItems, novoItem],
                        totalCarrinho: novoTotalCarrinho
                    };

                    return { carrinho: novoCarrinho };
                }

                return state;
            }),

            decrementarQuantidade: (item) => set((state) => {
                const { carrinho } = state;
                const carrinhoItemIndex = carrinho.carrinhoItems.findIndex(ci => ci.item.id === item.id);

                if (carrinhoItemIndex !== -1 && carrinho.carrinhoItems[carrinhoItemIndex].quantidade > 1) {
                    const newCarrinhoItems = [...carrinho.carrinhoItems];
                    newCarrinhoItems[carrinhoItemIndex].quantidade -= 1;
                    newCarrinhoItems[carrinhoItemIndex].valor -= item.valor;

                    const novoTotalCarrinho = carrinho.totalCarrinho - item.valor;

                    const novoCarrinho = {
                        ...carrinho,
                        carrinhoItems: newCarrinhoItems,
                        totalCarrinho: novoTotalCarrinho
                    };

                    return { carrinho: novoCarrinho };
                }

                return state;
            }),

            incrementarQuantidade: (item) => set((state) => {
                const { carrinho } = state;
                const itemExiste = carrinho.carrinhoItems.findIndex(i => i.item.id === item.id);

                if (itemExiste !== -1 && item.ativo) {
                    const newCarrinhoItems = [...carrinho.carrinhoItems];

                    if(item.estoque <= newCarrinhoItems[itemExiste].quantidade) return state;
                    
                    newCarrinhoItems[itemExiste].quantidade += 1;
                    newCarrinhoItems[itemExiste].valor += item.valor;

                    const novoTotalCarrinho = carrinho.totalCarrinho + item.valor;

                    const novoCarrinho = {
                        ...carrinho,
                        carrinhoItems: newCarrinhoItems,
                        totalCarrinho: novoTotalCarrinho
                    };

                    return { carrinho: novoCarrinho };
                }

                return state;
            }),

            removerItemDoCarrinho: (itemId) => set((state) => {
                const { carrinho } = state;
                const carrinhoAtualizado = carrinho.carrinhoItems.filter(ci => ci.item.id !== itemId);

                const novoTotalCarrinho = carrinhoAtualizado.reduce((acc, ci) => acc + ci.valor, 0);

                const novoCarrinho = {
                    ...carrinho,
                    carrinhoItems: carrinhoAtualizado,
                    totalCarrinho: novoTotalCarrinho
                };

                return { carrinho: novoCarrinho };
            }),

            esvaziarCarrinho: () => set(() => {
                return {
                    carrinho: {
                        carrinhoItems: [],
                        totalCarrinho: 0
                    }
                };
            }),

            calcularTotalCarrinho: () => set((state) => {
                const { carrinho } = state;
                const calculoTotalCarrinho = carrinho.carrinhoItems.reduce((acc, item) => acc + item.valor, 0);

                const novoCarrinho = {
                    ...carrinho,
                    totalCarrinho: calculoTotalCarrinho
                };

                return { carrinho: novoCarrinho };
            })
        }),
        {
            name: 'cart',
            storage: createJSONStorage(() => localStorage)
        }
    )
);
