import { useEffect, useState } from 'react';
import api from '../api/axios';

interface Client {
  id: string;
  name: string;
  email: string;
  nif: string;
}

export default function Clients() {
  const [clients, setClients] = useState<Client[]>([]);

  useEffect(() => {
    api.get('/clients?page=1&pageSize=10')
      .then(res => setClients(res.data.items));
  }, []);

  return (
    <div>
      <h2>Clients</h2>
      <ul>
        {clients.map(c => (
          <li key={c.id}>{c.name} — {c.nif}</li>
        ))}
      </ul>
    </div>
  );
}