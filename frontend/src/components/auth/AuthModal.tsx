
import React, { useState } from 'react';
import { Sheet, SheetContent, SheetHeader, SheetTitle } from '@/components/ui/sheet';
import LoginForm from './LoginForm';
import RegisterForm from './RegisterForm';

interface AuthModalProps {
  isOpen: boolean;
  onClose: () => void;
  mode: 'login' | 'register';
  onSwitchMode: (mode: 'login' | 'register') => void;
}

const AuthModal = ({ isOpen, onClose, mode, onSwitchMode }: AuthModalProps) => {
  return (
    <Sheet open={isOpen} onOpenChange={onClose}>
      <SheetContent side="right" className="w-full sm:max-w-md">
        <SheetHeader>
          <SheetTitle className="text-pink-600">
            {mode === 'login' ? 'Iniciar Sesión' : 'Registrarse'}
          </SheetTitle>
        </SheetHeader>
        
        <div className="mt-6">
          {mode === 'login' ? (
            <LoginForm onSwitchToRegister={() => onSwitchMode('register')} />
          ) : (
            <RegisterForm onSwitchToLogin={() => onSwitchMode('login')} />
          )}
        </div>
      </SheetContent>
    </Sheet>
  );
};

export default AuthModal;
