
import React, { useState } from 'react';
import { Sheet, SheetContent, SheetHeader, SheetTitle } from '@/components/ui/sheet';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Textarea } from '@/components/ui/textarea';
import { Upload, ImagePlus } from 'lucide-react';

interface SellModalProps {
  isOpen: boolean;
  onClose: () => void;
  onOpenAuth: (mode: 'login' | 'register') => void;
}

const SellModal = ({ isOpen, onClose, onOpenAuth }: SellModalProps) => {
  // Simular estado de autenticación (por ahora false)
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  
  const [productData, setProductData] = useState({
    name: '',
    price: '',
    quantity: '',
    description: '',
    image: null as File | null
  });

  const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setProductData({ ...productData, image: file });
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Datos del producto a enviar a Supabase:', productData);
    // TODO: Aquí se conectará con Supabase para guardar el producto
    alert('Producto publicado exitosamente!');
    onClose();
  };

  const handleLogin = () => {
    onClose();
    onOpenAuth('login');
  };

  if (!isAuthenticated) {
    return (
      <Sheet open={isOpen} onOpenChange={onClose}>
        <SheetContent side="right" className="w-full sm:max-w-md">
          <SheetHeader>
            <SheetTitle className="text-pink-600">Vender Producto</SheetTitle>
          </SheetHeader>
          
          <div className="mt-6 text-center space-y-4">
            <div className="bg-pink-50 p-6 rounded-lg">
              <h3 className="text-lg font-medium text-pink-800 mb-2">
                Inicia sesión para vender
              </h3>
              <p className="text-pink-600 mb-4">
                Necesitas una cuenta para publicar productos en nuestra tienda.
              </p>
              <Button
                onClick={handleLogin}
                className="bg-pink-500 hover:bg-pink-600 text-white w-full"
              >
                Iniciar Sesión
              </Button>
            </div>
            
            <p className="text-sm text-gray-500">
              ¿No tienes cuenta? Al hacer clic en "Iniciar Sesión" también podrás registrarte.
            </p>
          </div>
        </SheetContent>
      </Sheet>
    );
  }

  return (
    <Sheet open={isOpen} onOpenChange={onClose}>
      <SheetContent side="right" className="w-full sm:max-w-lg overflow-y-auto">
        <SheetHeader>
          <SheetTitle className="text-pink-600">Vender Producto</SheetTitle>
        </SheetHeader>
        
        <form onSubmit={handleSubmit} className="mt-6 space-y-6">
          {/* Nombre del producto */}
          <div>
            <Label htmlFor="productName">Nombre del Producto *</Label>
            <Input
              id="productName"
              type="text"
              value={productData.name}
              onChange={(e) => setProductData({ ...productData, name: e.target.value })}
              placeholder="Ej: iPhone 15 Pro Max"
              required
              className="border-pink-200 focus:border-pink-400"
            />
          </div>

          {/* Precio */}
          <div>
            <Label htmlFor="price">Precio *</Label>
            <div className="relative">
              <span className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-500">$</span>
              <Input
                id="price"
                type="number"
                value={productData.price}
                onChange={(e) => setProductData({ ...productData, price: e.target.value })}
                placeholder="0.00"
                min="0"
                step="0.01"
                required
                className="pl-8 border-pink-200 focus:border-pink-400"
              />
            </div>
          </div>

          {/* Cantidad */}
          <div>
            <Label htmlFor="quantity">Cantidad Disponible *</Label>
            <Input
              id="quantity"
              type="number"
              value={productData.quantity}
              onChange={(e) => setProductData({ ...productData, quantity: e.target.value })}
              placeholder="1"
              min="1"
              required
              className="border-pink-200 focus:border-pink-400"
            />
          </div>

          {/* Imagen */}
          <div>
            <Label htmlFor="image">Imagen del Producto</Label>
            <div className="mt-2">
              <label htmlFor="image" className="cursor-pointer">
                <div className="border-2 border-dashed border-pink-200 rounded-lg p-6 text-center hover:border-pink-400 transition-colors">
                  {productData.image ? (
                    <div className="space-y-2">
                      <ImagePlus className="mx-auto h-8 w-8 text-pink-500" />
                      <p className="text-sm text-pink-600">
                        Imagen seleccionada: {productData.image.name}
                      </p>
                      <p className="text-xs text-gray-500">
                        Haz clic para cambiar
                      </p>
                    </div>
                  ) : (
                    <div className="space-y-2">
                      <Upload className="mx-auto h-8 w-8 text-pink-400" />
                      <p className="text-sm text-pink-600">
                        Haz clic para subir una imagen
                      </p>
                      <p className="text-xs text-gray-500">
                        PNG, JPG o GIF (máx. 5MB)
                      </p>
                    </div>
                  )}
                </div>
              </label>
              <input
                id="image"
                type="file"
                accept="image/*"
                onChange={handleImageUpload}
                className="hidden"
              />
            </div>
          </div>

          {/* Descripción */}
          <div>
            <Label htmlFor="description">Descripción *</Label>
            <Textarea
              id="description"
              value={productData.description}
              onChange={(e) => setProductData({ ...productData, description: e.target.value })}
              placeholder="Describe tu producto, incluye características importantes, estado, etc."
              rows={4}
              required
              className="border-pink-200 focus:border-pink-400"
            />
          </div>

          {/* Botones */}
          <div className="flex space-x-3 pt-4">
            <Button
              type="button"
              variant="outline"
              onClick={onClose}
              className="flex-1 border-pink-200 text-pink-600 hover:bg-pink-50"
            >
              Cancelar
            </Button>
            <Button
              type="submit"
              className="flex-1 bg-pink-500 hover:bg-pink-600 text-white"
            >
              Publicar Producto
            </Button>
          </div>
        </form>

        {/* Nota sobre conexión a base de datos */}
        <div className="mt-6 p-4 bg-gray-50 rounded-lg">
          <p className="text-xs text-gray-600">
            <strong>Nota de desarrollo:</strong> Los datos se enviarán a Supabase cuando se configure la base de datos.
          </p>
        </div>
      </SheetContent>
    </Sheet>
  );
};

export default SellModal;
