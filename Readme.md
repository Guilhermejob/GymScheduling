# 🏋️‍♂️ Gym Class Scheduler API

API REST desenvolvida em **C# (.NET 8)** para gerenciar **aulas coletivas em academias**, permitindo o **cadastro de alunos, aulas, agendamentos** e geração de **relatórios mensais**.

---

## 🧩 Funcionalidades

### 👨‍🎓 Alunos
- Cadastro de alunos com nome e tipo de plano:
  - **Mensal:** até 12 aulas/mês  
  - **Trimestral:** até 20 aulas/mês  
  - **Anual:** até 30 aulas/mês  

### 🧘‍♀️ Aulas
- Cadastro de aulas com:
  - Tipo (Cross, Funcional, Pilates etc)
  - Data e hora
  - Capacidade máxima de alunos  

### 📅 Agendamentos
- Um aluno pode se inscrever em aulas, respeitando:
  - Capacidade máxima da turma  
  - Limite mensal do plano do aluno  

### 📊 Relatórios
- Total de aulas agendadas no mês por aluno  
- Tipos de aula mais frequentes  

---

## ⚙️ Tecnologias e Dependências

| Componente | Descrição |
|-------------|------------|
| **.NET 8** | Framework principal |
| **Entity Framework Core** | ORM para banco de dados |
| **SQL Server** | Banco de dados relacional |
| **Swagger / Swashbuckle** | Documentação interativa da API |
| **Newtonsoft.Json** *(opcional)* | Serialização JSON avançada |

---

## 🏗️ Estrutura de Pastas

```plaintext
GYMSCHEDULING/
├── Application/
│   ├── Results/
│   │    └── Result.cs
│   ├── Services/
│   │    └── SchedullingService.cs
│ 
├── Controllers/
│   ├── StudentsController.cs
│   ├── ClassesController.cs
│   └── BookingsController.cs
│
├── Data/
│   └── GymDbContext.cs
│
├── Domain/
│   ├── Entities/
│   │    └── ClasSession.cs
│   │    └── Schedule.cs
│   │    └── Student.cs
│   ├── Enums/
│   │    └── PlanType.cs
│   └── ValueObjects/
│ 
├── DTOs/
│   └── CreateClassSessionDto.cs
│   └── CreateScheduleDto.cs
│   └── CreateStudentDto.cs
│
├── Migrations/
│
├── Program.cs
├── appsettings.json
└── README.md
```

---

## 🚀 Como executar o projeto

### 1️⃣ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)  
- [Visual Studio 2022/2023] ou [VS Code]

---

### 2️⃣ Clonar o repositório

```bash
git git@github.com:Guilhermejob/GymScheduling.git
cd GymScheduling
```

---

### 3️⃣ Configurar o banco de dados

No arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GymSchedulingDb;Trusted_Connection=True;"
  }
}
```

---

### 4️⃣ Executar as migrações

```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### 5️⃣ Rodar a aplicação

```bash
dotnet run
```

URL padrão:
```
https://localhost:5001/
```

Swagger disponível na raiz (caso configurado com `RoutePrefix = string.Empty`).

---

## 🧠 Regras de Negócio

| Regra | Descrição |
|-------|------------|
| 📌 Capacidade máxima | Nenhuma aula pode ultrapassar o limite definido |
| 📌 Plano mensal | Até 12 aulas/mês |
| 📌 Plano trimestral | Até 20 aulas/mês |
| 📌 Plano anual | Até 30 aulas/mês |
| 📌 Duplicidade | Aluno não pode agendar a mesma aula duas vezes |
| 📌 Concorrência | Transações protegem contra overbooking |

---

## 🔐 Endpoints Principais

### 👨‍🎓 Alunos

**GET** `/api/students`  
**POST** `/api/students`

#### 🧾 Exemplo de requisição

```json
{
  "name": "Carlos Silva",
  "plan": "Mensal"
}
```

#### 🧾 Exemplo de resposta

```json
{
  "id": "8b8a41f2-5db6-4e21-a7f4-734f77f3f0c2",
  "name": "Carlos Silva",
  "plan": "Mensal"
}
```

---

### 🧘‍♀️ Aulas

**GET** `/api/classes`  
**POST** `/api/classes`

#### 🧾 Exemplo de requisição

```json
{
  "type": "Cross",
  "startAt": "2025-11-10T18:00:00",
  "capacity": 10
}
```

#### 🧾 Exemplo de resposta

```json
{
  "id": "f45b7231-3b0f-4b32-9a16-f0ec90a7d3e7",
  "type": "Cross",
  "startAt": "2025-11-10T18:00:00",
  "capacity": 10
}
```

---

### 📅 Agendamentos

**POST** `/api/bookings`

#### 🧾 Exemplo de requisição

```json
{
  "studentId": "8b8a41f2-5db6-4e21-a7f4-734f77f3f0c2",
  "classSessionId": "f45b7231-3b0f-4b32-9a16-f0ec90a7d3e7"
}
```

#### 🧾 Exemplo de resposta (sucesso)

```json
{
  "success": true,
  "message": "Agendamento realizado com sucesso."
}
```

#### 🧾 Exemplo de resposta (falha)

```json
{
  "success": false,
  "message": "Limite mensal do plano atingido."
}
```

---

### 📊 Relatórios

**GET** `/api/reports/{studentId}`

#### 🧾 Exemplo de resposta

```json
{
  "student": "Carlos Silva",
  "totalClassesThisMonth": 10,
  "mostFrequentClassTypes": ["Cross", "Pilates"]
}
```

---

## 🧰 Uso via Swagger

```bash
dotnet run
```

Acesse:
```
https://localhost:5001/
```

---

## 🧱 Padrão de Branches e Commits

### 🌿 Branches
- `master`: versão estável  
- `feature/<nome>`: novas features  
- `fix/<nome>`: correções

### 🧩 Commits

| Tipo | Exemplo |
|------|----------|
| `feat:` | `feat: adiciona endpoint de agendamento` |
| `fix:` | `fix: corrige validação de limite mensal` |
| `refactor:` | `refactor: melhora lógica de transação` |
| `docs:` | `docs: adiciona README.md` |

---



## 💬 Autor

**Guilherme Job**  
[LinkedIn](https://www.linkedin.com/in/guilherme-armesto-job/) | [GitHub](https://github.com/Guilhermejob)
