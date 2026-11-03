// Controllers read the request, call the model, and send the response.
const Product = require('../models/product.model');
const validateProduct = require('../utils/validateProduct');

async function getAll(req, res) {
  const products = await Product.findAll(req.query.search);
  res.json(products);
}

async function getOne(req, res) {
  const product = await Product.findById(req.params.id);
  if (!product) return res.status(404).json({ message: 'Product not found' });
  res.json(product);
}

async function create(req, res) {
  const { errors, product } = validateProduct(req.body);
  if (errors.length > 0) return res.status(400).json({ message: errors.join(', ') });

  const saved = await Product.create(product);
  res.status(201).json(saved);
}

async function update(req, res) {
  const { errors, product } = validateProduct(req.body);
  if (errors.length > 0) return res.status(400).json({ message: errors.join(', ') });

  const saved = await Product.update(req.params.id, product);
  if (!saved) return res.status(404).json({ message: 'Product not found' });
  res.json(saved);
}

async function remove(req, res) {
  const deleted = await Product.remove(req.params.id);
  if (!deleted) return res.status(404).json({ message: 'Product not found' });
  res.status(204).end();
}

module.exports = { getAll, getOne, create, update, remove };
