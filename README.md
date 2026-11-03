# AGP Inventory System

A beginner-friendly starter repository for an inventory management system built in three parts:

| Layer    | Technology                          | Hosted on |
|----------|-------------------------------------|-----------|
| Frontend | C# desktop app (.NET, WinForms or WPF) | Runs on the user's Windows PC |
| Backend  | Node.js + Express (REST API)        | [Render](https://render.com) |
| Database | PostgreSQL                          | [Aiven](https://aiven.io) |

> **Nothing is built yet.** This repository only contains the folder structure,
> README files and the beginner guide. Follow the guide to build each part step by step.

---

## Team

| Member | GitHub | Role |
|--------|--------|------|
| Shaloh (repository owner) | [@Shaloh69](https://github.com/Shaloh69) | Project lead |
| Alexa Jayme | [@algrze43](https://github.com/algrze43) | Collaborator |
| Mitzi Engbino | [@engbinomitzi](https://github.com/engbinomitzi) | Collaborator |
| Elaiza Potot | [@elai0503](https://github.com/elai0503) | Collaborator |

> Fill in each member's role (Database / Backend / Frontend / QA) – see Section 5 of the guide.

## Getting the code

```bash
git clone https://github.com/Shaloh69/AGP-Inventory-System.git
cd AGP-Inventory-System
```

## Start here

1. Read the full guide: **[docs/Beginners-Guide.pdf](docs/Beginners-Guide.pdf)**
2. Pick a UI library: **[docs/UI-LIBRARIES.md](docs/UI-LIBRARIES.md)**
3. Follow the README inside each folder in this order:
   1. [database/README.md](database/README.md) – create the PostgreSQL database on Aiven
   2. [server/README.md](server/README.md) – build the Express API and deploy it to Render
   3. [client/README.md](client/README.md) – build the C# app that talks to the API

---

## How the parts talk to each other

```
 ┌────────────────────┐   HTTPS + JSON    ┌─────────────────────┐   SQL over SSL   ┌──────────────────────┐
 │  C# Desktop App    │ ────────────────▶ │  Node.js + Express  │ ───────────────▶ │  PostgreSQL          │
 │  (client/)         │ ◀──────────────── │  (server/) Render   │ ◀─────────────── │  (database/) Aiven   │
 └────────────────────┘                   └─────────────────────┘                  └──────────────────────┘
```

**Rule:** the C# app **never** connects to the database directly. It only calls the API.
The database password lives only on the server (in environment variables).

---

## Folder structure

```
AGP-Inventory-System/
├── README.md                 ← you are here
├── .gitignore                ← files Git must never upload (.env, bin/, node_modules/ …)
│
├── client/                   ← C# frontend (Visual Studio solution goes here)
│   ├── README.md
│   └── AppClient/
│       ├── Forms/            ← screens / windows (LoginForm, MainForm …)
│       ├── Models/           ← C# classes that match the API's JSON
│       ├── Services/         ← ApiClient: all HTTP calls to the server
│       ├── Helpers/          ← small reusable utilities (validation, formatting)
│       └── Assets/           ← images and icons
│
├── server/                   ← Node.js + Express backend (deployed to Render)
│   ├── README.md
│   ├── .env.example          ← copy to .env and fill in your own values
│   ├── certs/                ← Aiven ca.pem goes here (never committed)
│   └── src/
│       ├── config/           ← database connection (db.js)
│       ├── routes/           ← URL → controller mapping
│       ├── controllers/      ← request handling logic
│       ├── models/           ← SQL queries for each table
│       ├── middleware/       ← auth checks, error handler, logging
│       └── utils/            ← helpers (password hashing, validators)
│
├── database/                 ← SQL scripts for PostgreSQL (Aiven)
│   ├── README.md
│   ├── migrations/           ← CREATE / ALTER TABLE scripts, numbered in order
│   ├── seeds/                ← sample data for testing
│   └── diagrams/             ← ERD images / exports
│
└── docs/                     ← documentation
    ├── README.md
    ├── Beginners-Guide.pdf   ← the full step-by-step guide
    └── UI-LIBRARIES.md       ← UI libraries usable in C#
```

---

## Build plan (summary)

| Phase | Goal | Done when… |
|-------|------|-----------|
| 0 | Install tools, create GitHub / Render / Aiven accounts | `git`, `node`, `dotnet` all print a version |
| 1 | Design and create the database on Aiven | Tables exist and you can query them from DBeaver/pgAdmin |
| 2 | Build the Express API locally | `GET http://localhost:3000/api/health` returns `{ "status": "ok" }` |
| 3 | Deploy the API to Render | The same URL works on `https://agp-api.onrender.com` |
| 4 | Build the C# screens with a UI library | Forms open and look right (with fake data) |
| 5 | Connect C# to the live API | Data you add in the app appears in the Aiven database |
| 6 | Test, polish and document | Checklist in the guide is complete |

Full details for every phase are in the [Beginners Guide](docs/Beginners-Guide.pdf).

---

## Team workflow (Git)

```bash
git checkout -b feature/login-screen   # 1. make a branch for your task
git add .                              # 2. stage your changes
git commit -m "Add login screen UI"    # 3. commit with a clear message
git push -u origin feature/login-screen  # 4. push, then open a Pull Request on GitHub
```

Never commit `.env`, `ca.pem`, passwords, `bin/`, `obj/` or `node_modules/`.
