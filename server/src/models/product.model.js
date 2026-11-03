// The model is the ONLY place that writes SQL for the products table.
// Every value goes in through $1, $2 ... placeholders - never glue values into the SQL string.
const pool = require('../config/db');

async function findAll(search) {
  if (search) {
    const result = await pool.query(
      `SELECT * FROM products WHERE name ILIKE '%' || $1 || '%' ORDER BY name`,
      [search]
    );
    return result.rows;
  }
  const result = await pool.query('SELECT * FROM products ORDER BY name');
  return result.rows;
}

async function findById(id) {
  const result = await pool.query('SELECT * FROM products WHERE id = $1', [id]);
  return result.rows[0]; // undefined when not found
}

async function create({ name, quantity, price }) {
  const result = await pool.query(
    `INSERT INTO products (name, quantity, price)
     VALUES ($1, $2, $3)
     RETURNING *`,
    [name, quantity, price]
  );
  return result.rows[0];
}

async function update(id, { name, quantity, price }) {
  const result = await pool.query(
    `UPDATE products
        SET name = $1, quantity = $2, price = $3, updated_at = NOW()
      WHERE id = $4
      RETURNING *`,
    [name, quantity, price, id]
  );
  return result.rows[0];
}

async function remove(id) {
  const result = await pool.query('DELETE FROM products WHERE id = $1', [id]);
  return result.rowCount > 0;
}

module.exports = { findAll, findById, create, update, remove };
