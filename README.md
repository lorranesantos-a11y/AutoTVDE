# AutoTVDE
Plataforma simples para cotação e emissão de apólices de seguro TVDE
Auto TVDE Lite

Auto TVDE Lite é uma aplicação fullstack desenvolvida como parte de um desafio técnico, com o objetivo de simular um sistema simples de cotação e emissão de apólices para veículos TVDE.

Pré-requisitos
Node.js 18+
.NET SDK 9.0
SQL Server LocalDB

# Execução do Projeto
1. Instalação das dependências (frontend) na pasta: AutoTVDE\autotvde-ui
npm install

2. Execução do sistema completo (Frontend + API)
npm start

Este comando executa simultaneamente:
react-scripts start → Frontend React
dotnet run → API ASP.NET Core

## O comando npm start é o ponto de entrada principal do sistema e inicia simultaneamente o frontend React e a API ASP.NET Core.
Não é necessário executar dotnet run manualmente.


Base de Dados & Migrations
Base de dados: SQL Server LocalDB
ORM: Entity Framework Core
Migrations incluídas no repositório:
InitialCreate
AutoTvdeDbContextModelSnapshot

#Importante

Base de Dados (LocalDB)
A aplicação utiliza SQL Server LocalDB

Ao iniciar a API:
O banco será criado automaticamente se não existir
As migrations serão aplicadas
Não é necessário executar comandos manuais de migration para uso básico

A aplicação pode aplicar migrations automaticamente via:

Database.Migrate();
ou manualmente:
dotnet ef database update

## Endereços após inicialização

Após o comando npm start, o sistema estará disponível em:

Frontend (React)
-> http://localhost:3000

API + Swagger
-> https://localhost:5000/swagger/index.html
