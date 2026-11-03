require('dotenv').config({ quiet: true }); // must run before anything reads process.env

let app;
try {
  app = require('./app');
} catch (err) {
  // Setup problems (no .env, no ca.pem ...): show a short message instead of a long crash.
  console.error(`\nThe server could not start:\n  ${err.message}\n`);
  process.exit(1);
}

const PORT = process.env.PORT || 3000;
app
  .listen(PORT, () => console.log(`AGP API running on http://localhost:${PORT}`))
  .on('error', (err) => {
    if (err.code === 'EADDRINUSE') {
      console.error(`\nPort ${PORT} is already in use - another server window is probably still open. Close it and try again.\n`);
    } else {
      console.error(err);
    }
    process.exit(1);
  });
