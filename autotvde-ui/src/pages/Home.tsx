import { useAuth } from '../auth/AuthContext';

export default function Home() {
  const { logout } = useAuth();

  return (
    <div>
      <h1>Auto TVDE Lite</h1>
      <p>Utilizador autenticado</p>

      <button onClick={logout}>Logout</button>
    </div>
  );
}
