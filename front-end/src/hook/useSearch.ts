import React, { useMemo } from 'react'
import { useLocation, useSearchParams } from 'react-router-dom'

export const useSearch = () => {
    const {search} = useLocation()

  return useMemo(() => new URLSearchParams(search), [search])
}
