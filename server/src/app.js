// Builds the Express app: middleware first, then routes, then error handlers.
const express = require('express');
const cors = require('cors');
const pool = require('./config/db');
const productRoutes = require('./routes/product.routes');
const notFound = require('./middleware/notFound');
const errorHandler = require('./middleware/errorHandler');

const app = express();
app.use(cors());
app.use(express.json()); // lets us read JSON bodies as req.body

// Is the server running?
app.get('/api/health', (req, res) => res.json({ status: 'ok' }));

// Can the server reach the database?
app.get('/api/health/db', async (req, res) => {
  const result = await pool.query('SELECT NOW() AS time');
  res.json({ status: 'ok', databaseTime: result.rows[0].time });
});

app.use('/api/products', productRoutes);

app.use(notFound);
app.use(errorHandler); // always last

module.exports = app;
