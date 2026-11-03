# Database – PostgreSQL (hosted on Aiven)

> Not built yet. Follow the steps below (and **Phase 1** of
> [../docs/Beginners-Guide.pdf](../docs/Beginners-Guide.pdf)).

## Folder purpose

| Folder | What goes inside | Example |
|--------|------------------|---------|
| `migrations/` | Scripts that create or change tables, run **in number order** | `001_create_users_table.sql` |
| `seeds/` | Sample/test data | `001_sample_users.sql` |
| `diagrams/` | ERD (Entity Relationship Diagram) exports | `erd.png` |

**Rule:** never edit a migration that teammates have already run. Add a new numbered
file instead (e.g. `004_add_phone_to_users.sql`).

## Create the database on Aiven

1. Sign up at <https://aiven.io> → **Create service** → **PostgreSQL**.
2. Choose the **Free** plan (or a trial) and a region close to your Render region.
3. Wait until the status is **Running**.
4. On the **Overview** page copy:
   - **Service URI** → goes into `server/.env` as `DATABASE_URL`
   - **CA certificate** (download) → save as `server/certs/ca.pem`
5. Connect with a GUI tool to run your SQL scripts:
   - [DBeaver Community](https://dbeaver.io) (recommended) or [pgAdmin](https://www.pgadmin.org)
   - Host, port, user, password and database are on the Aiven Overview page
   - SSL mode: `require` (or `verify-full` with the CA certificate)

## Naming conventions

- Table names: lowercase, plural, snake_case → `users`, `order_items`
- Column names: lowercase snake_case → `first_name`, `created_at`
- Every table has `id SERIAL PRIMARY KEY` (or `GENERATED ALWAYS AS IDENTITY`)
- Foreign keys: `<singular_table>_id` → `user_id`
- Add `created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()` to most tables

## Useful SQL

```sql
-- list tables
SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';

-- see the columns of a table
SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'users';
```
