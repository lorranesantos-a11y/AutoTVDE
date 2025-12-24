import { useNavigate } from 'react-router-dom';
import './dashboard.css';

const Dashboard = () => {
  const navigate = useNavigate();

  return (
    <div className="dashboard">
      <h2>Bem-vindo</h2>
      <p className="muted">Ações disponíveis</p>

      <button onClick={() => navigate('/quotes/new')}>
       Nova Cotação
      </button>
    </div>
  );
};

export default Dashboard;
