-- Sample products for testing. ON CONFLICT DO NOTHING makes it safe to run again.
INSERT INTO products (name, quantity, price) VALUES
    ('Claw Hammer',          25, 349.00),
    ('Measuring Tape 5m',    40, 189.50),
    ('Paint Brush 2"',        3,  65.00),
    ('Wood Screws (100 pcs)', 60,  99.75),
    ('Safety Gloves',         4, 120.00)
ON CONFLICT (name) DO NOTHING;
