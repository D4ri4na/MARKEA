
import React from 'react';
import { Card, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { ShoppingCart } from 'lucide-react';

interface Product {
  id: string;
  name: string;
  price: number;
  image: string;
  description: string;
  category: string;
}

interface ProductCardProps {
  product: Product;
}

const ProductCard = ({ product }: ProductCardProps) => {
  const handleAddToCart = () => {
    console.log('Agregando al carrito:', product.id);
    // Aquí irá la lógica del carrito
  };

  return (
    <Card className="overflow-hidden hover:shadow-lg transition-shadow duration-200 border-pink-100">
      <div className="aspect-square bg-pink-50">
        <img
          src={product.image}
          alt={product.name}
          className="w-full h-full object-cover"
        />
      </div>
      <CardContent className="p-4">
        <h3 className="font-semibold text-lg mb-2 text-gray-800">
          {product.name}
        </h3>
        <p className="text-gray-600 text-sm mb-3 line-clamp-2">
          {product.description}
        </p>
        <div className="flex items-center justify-between">
          <span className="text-2xl font-bold text-pink-600">
            ${product.price}
          </span>
          <Button
            onClick={handleAddToCart}
            size="sm"
            className="bg-pink-500 hover:bg-pink-600 text-white"
          >
            <ShoppingCart className="h-4 w-4 mr-1" />
            Agregar
          </Button>
        </div>
      </CardContent>
    </Card>
  );
};

export default ProductCard;
