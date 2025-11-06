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

### 👩‍🎓 Alunos

#### GET `/api/Students`
Lista todos os alunos.

#### POST `/api/Students`
Cria um novo aluno.

##### Exemplo de requisição
```json
{
  "name": "João Silva",
  "plan": "Mensal"
}
```

##### Exemplo de resposta
```json
{
  "id": "b7c1234f-8f9a-4a34-9df2-ffbabc123123",
  "name": "João Silva",
  "plan": "Mensal"
}
```

---

### 🧘‍♀️ Aulas

#### GET `/api/ClassSession`
Lista todas as aulas cadastradas.

#### GET `/api/ClassSession/{id}`
Retorna uma aula específica (com agendamentos incluídos).

#### POST `/api/ClassSession`
Cria uma nova aula.  
⚠️ Inclui verificação para **não permitir duas aulas no mesmo local e horário**.

##### Exemplo de requisição
```json
{
  "classType": "Boxe",
  "startAt": "2025-11-10T05:32:37.938Z",
  "capacity": 10,
  "location": "Sala 01",
  "instructor": "Balboa"
}
```

##### Exemplo de resposta (sucesso)
```json
{
  "id": "ecafbb40-1122-4b21-881a-67f3ed6c1cc9",
  "classType": "Boxe",
  "startAt": "2025-11-10T05:32:37.938Z",
  "capacity": 10,
  "location": "Sala 01",
  "instructor": "Balboa"
}
```

##### Exemplo de resposta (erro)
```json
{
  "message": "Já existe uma aula agendada neste local e horário."
}
```

---

### 📅 Agendamentos

#### POST `/api/Booking`
Agenda um aluno em uma aula, respeitando todas as regras.

##### Exemplo de requisição
```json
{
  "studentId": "b7c1234f-8f9a-4a34-9df2-ffbabc123123",
  "classSessionId": "ecafbb40-1122-4b21-881a-67f3ed6c1cc9"
}
```

##### Exemplo de resposta (sucesso)
```json
{ "success": true }
```

##### Exemplo de resposta (falha)
```json
{ "message": "Aluno já inscrito nesta aula" }
```

Ou:

```json
{ "message": "Aluno já possui uma aula agendada neste horário" }
```

---

### 📊 Relatórios

#### GET `/api/Reports/{studentId}`
Retorna um resumo com:
- Total de aulas agendadas no mês;
- Modalidades mais frequentes.

##### Exemplo de resposta
```json
{
  "student": "João Silva",
  "totalAulasNoMes": 8,
  "modalidadesFrequentes": ["Crossfit", "Funcional"]
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
