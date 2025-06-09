
import React from 'react';
import { Sheet, SheetContent, SheetHeader, SheetTitle } from '@/components/ui/sheet';
import { Button } from '@/components/ui/button';
import { Minus, Plus, Trash2 } from 'lucide-react';

interface CartItem {
  id: string;
  name: string;
  price: number;
  quantity: number;
  image: string;
}

interface CartProps {
  isOpen: boolean;
  onClose: () => void;
}

// Datos de ejemplo del carrito
const mockCartItems: CartItem[] = [
  {
    id: '1',
    name: 'iPhone 14',
    price: 999,
    quantity: 1,
    image: '/placeholder.svg'
  },
  {
    id: '2',
    name: 'Vestido Rosa',
    price: 59,
    quantity: 2,
    image: '/placeholder.svg'
  }
];

const Cart = ({ isOpen, onClose }: CartProps) => {
  const total = mockCartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);

  const handleQuantityChange = (id: string, change: number) => {
    console.log('Cambiar cantidad:', id, change);
    // Aquí irá la lógica para actualizar cantidad
  };

  const handleRemoveItem = (id: string) => {
    console.log('Eliminar item:', id);
    // Aquí irá la lógica para eliminar del carrito
  };

  const handleCheckout = () => {
    console.log('Proceder al pago');
    // Aquí irá la lógica de pago
  };

  return (
    <Sheet open={isOpen} onOpenChange={onClose}>
      <SheetContent side="right" className="w-full sm:max-w-md">
        <SheetHeader>
          <SheetTitle className="text-pink-600">Carrito de Compras</SheetTitle>
        </SheetHeader>
        
        <div className="flex flex-col h-full">
          <div className="flex-1 overflow-y-auto py-4">
            {mockCartItems.length === 0 ? (
              <div className="text-center py-8">
                <p className="text-gray-500">Tu carrito está vacío</p>
              </div>
            ) : (
              <div className="space-y-4">
                {mockCartItems.map((item) => (
                  <div key={item.id} className="flex items-center space-x-3 p-3 border border-pink-100 rounded-lg">
                    <img
                      src={item.image}
                      alt={item.name}
                      className="w-16 h-16 object-cover rounded-md bg-pink-50"
                    />
                    <div className="flex-1">
                      <h3 className="font-medium text-sm">{item.name}</h3>
                      <p className="text-pink-600 font-semibold">${item.price}</p>
                      
                      <div className="flex items-center space-x-2 mt-2">
                        <Button
                          size="sm"
                          variant="outline"
                          onClick={() => handleQuantityChange(item.id, -1)}
                          className="h-8 w-8 p-0 border-pink-200"
                        >
                          <Minus className="h-4 w-4" />
                        </Button>
                        <span className="w-8 text-center">{item.quantity}</span>
                        <Button
                          size="sm"
                          variant="outline"
                          onClick={() => handleQuantityChange(item.id, 1)}
                          className="h-8 w-8 p-0 border-pink-200"
                        >
                          <Plus className="h-4 w-4" />
                        </Button>
                        <Button
                          size="sm"
                          variant="outline"
                          onClick={() => handleRemoveItem(item.id)}
                          className="h-8 w-8 p-0 border-red-200 text-red-500 hover:bg-red-50"
                        >
                          <Trash2 className="h-4 w-4" />
                        </Button>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
          
          {mockCartItems.length > 0 && (
            <div className="border-t pt-4 space-y-4">
              <div className="flex justify-between items-center">
                <span className="text-lg font-semibold">Total:</span>
                <span className="text-2xl font-bold text-pink-600">${total}</span>
              </div>
              <Button
                onClick={handleCheckout}
                className="w-full bg-pink-500 hover:bg-pink-600"
              >
                Proceder al Pago
              </Button>
            </div>
          )}
        </div>
      </SheetContent>
    </Sheet>
  );
};

export default Cart;
