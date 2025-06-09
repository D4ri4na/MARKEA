
import React from 'react';
import { Button } from '@/components/ui/button';
import { ShoppingCart, User, UserPlus } from 'lucide-react';

interface PropiedadesHeader {
  alAbrirAuth: (modo: 'login' | 'register') => void;
  alAbrirCarrito: () => void;
}

const Header = ({ alAbrirAuth, alAbrirCarrito }: PropiedadesHeader) => {
  return (
    <header className="bg-white border-b border-pink-100 shadow-sm">
      <div className="container mx-auto px-4 py-4 flex items-center justify-between">
        <div className="flex items-center space-x-4">
          <img 
            src="/lovable-uploads/4bb9542e-8e4f-4ed5-8917-ca3c6e6c4b39.png" 
            alt="Markea Logo" 
            className="h-10 w-auto"
          />
        </div>

        <div className="flex items-center space-x-4">
          <Button
            variant="outline"
            size="sm"
            onClick={() => alAbrirAuth('login')}
            className="border-pink-200 text-pink-600 hover:bg-pink-50"
          >
            <User className="h-4 w-4 mr-2" />
            Iniciar Sesión
          </Button>
          
          <Button
            variant="outline"
            size="sm"
            onClick={() => alAbrirAuth('register')}
            className="border-pink-200 text-pink-600 hover:bg-pink-50"
          >
            <UserPlus className="h-4 w-4 mr-2" />
            Registrarse
          </Button>

          <Button
            variant="outline"
            size="sm"
            onClick={alAbrirCarrito}
            className="border-pink-200 text-pink-600 hover:bg-pink-50"
          >
            <ShoppingCart className="h-4 w-4 mr-2" />
            Carrito
          </Button>
        </div>
      </div>
    </header>
  );
};

export default Header;
