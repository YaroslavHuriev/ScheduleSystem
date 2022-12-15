import axios from 'axios';
import { useEffect } from 'react'
import { useNavigate } from 'react-router-dom';

const instance = axios.create()

const AxiosInterceptor = ({ children }) => {

    const navigate = useNavigate()
    useEffect(() => {

        const resInterceptor = response => {
            console.log(response)
            return response;
        }

        const errInterceptor = error => {
            if (error.response.status === 401) {
                navigate('/login')
                console.log(axios.defaults.headers.common["Authorization"])
            };
            return Promise.reject();
        }


        const requestInterceptor = instance.interceptors.request.use(config => {
            config.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
            return config;
        });
        const interceptor = instance.interceptors.response.use(resInterceptor, errInterceptor);

        return () => {
            instance.interceptors.request.eject(requestInterceptor);
            instance.interceptors.response.eject(interceptor);
        }

    }, [])
    return children;
}


export default instance;
export { AxiosInterceptor }