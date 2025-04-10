
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import { useCallback, useState } from "react"
import { UpdateProduto } from "./updateProduto"
import { DeletarProduto } from "./deletarProduto"


type DropdownProps = {
    children : React.ReactNode,
    produtoId: string
  }
  
  export const DropdownMenuComponent = ({produtoId, children}: DropdownProps ) => {
  
    const [isOpenModalUpdate, setIsOpenModalUpdate] = useState(false)
    const [isOpenModalRemove, setIsOpenModalRemove] = useState(false)
  
    const handleOpenModalUpdate = useCallback(() => {
        setIsOpenModalUpdate(prev => !prev)
    },[isOpenModalUpdate])

    const handleOpenModalRemove = useCallback(() => {
        setIsOpenModalRemove(prev => !prev)
    },[isOpenModalRemove])

    return (
      <>
        <DropdownMenu>
          <DropdownMenuTrigger>{children}</DropdownMenuTrigger>
          <DropdownMenuContent>
            <DropdownMenuLabel>Ações</DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem onClick={handleOpenModalUpdate }>
              Atualizar
            </DropdownMenuItem>
            <DropdownMenuItem onClick= {handleOpenModalRemove}>
              Deletar
            </DropdownMenuItem>
            <DropdownMenuItem>Detalhes</DropdownMenuItem>
  
          </DropdownMenuContent>
        </DropdownMenu>

        <UpdateProduto 
         setIsOpenModalUpdate={setIsOpenModalUpdate} 
         isOpenModalUpdate={isOpenModalUpdate}  
         id={produtoId} 
        />
        <DeletarProduto 
         setIsOpenModalRemove={setIsOpenModalRemove} 
         isOpenModalRemove= {isOpenModalRemove} 
         id={produtoId} 
        />
      </>
    )
  }