
import React, { useState } from 'react';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';

interface LoginFormProps {
  onSwitchToRegister: () => void;
}

const LoginForm = ({ onSwitchToRegister }: LoginFormProps) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Login:', { email, password });
    // Aquí irá la lógica de autenticación
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <Label htmlFor="email">Correo Electrónico</Label>
        <Input
          id="email"
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
          className="border-pink-200 focus:border-pink-400"
        />
      </div>

      <div>
        <Label htmlFor="password">Contraseña</Label>
        <Input
          id="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
          className="border-pink-200 focus:border-pink-400"
        />
      </div>

      <Button
        type="submit"
        className="w-full bg-pink-500 hover:bg-pink-600"
      >
        Iniciar Sesión
      </Button>

      <div className="text-center">
        <button
          type="button"
          onClick={onSwitchToRegister}
          className="text-pink-600 hover:underline text-sm"
        >
          ¿No tienes cuenta? Regístrate aquí
        </button>
      </div>
    </form>
  );
};

export default LoginForm;
