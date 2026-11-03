// Runs every .sql file in database/seeds (sample data for testing).
// Seed files use ON CONFLICT DO NOTHING, so running this twice is safe.
// Usage: npm run db:seed
require('dotenv').config({ quiet: true });
const fs = require('fs');
const path = require('path');
const pool = require('../config/db');

const folder = path.join(__dirname, '..', '..', '..', 'database', 'seeds');

async function seed() {
  const files = fs.readdirSync(folder).filter((f) => f.endsWith('.sql')).sort();
  for (const file of files) {
    await pool.query(fs.readFileSync(path.join(folder, file), 'utf8'));
    console.log(`  seeded ${file}`);
  }
}

seed()
  .then(() => console.log('Seeding complete.'))
  .catch((err) => { console.error(err.message); process.exitCode = 1; })
  .finally(() => pool.end());
