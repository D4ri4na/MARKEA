
import React from 'react';
import { Card, CardContent } from '@/components/ui/card';

interface Category {
  id: string;
  name: string;
  icon: string;
  color: string;
}

interface CategoryGridProps {
  onSelectCategory: (categoryId: string | null) => void;
  selectedCategory: string | null;
}

const categories: Category[] = [
  { id: 'electronics', name: 'Electrónicos', icon: '📱', color: 'bg-pink-100' },
  { id: 'clothing', name: 'Ropa', icon: '👕', color: 'bg-pink-200' },
  { id: 'home', name: 'Hogar', icon: '🏠', color: 'bg-pink-100' },
  { id: 'beauty', name: 'Belleza', icon: '💄', color: 'bg-pink-200' },
  { id: 'sports', name: 'Deportes', icon: '⚽', color: 'bg-pink-100' },
  { id: 'books', name: 'Libros', icon: '📚', color: 'bg-pink-200' },
];

const CategoryGrid = ({ onSelectCategory, selectedCategory }: CategoryGridProps) => {
  return (
    <div>
      <h2 className="text-2xl font-bold text-center mb-8 text-gray-800">
        Categorías
      </h2>
      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-4">
        {categories.map((category) => (
          <Card
            key={category.id}
            className={`cursor-pointer transition-all duration-200 hover:scale-105 ${
              selectedCategory === category.id
                ? 'ring-2 ring-pink-400 border-pink-400'
                : 'border-pink-100 hover:border-pink-300'
            }`}
            onClick={() => 
              onSelectCategory(selectedCategory === category.id ? null : category.id)
            }
          >
            <CardContent className={`p-6 text-center ${category.color}`}>
              <div className="text-4xl mb-3">{category.icon}</div>
              <h3 className="font-semibold text-gray-700">{category.name}</h3>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default CategoryGrid;
