import {LogOut, CircleUser, Heart, ShoppingBag, Search} from 'lucide-react'
import {Link, NavLink} from 'react-router-dom'
import { UseAuthContext } from '@/context/AuthProvider'
import { useCarrinhoStore } from '@/Store/CarrinhoStore'
import { SearchBar } from './searchBar'
import { logoutApi } from '@/api/apiAuth'

export const Navbar = () => {
const carrinho = useCarrinhoStore((state) => state)
const {auth} = UseAuthContext()
console.log(auth)
console.log(localStorage.getItem('token'))
console.log(carrinho.carrinho)

console.log(window.localStorage.getItem('cart')!)
  return (
    <>
        <header className="px-6 h-24 w-full border-b border-border bg-background/95 supports-[backdrop-filter]:bg-background/80 backdrop-saturate-200 backdrop-blur-[20px] z-50">
        <div className='flex items-center justify-between w-full h-full'>
            <a href='/' className='text-black text-2xl font-bold'>
                ProverStore
            </a>
            <SearchBar />
            <ul className="flex gap-8">
                <li>
                    <NavLink to={`/favoritos/${auth.usuario?.id}`}>
                        <Heart size={26}/>
                    </NavLink>
                </li>
                <li>
                    <NavLink className='flex' to="/carrinho" > {<ShoppingBag/>} {carrinho.carrinho.carrinhoItems.length ?? 0}  </NavLink>
                </li>
                {auth.token ? (
                    <li>
                        <NavLink
                        onClick={() =>{ 
                            logoutApi()
                            window.localStorage.removeItem('token')
                            
                        }}  
                        to={"/auth"}
                        > 
                            <LogOut size={26}/>
                        </NavLink>
                    </li>
                ):(
                    <li>
                        <NavLink to={"/auth"}>
                            <CircleUser size={26}/>
                        </NavLink>
                    </li>
                )}
                
            </ul>
        </div>
    </header>
    </>
    
  )
}
