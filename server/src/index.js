require('dotenv').config({ quiet: true }); // must run before anything reads process.env
const app = require('./app');

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`AGP API running on http://localhost:${PORT}`));
