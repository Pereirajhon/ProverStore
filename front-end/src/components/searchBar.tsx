import { Search } from 'lucide-react'
import React, { useCallback, useState } from 'react'
import { useNavigate } from 'react-router-dom'

export const SearchBar = () => {
    const [query, setQuery] = useState('')
    
    const navigate = useNavigate()

    const handleSearch = useCallback((e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    if(query){
        return navigate(`/search?q=${query}`)
    }

    },[query, navigate])

  return (
    <>
        <form onSubmit={handleSearch} className='inline-flex items-center relative'>
            <Search 
                size={22}
                className="text-slate-700 absolute ml-2 visible" 
            />

            <input
                className='w-[384px] h-10 pl-8 pr-4 py-4 bg-[#f2f0ea] placeholder:text-slate-600 text-slate-800 relative rounded-lg' 
                type='search' 
                placeholder= "Buscar"
                onChange={(e) => setQuery(e.target.value)}
            />
        </form>
        
    </>
  )
}
