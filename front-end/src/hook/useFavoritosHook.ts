import { useState } from 'react'
import { desFavoritarApi, favoritarApi } from '../api/favoritoApi'
import { useMutation, useQueryClient } from '@tanstack/react-query'

type FavoritosType = {
    clienteId: string,
    produtoId: string,
}

export const useFavoritosHook = () => {

    const queryClient = useQueryClient()

    const favoritar = useMutation({
    mutationKey: ['produtos'],
    mutationFn: async(data: FavoritosType)=> {
        await favoritarApi(data)
    },
    onSuccess: () => {
        queryClient.invalidateQueries({
            queryKey: ['produtos']
        })
    }
   })

   const desFavoritar = useMutation({
    mutationKey: ['produtos'],
    mutationFn:async (data: FavoritosType) => await desFavoritarApi(data),
    onSuccess: () => {
        queryClient.invalidateQueries({
            queryKey: ['produtos']
        })
    }
   })


   return {
    desFavoritar, 
    favoritar
   }
}
