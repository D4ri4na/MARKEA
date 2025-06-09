
import React from 'react';
import ProductCard from './ProductCard';

interface Product {
  id: string;
  name: string;
  price: number;
  image: string;
  description: string;
  category: string;
}

interface ProductGridProps {
  searchQuery: string;
  selectedCategory: string | null;
}

// Productos de ejemplo
const mockProducts: Product[] = [
  {
    id: '1',
    name: 'iPhone 14',
    price: 999,
    image: '/placeholder.svg',
    description: 'Último modelo de iPhone',
    category: 'electronics'
  },
  {
    id: '2',
    name: 'Vestido Rosa',
    price: 59,
    image: '/placeholder.svg',
    description: 'Hermoso vestido rosa',
    category: 'clothing'
  },
  {
    id: '3',
    name: 'Lámpara Moderna',
    price: 129,
    image: '/placeholder.svg',
    description: 'Lámpara para el hogar',
    category: 'home'
  },
  {
    id: '4',
    name: 'Labial Mate',
    price: 25,
    image: '/placeholder.svg',
    description: 'Labial de larga duración',
    category: 'beauty'
  },
];

const ProductGrid = ({ searchQuery, selectedCategory }: ProductGridProps) => {
  const filteredProducts = mockProducts.filter((product) => {
    const matchesSearch = product.name.toLowerCase().includes(searchQuery.toLowerCase());
    const matchesCategory = !selectedCategory || product.category === selectedCategory;
    return matchesSearch && matchesCategory;
  });

  return (
    <div>
      <h2 className="text-2xl font-bold mb-6 text-gray-800">
        Productos {selectedCategory && `- ${selectedCategory}`}
      </h2>
      
      {filteredProducts.length === 0 ? (
        <div className="text-center py-12">
          <p className="text-gray-500 text-lg">No se encontraron productos</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {filteredProducts.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      )}
    </div>
  );
};

export default ProductGrid;
