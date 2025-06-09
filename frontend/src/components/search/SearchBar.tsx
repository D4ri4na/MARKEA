
import React from 'react';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Search, Plus } from 'lucide-react';

interface PropiedadesSearchBar {
  value: string;
  onChange: (valor: string) => void;
  onOpenSell?: () => void;
}

const SearchBar = ({ value, onChange, onOpenSell }: PropiedadesSearchBar) => {
  return (
    <div className="max-w-4xl mx-auto flex items-center gap-4">
      <div className="relative flex-1">
        <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-pink-400 h-5 w-5" />
        <Input
          type="text"
          placeholder="¿Qué producto estás buscando?"
          value={value}
          onChange={(e) => onChange(e.target.value)}
          className="pl-10 pr-4 py-3 w-full border-pink-200 focus:border-pink-400 focus:ring-pink-400 rounded-full text-lg"
        />
      </div>
      
      <Button
        onClick={onOpenSell}
        className="bg-pink-500 hover:bg-pink-600 text-white px-6 py-3 rounded-full font-medium flex items-center gap-2"
      >
        <Plus className="h-5 w-5" />
        Vender
      </Button>
    </div>
  );
};

export default SearchBar;
