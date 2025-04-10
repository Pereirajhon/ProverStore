import React from 'react'
import { createBrowserRouter, Routes } from 'react-router-dom'
import NotFound from './pages/NotFound'
import DashboardPage from './pages/admin/dashboard/page'
import PedidosPage from './pages/admin/pedidos/page'
import HomePage from './pages/Home/home'
import CarrinhoPage from './pages/carrinho/page'
import FavoritosPage from './pages/favoritos/page'
import { AppLayout } from './pages/_layouts/App'
import AuthPage from './pages/auth/page'
import {RotasProtegidas} from './Services/RotasProtegidas'
import ProdutoPage from './pages/produto'
import SearchPage from './pages/search/page'

export const rotas = createBrowserRouter([
  {
    path: '/',
    element: <AppLayout />,
    errorElement: <NotFound/> ,
    
    children: [
      {
        path: '/',
        element: <HomePage/>,
      },
      {
        path: '/search',
        element: <SearchPage/>,
      },

      {
        path: '/produto/:id',
        element: <ProdutoPage/>,
      },
      {
        path: '/carrinho',
        element: <CarrinhoPage/>,
      },
      {
        path: '/checkout',
        element: <CheckoutPage/>,
      },
      {
        path: '/favoritos/:usuario_id',
        element: <FavoritosPage/>
      },
      {
        path: '/auth',
        element: <AuthPage/>,
      },
    ],
  },
  {
    path: '/',
    element: <RotasProtegidas role='User' />,
    children: [
      {
        path: '/auth',
        element: <AuthPage/>,
      },
    ],
  },
  {
    path: '/admin',
    element: <RotasProtegidas role='ADMIN' />,
    children: [
      {
        path: '/admin/dashboard',
        element: <DashboardPage/>,
      },
      {
        path: '/admin/pedidos',
        element: <PedidosPage/>,
      },
    ],

  },
])
