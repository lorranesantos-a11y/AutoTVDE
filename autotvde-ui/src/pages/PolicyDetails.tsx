import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { PolicyResponse } from '../types/policy';
import './policy-details.css';

const PolicyDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [policy, setPolicy] = useState<PolicyResponse | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadPolicy = async () => {
      try {
        const response = await api.get<PolicyResponse>(
          `/policies/${id}`
        );
        setPolicy(response.data);
      } catch {
        alert('Erro ao carregar apólice');
      } finally {
        setLoading(false);
      }
    };

    loadPolicy();
  }, [id]);

  if (loading) return <p>Carregando apólice...</p>;
  if (!policy) return <p>Apólice não encontrada.</p>;

  return (
    <div className="policy-container">
<button
  onClick={() => navigate(-2)}
  className="back-link"
>
  ← Voltar ao Dashboard
</button>

      <div className="policy-card">
        <h2 className="policy-title">Detalhes da Apólice</h2>

        <ul className="policy-list">
          <li>
            <span className="policy-label">Número</span>
            <span className="policy-value">{policy.policyNumber}</span>
          </li>

          <li>
            <span className="policy-label">Vigência</span>
            <span className="policy-value">
              {new Date(policy.effectiveFrom).toLocaleDateString()} até{' '}
              {new Date(policy.effectiveTo).toLocaleDateString()}
            </span>
          </li>

          <li>
            <span className="policy-label">Comissão</span>
            <span className="policy-value">
              €{policy.commission.toFixed(2)}
            </span>
          </li>
        </ul>

        <div className="policy-total">
          <span>Prémio Total</span>
          <span>€{policy.totalPremium.toFixed(2)}</span>
        </div>
      </div>
    </div>
  );
};

export default PolicyDetails;
