import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';

import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import { AuthProvider } from './auth/AuthContext';
import { ProtectedRoute } from './auth/ProtectedRoute';
import AuthenticatedLayout from './layout/AuthenticatedLayout';
import NewQuote from './pages/NewQuote';
import PolicyDetails from './pages/PolicyDetails';


function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login />} />

          <Route
            path="/"
            element={
              <ProtectedRoute>
                <AuthenticatedLayout>
                  <Dashboard />
                </AuthenticatedLayout>
              </ProtectedRoute>
            }
          />

          <Route path="*" element={<Navigate to="/" />} />

         <Route
          path="/"
          element={
            <ProtectedRoute>
              <AuthenticatedLayout>
                <Dashboard />
              </AuthenticatedLayout>
            </ProtectedRoute>
          }
        />

        <Route
          path="/quotes/new"
          element={
            <ProtectedRoute>
              <AuthenticatedLayout>
                <NewQuote />
              </AuthenticatedLayout>
            </ProtectedRoute>
          }
        />
        <Route
  path="/policies/:id"
  element={
    <ProtectedRoute>
      <AuthenticatedLayout>
        <PolicyDetails />
      </AuthenticatedLayout>
    </ProtectedRoute>
  }
/>
                </Routes>
              </BrowserRouter>
            </AuthProvider>
  );
}

export default App;
