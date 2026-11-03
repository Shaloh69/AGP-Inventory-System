-- 001: the products table (the first feature of the inventory system)
CREATE TABLE products (
    id          SERIAL        PRIMARY KEY,
    name        VARCHAR(120)  NOT NULL UNIQUE,
    quantity    INTEGER       NOT NULL DEFAULT 0 CHECK (quantity >= 0),
    price       NUMERIC(10,2) NOT NULL DEFAULT 0 CHECK (price >= 0),
    created_at  TIMESTAMPTZ   NOT NULL DEFAULT NOW(),
    updated_at  TIMESTAMPTZ   NOT NULL DEFAULT NOW()
);
