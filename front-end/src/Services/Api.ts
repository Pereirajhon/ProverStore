import Axios from 'axios';

export const api = Axios.create({
  baseURL: "https://localhost:44397/api",
 // headers: {Authorization: `Bearer ${JSON.parse(window.localStorage.getItem('token')!)}`}

})

api.interceptors.request.use(async(config) =>{
  const token = JSON.parse(window.localStorage.getItem('token')!)
  if(token){
    //config.headers.Authorization = `Bearer ${token.token}`
    api.defaults.headers.common['Authorization'] = `Bearer ${token.token}`
  }
  return config

}, 
(error) => {

  if(error.response?.status == 401){
    console.log('Usuário não Autorizado')
  //  const router = useRouter()
  //  router.push('/auth')
  }
  
  if(error.response?.status == 403 ){
    console.log('Acesso Negado')
  
  }

  return Promise.reject(error)
})

//api.interceptors.response.use(async(response:AxiosResponse) => response,
//(error: AxiosError) => {
  
//)