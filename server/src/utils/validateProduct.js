// A small, pure function: it receives data and returns a result, nothing else.
// Pure functions are the easiest to write, read and test - start here.
function validateProduct(body) {
  const errors = [];
  const name = typeof body.name === 'string' ? body.name.trim() : '';
  const quantity = Number(body.quantity);
  const price = Number(body.price);

  if (name === '') errors.push('name is required');
  if (name.length > 120) errors.push('name must be 120 characters or less');
  if (!Number.isInteger(quantity) || quantity < 0) errors.push('quantity must be a whole number, 0 or more');
  if (Number.isNaN(price) || price < 0) errors.push('price must be a number, 0 or more');

  return { errors, product: { name, quantity, price } };
}

module.exports = validateProduct;
