# Database – PostgreSQL (hosted on Aiven)

## Folder purpose

| Folder | What goes inside | Example |
|--------|------------------|---------|
| `migrations/` | Scripts that create or change tables, run **in number order, once** | `001_create_products_table.sql` |
| `seeds/` | Sample data for testing (safe to run again) | `001_sample_products.sql` |
| `diagrams/` | ERD (Entity Relationship Diagram) exports | `erd.png` |

## Run them

```bash
cd server
npm run db:migrate   # runs every migration that has not run yet (remembered in schema_migrations)
npm run db:seed      # runs every seed file
```

`.\run-dev.bat setup` from the repository root does both.

**Rule:** never edit a migration that has already run. Add a new numbered file instead,
e.g. `002_create_suppliers_table.sql` or `003_add_category_to_products.sql`.

## Create the database on Aiven

1. Sign up at <https://aiven.io> → **Create service** → **PostgreSQL**.
2. Choose the **Free** plan (or a trial) and a region close to your Render region.
3. Wait until the status is **Running**.
4. On the **Overview** page copy:
   - **Service URI** → `server/.env` as `DATABASE_URL` (remove `?sslmode=require` from the end)
   - **CA certificate** (download) → save as `server/certs/ca.pem`
5. Run `npm run db:migrate` and `npm run db:seed` in `server/`.
6. Optional: look at the tables with [DBeaver](https://dbeaver.io) or [pgAdmin](https://www.pgadmin.org)
   (host, port, user, password are on the Aiven Overview page; SSL mode `require`).

## Naming conventions

- Table names: lowercase, plural, snake_case → `products`, `stock_movements`
- Column names: lowercase snake_case → `created_at`, `supplier_id`
- Every table has `id SERIAL PRIMARY KEY`
- Foreign keys: `<singular_table>_id` → `supplier_id INTEGER REFERENCES suppliers(id)`
- Add `created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()` to most tables

## Useful SQL

```sql
-- list tables
SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';

-- which migrations have run?
SELECT * FROM schema_migrations ORDER BY filename;
```
