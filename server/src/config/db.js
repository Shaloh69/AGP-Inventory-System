const fs = require('fs');
const path = require('path');
const { Pool, types } = require('pg');

// PostgreSQL sends NUMERIC (e.g. price 12.50) as text so no precision is lost.
// For this app a normal JavaScript number is fine, so convert it (type id 1700 = NUMERIC).
types.setTypeParser(1700, (value) => parseFloat(value));

// Aiven only accepts SSL connections that are checked against its CA certificate.
// For a database on your own PC without SSL, put DB_SSL=false in .env.
function sslConfig() {
  if (process.env.DB_SSL === 'false') return false;

  const caPath = process.env.DB_CA_PATH;
  if (!caPath || !fs.existsSync(caPath)) {
    throw new Error(
      `CA certificate not found at "${caPath}". Download it from the Aiven console ` +
      'and save it as server/certs/ca.pem (or set DB_SSL=false for a local database).'
    );
  }
  return { rejectUnauthorized: true, ca: fs.readFileSync(path.resolve(caPath)).toString() };
}

if (!process.env.DATABASE_URL) {
  throw new Error('DATABASE_URL is missing. Copy server/.env.example to server/.env and fill it in.');
}

// A Pool keeps a few connections open and reuses them (much faster than reconnecting).
const pool = new Pool({
  connectionString: process.env.DATABASE_URL,
  ssl: sslConfig(),
});

module.exports = pool;
