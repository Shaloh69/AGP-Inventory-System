# Server – Node.js + Express (hosted on Render)

The REST API that sits between the C# app and the PostgreSQL database.

> Not built yet. Follow the steps below (and **Phase 2–3** of
> [../docs/Beginners-Guide.pdf](../docs/Beginners-Guide.pdf)).

## Folder purpose

| Folder | What goes inside | Example file |
|--------|------------------|--------------|
| `src/config/` | Database connection and app settings | `db.js` |
| `src/routes/` | Maps URLs to controller functions | `task.routes.js` |
| `src/controllers/` | Reads the request, calls the model, sends the response | `task.controller.js` |
| `src/models/` | SQL queries for one table | `task.model.js` |
| `src/middleware/` | Runs before/after routes (auth, errors, logging) | `errorHandler.js` |
| `src/utils/` | Small helpers | `password.js` |
| `certs/` | Aiven `ca.pem` (ignored by Git) | `ca.pem` |
| `src/index.js` | Entry point: creates the app and starts listening | – |

## Request flow

```
C# app ──▶ route ──▶ middleware ──▶ controller ──▶ model (SQL) ──▶ PostgreSQL
   ◀──────────────────── JSON response ◀────────────────────────────┘
```

## First-time setup (local)

```bash
cd server
npm init -y
npm install express pg dotenv cors
# optional, later: npm install bcrypt jsonwebtoken
```

Add scripts to `package.json`:

```json
"scripts": {
  "start": "node src/index.js",
  "dev": "node --watch src/index.js"
}
```

Then:

1. Copy `.env.example` to `.env` and fill in your Aiven values.
2. Put the Aiven **CA certificate** in `certs/ca.pem`.
3. Create `src/index.js` and `src/config/db.js` (code is in the guide).
4. Run `npm run dev` and open `http://localhost:3000/api/health`.

## Deploying to Render

| Render setting | Value |
|----------------|-------|
| Service type   | Web Service |
| Root Directory | `server` |
| Runtime        | Node |
| Build Command  | `npm install` |
| Start Command  | `npm start` |
| Environment    | `DATABASE_URL`, `DB_CA_PATH=/etc/secrets/ca.pem`, `JWT_SECRET` |
| Secret File    | `ca.pem` (paste the Aiven certificate) |

Render's free plan **sleeps after ~15 minutes without traffic**; the first request
afterwards can take up to a minute. The C# app should use a long timeout and show a
"Connecting…" message.

## API naming convention

| Action | Method | URL |
|--------|--------|-----|
| List all | `GET` | `/api/tasks` |
| Get one | `GET` | `/api/tasks/:id` |
| Create | `POST` | `/api/tasks` |
| Update | `PUT` | `/api/tasks/:id` |
| Delete | `DELETE` | `/api/tasks/:id` |

Use plural nouns, lowercase, no verbs in URLs. Always return JSON.

## Rules

- Never put passwords in code – use `process.env.*`.
- Always use parameterised queries: `pool.query('... WHERE id = $1', [id])`.
- Never store plain passwords – hash them with `bcrypt`.
