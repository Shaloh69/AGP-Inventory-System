// Runs every .sql file in database/migrations, in name order, ONCE.
// Files that already ran are remembered in the schema_migrations table.
// Usage: npm run db:migrate
require('dotenv').config({ quiet: true });
const fs = require('fs');
const path = require('path');
const pool = require('../config/db');

const folder = path.join(__dirname, '..', '..', '..', 'database', 'migrations');

async function migrate() {
  await pool.query(`
    CREATE TABLE IF NOT EXISTS schema_migrations (
      filename TEXT PRIMARY KEY,
      ran_at   TIMESTAMPTZ NOT NULL DEFAULT NOW()
    )`);

  const done = new Set((await pool.query('SELECT filename FROM schema_migrations')).rows.map((r) => r.filename));
  const files = fs.readdirSync(folder).filter((f) => f.endsWith('.sql')).sort();

  for (const file of files) {
    if (done.has(file)) {
      console.log(`  skip  ${file}`);
      continue;
    }
    const sql = fs.readFileSync(path.join(folder, file), 'utf8');
    const client = await pool.connect();
    try {
      await client.query('BEGIN'); // all of the file, or nothing
      await client.query(sql);
      await client.query('INSERT INTO schema_migrations (filename) VALUES ($1)', [file]);
      await client.query('COMMIT');
      console.log(`  ran   ${file}`);
    } catch (err) {
      await client.query('ROLLBACK');
      throw new Error(`${file} failed: ${err.message}`);
    } finally {
      client.release();
    }
  }
}

migrate()
  .then(() => console.log('Migrations complete.'))
  .catch((err) => { console.error(err.message); process.exitCode = 1; })
  .finally(() => pool.end());
