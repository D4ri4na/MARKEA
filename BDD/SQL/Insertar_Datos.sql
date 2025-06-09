-- Insertar usuarios
USE MARKEA
INSERT INTO Usuarios (usuario, correo, contrasena, es_vendedor) VALUES
('JuanPerez', 'juan@example.com', 'hashedpassword1', 0),
('MariaLopez', 'maria@example.com', 'hashedpassword2', 1),
('CarlosGomez', 'carlos@example.com', 'hashedpassword3', 1),
('AnaTorres', 'ana@example.com', 'hashedpassword4', 0);

-- Insertar categorías
INSERT INTO Categorias (nombre) VALUES
('Electrónica'),
('Ropa'),
('Hogar'),
('Libros');

INSERT INTO Productos (id_vendedor, id_categoria, nombre, descripcion, precio, stock) VALUES
(2, 1, 'Auriculares Bluetooth', 'Auriculares inalámbricos con cancelación de ruido', 49.99, 100),
(2, 2, 'Camisa de algodón', 'Camisa casual para hombre', 25.00, 50),
(3, 3, 'Lámpara de escritorio', 'Lámpara LED con brazo flexible', 35.50, 30),
(3, 4, 'Libro de SQL', 'Aprende SQL desde cero', 20.00, 200);

INSERT INTO Ventas (id_comprador, id_vendedor, estado) VALUES
(1, 2, 'pagado'),
(4, 3, 'enviado');

INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, precio_unitario) VALUES
(1, 1, 2, 49.99),
(1, 2, 1, 25.00),
(2, 3, 1, 35.50),
(2, 4, 3, 20.00);
