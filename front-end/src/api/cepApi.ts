import { api } from '@/Services/Api'
import axios from 'axios'
import React from 'react'

export const cepApi = async(cep: string) => {
  const response = await axios.get(`viacep.com.br/ws/${cep}/json/`)
  return response.data
}
