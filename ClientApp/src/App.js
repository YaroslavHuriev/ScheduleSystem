import React, { useEffect } from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import { AxiosInterceptor } from './components/AxiosInterceptor';

export default function App() {

  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  useEffect(() => {
    if(!token){
      navigate('/login');
    }
}, [])

  return (
    <Layout>
      <AxiosInterceptor>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </AxiosInterceptor>
    </Layout>
  );
}
