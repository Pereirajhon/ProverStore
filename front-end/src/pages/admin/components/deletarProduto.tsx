import { useMutation, useQueryClient } from '@tanstack/react-query'
import { produtoDelete } from '@/api/produtoApi'
import React from 'react'
import { IProduto } from '@/Types/ProdutoType'
import { AlertDialog, AlertDialogAction, AlertDialogCancel, AlertDialogContent, AlertDialogDescription, AlertDialogFooter, AlertDialogHeader, AlertDialogTitle, AlertDialogTrigger } from '@/components/ui/alert-dialog'
import { Button } from '@/components/ui/button'

type DeleteProps = {
  id: string,
  isOpenModalRemove : boolean
  setIsOpenModalRemove: (isOpenModalRemove:boolean) => void
}

export const DeletarProduto = ({id, isOpenModalRemove, setIsOpenModalRemove}: DeleteProps) => {

    const queryClient = useQueryClient()

    const {mutateAsync: deletarProduto} = useMutation({

      mutationKey: ['produtos'],
      mutationFn: (id:string) => produtoDelete(id),
      onSuccess: (data) => {
        queryClient.getQueryData(['produtos'])
        queryClient.setQueryData(['produtos'], (produto: IProduto[]) => {
          produto.filter(p => p.id !== data.id)
        })
      },
      onError: (e) => {
        console.error(e)
      }
    })

    
    return (
      <AlertDialog onOpenChange={setIsOpenModalRemove} open={isOpenModalRemove}>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Você tem certeza ?</AlertDialogTitle>
          <AlertDialogDescription>
            Esssa ação não pode ser desfeita. Isso irá excluir permanentemente seu produto.
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>
            Cancelar
          </AlertDialogCancel>

          <AlertDialogAction className='bg-destructive' onClick={() => deletarProduto(id)}>
            Excluir
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
