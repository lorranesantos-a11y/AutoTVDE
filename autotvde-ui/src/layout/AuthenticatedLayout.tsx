import React from 'react';
import { useAuth } from '../auth/AuthContext';
import './layout.css';

const AuthenticatedLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { logout } = useAuth();

  return (
    <div className="app-container">
      <header className="app-header">
        <div>
          <h1>Auto TVDE Lite</h1>
          <p>Plataforma simples de cotação e emissão de apólices</p>
        </div>

        <button onClick={logout} className="logout-button">
          Sair
        </button>
      </header>

      <main className="app-content">{children}</main>
    </div>
  );
};

export default AuthenticatedLayout;
