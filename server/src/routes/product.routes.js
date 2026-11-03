const express = require('express');
const controller = require('../controllers/product.controller');

const router = express.Router();

router.get('/', controller.getAll);        // GET    /api/products  (or /api/products?search=ham)
router.get('/:id', controller.getOne);     // GET    /api/products/5
router.post('/', controller.create);       // POST   /api/products
router.put('/:id', controller.update);     // PUT    /api/products/5
router.delete('/:id', controller.remove);  // DELETE /api/products/5

module.exports = router;
