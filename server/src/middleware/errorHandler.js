// Express calls this whenever a route throws an error (Express 5 also catches async errors).
function errorHandler(err, req, res, next) {
  // PostgreSQL error codes: https://www.postgresql.org/docs/current/errcodes-appendix.html
  if (err.code === '23505') return res.status(409).json({ message: 'That name already exists' });
  if (err.code === '22P02') return res.status(400).json({ message: 'Invalid id or value' });

  console.error(err); // shows in the terminal and in the Render logs
  res.status(err.status || 500).json({ message: 'Something went wrong on the server' });
}

module.exports = errorHandler;
