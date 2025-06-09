
import React, { useState } from 'react';
import Header from '../components/layout/Header';
import SearchBar from '../components/search/SearchBar';
import CategoryGrid from '../components/categories/CategoryGrid';
import ProductGrid from '../components/products/ProductGrid';
import Cart from '../components/cart/Cart';
import AuthModal from '../components/auth/AuthModal';
import SellModal from '../components/sell/SellModal';

const Index = () => {
  const [esAuthAbierto, setEsAuthAbierto] = useState(false);
  const [modoAuth, setModoAuth] = useState<'login' | 'register'>('login');
  const [consultaBusqueda, setConsultaBusqueda] = useState('');
  const [categoriaSeleccionada, setCategoriaSeleccionada] = useState<string | null>(null);
  const [esCarritoAbierto, setEsCarritoAbierto] = useState(false);
  const [esModalVentaAbierto, setEsModalVentaAbierto] = useState(false);

  const manejarAbrirAuth = (modo: 'login' | 'register') => {
    setModoAuth(modo);
    setEsAuthAbierto(true);
  };

  const manejarAbrirVenta = () => {
    setEsModalVentaAbierto(true);
  };

  return (
    <div className="min-h-screen bg-background">
      <Header 
        alAbrirAuth={manejarAbrirAuth}
        alAbrirCarrito={() => setEsCarritoAbierto(true)}
      />
      
      <main className="container mx-auto px-4 py-8">
        <div className="mb-8">
          <SearchBar 
            value={consultaBusqueda}
            onChange={setConsultaBusqueda}
            onOpenSell={manejarAbrirVenta}
          />
        </div>

        <div className="mb-12">
          <CategoryGrid 
            onSelectCategory={setCategoriaSeleccionada}
            selectedCategory={categoriaSeleccionada}
          />
        </div>

        <div>
          <ProductGrid 
            searchQuery={consultaBusqueda}
            selectedCategory={categoriaSeleccionada}
          />
        </div>
      </main>

      <AuthModal
        isOpen={esAuthAbierto}
        onClose={() => setEsAuthAbierto(false)}
        mode={modoAuth}
        onSwitchMode={setModoAuth}
      />

      <Cart
        isOpen={esCarritoAbierto}
        onClose={() => setEsCarritoAbierto(false)}
      />

      <SellModal
        isOpen={esModalVentaAbierto}
        onClose={() => setEsModalVentaAbierto(false)}
        onOpenAuth={manejarAbrirAuth}
      />
    </div>
  );
};

export default Index;
