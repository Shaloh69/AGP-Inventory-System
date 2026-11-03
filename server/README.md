# Server – Node.js + Express (hosted on Render)

The REST API between the C# app and the PostgreSQL database. It already has one complete,
tested feature – **products** – that you copy for every new feature.

## Run it

```bash
# from the repository root
run-dev.bat setup      # first time: npm install + create tables + sample data
run-dev.bat server     # start on http://localhost:3000 (restarts when you save a file)
```

Or by hand inside `server/`:

```bash
npm install
npm run db:migrate     # runs database/migrations/*.sql that have not run yet
npm run db:seed        # adds database/seeds/*.sql sample data (safe to repeat)
npm run dev            # start with auto-restart   (npm start = without)
```

Before the first run: copy `.env.example` to `.env` and fill in your Aiven values, and save
the Aiven CA certificate as `certs/ca.pem`.

## Folder map

| File | Job |
|------|-----|
| `src/index.js` | Entry point: loads `.env`, starts listening |
| `src/app.js` | Builds the app: middleware → routes → error handlers |
| `src/config/db.js` | PostgreSQL connection pool (SSL for Aiven) |
| `src/routes/product.routes.js` | URL + method → controller function |
| `src/controllers/product.controller.js` | Reads the request, validates, calls the model, sends JSON |
| `src/models/product.model.js` | **All SQL** for the `products` table |
| `src/utils/validateProduct.js` | A small pure function that checks input |
| `src/middleware/errorHandler.js` | Turns errors into JSON (409 duplicate, 400 bad id, 500) |
| `src/middleware/notFound.js` | 404 for unknown URLs |
| `src/scripts/migrate.js` · `seed.js` | `npm run db:migrate` · `npm run db:seed` |

## Endpoints

| Method | URL | Body | Success |
|--------|-----|------|---------|
| GET | `/api/health` | – | `{ "status": "ok" }` |
| GET | `/api/health/db` | – | `{ "status": "ok", "databaseTime": … }` |
| GET | `/api/products` | – | 200 + array (add `?search=ham` to filter) |
| GET | `/api/products/:id` | – | 200 + product, or 404 |
| POST | `/api/products` | `{ "name", "quantity", "price" }` | 201 + new product |
| PUT | `/api/products/:id` | `{ "name", "quantity", "price" }` | 200 + updated product |
| DELETE | `/api/products/:id` | – | 204 |

Errors always look like `{ "message": "..." }` – 400 invalid input, 404 not found,
409 name already exists, 500 server error.

## Add a new feature (e.g. suppliers)

1. `database/migrations/002_create_suppliers_table.sql` → `npm run db:migrate`
2. `src/models/supplier.model.js` (copy `product.model.js`, change the SQL)
3. `src/controllers/supplier.controller.js`
4. `src/routes/supplier.routes.js`
5. In `src/app.js`: `app.use('/api/suppliers', require('./routes/supplier.routes'));`
6. Test with Postman / Bruno, then add the C# side (see `client/README.md`).

## Deploying to Render

| Render setting | Value |
|----------------|-------|
| Service type   | Web Service |
| Root Directory | `server` |
| Build Command  | `npm install` |
| Start Command  | `npm start` |
| Health Check   | `/api/health` |
| Environment    | `DATABASE_URL`, `DB_CA_PATH=/etc/secrets/ca.pem` |
| Secret File    | `ca.pem` (paste the Aiven certificate) |

Run `npm run db:migrate` once against Aiven from your PC (with the Aiven `.env`) to create the tables.
Render's free plan sleeps after ~15 minutes idle; the first request can take up to a minute.

## Rules

- Never put passwords in code – use `process.env.*`.
- SQL only in models, always with `$1, $2` placeholders.
- Never store plain passwords – hash them with `bcrypt`.
