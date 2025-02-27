import React from 'react';
import ReactDOM from 'react-dom/client';
import './style/index.css';
import { BrowserRouter, Routes, Route } from "react-router";

import Home from './pages/Home';
import Layout from './Layout';
import NumberSums from './pages/NumberSums';
import NotFound from './pages/NotFound';

export default function App() {

  return (
    <BrowserRouter>
    <Routes>
      <Route path='/' element={<Layout />}>
        <Route index element={<Home />} />
        <Route path='numsums' element={<NumberSums />} />
        <Route path='*' status={404} element={<NotFound />} />
      </Route>
    </Routes>
    </BrowserRouter>
  );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);
