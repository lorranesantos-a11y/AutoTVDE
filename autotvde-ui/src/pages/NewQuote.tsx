import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';
import './new-quote.css';
import {
  QuoteRequest,
  QuotePriceResponse,
  BindQuoteResponse,
} from '../types/quote';

const NewQuote: React.FC = () => {
  const navigate = useNavigate();

  const [form, setForm] = useState<QuoteRequest>({
    birthDate: '',
    vehiclePowerKw: 0,
    vehicleUsage: 'TVDE',
    city: '',
    ncbYears: 0,
    hasGlassCoverage: false,
    hasRoadsideCoverage: false,
  });

  const [quote, setQuote] = useState<QuotePriceResponse | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setQuote(null);

    try {
      const response = await api.post<QuotePriceResponse>(
        '/quotes/price',
        form
      );

      setQuote(response.data);
    } catch (err: any) {
      if (err.response) {
        // 🔹 erro simples (string)
        if (typeof err.response.data === 'string') {
          setError(err.response.data);
        }
        // 🔹 ValidationProblemDetails
        else if (err.response.data?.errors) {
          const firstKey = Object.keys(err.response.data.errors)[0];
          const firstMessage =
            err.response.data.errors[firstKey][0];
          setError(firstMessage);
        } else {
          setError('Erro ao calcular cotação.');
        }
      } else {
        setError('Erro de comunicação com o servidor.');
      }
    } finally {
      setLoading(false);
    }
  };

  const handleBind = async () => {
    if (!quote) return;

    try {
      const response = await api.post<BindQuoteResponse>(
        `/quotes/${quote.quoteId}/bind`
      );

      navigate(`/policies/${response.data.id}`);
    } catch {
      setError('Erro ao emitir apólice.');
    }
  };

return (
  <div className="new-quote-container">
    <div className="new-quote-card">
      <h2>Nova Cotação</h2>

      {error && <div className="error-box">{error}</div>}

      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Data de Nascimento</label>
          <input
            type="date"
            value={form.birthDate}
            onChange={e =>
              setForm({ ...form, birthDate: e.target.value })
            }
            required
          />
        </div>

        <div className="form-group">
          <label>Potência do Veículo (kW)</label>
          <input
            type="number"
            value={form.vehiclePowerKw}
            onChange={e =>
              setForm({
                ...form,
                vehiclePowerKw: Number(e.target.value),
              })
            }
            required
          />
        </div>

        <div className="form-group">
          <label>Cidade</label>
          <input
            type="text"
            value={form.city}
            onChange={e =>
              setForm({ ...form, city: e.target.value })
            }
            required
          />
        </div>

        <div className="form-group">
          <label>Anos sem sinistro (NCB)</label>
          <input
            type="number"
            value={form.ncbYears}
            onChange={e =>
              setForm({
                ...form,
                ncbYears: Number(e.target.value),
              })
            }
          />
        </div>

        <div className="checkbox-group">
          <label>
            <input
              type="checkbox"
              checked={form.hasGlassCoverage}
              onChange={e =>
                setForm({
                  ...form,
                  hasGlassCoverage: e.target.checked,
                })
              }
            />{' '}
            Cobertura de Vidros
          </label>

          <label>
            <input
              type="checkbox"
              checked={form.hasRoadsideCoverage}
              onChange={e =>
                setForm({
                  ...form,
                  hasRoadsideCoverage: e.target.checked,
                })
              }
            />{' '}
            Assistência em Viagem
          </label>
        </div>

        <button
          type="submit"
          className="primary-button"
          disabled={loading}
        >
          {loading ? 'Calculando...' : 'Calcular Cotação'}
        </button>
      </form>

      {quote && (
        <div className="breakdown">
          <h3>Detalhe da Cotação</h3>

          <ul>
            <li><span>Base</span><span>€{quote.breakdown.basePremium.toFixed(2)}</span></li>
            <li><span>Ajuste Idade</span><span>€{quote.breakdown.ageAdjustment.toFixed(2)}</span></li>
            <li><span>Uso TVDE</span><span>€{quote.breakdown.usageAdjustment.toFixed(2)}</span></li>
            <li><span>Cidade</span><span>€{quote.breakdown.citySurcharge.toFixed(2)}</span></li>
            <li><span>NCB</span><span>€{quote.breakdown.ncbDiscount.toFixed(2)}</span></li>
            <li><span>Opcionais</span><span>€{quote.breakdown.optionalCoverages.toFixed(2)}</span></li>
          </ul>

          <div className="breakdown-total">
            Total: €{quote.breakdown.total.toFixed(2)}
          </div>

          <button
            className="secondary-button"
            onClick={handleBind}
          >
            Emitir Apólice
          </button>
        </div>
      )}
    </div>
  </div>
);
};

export default NewQuote;
