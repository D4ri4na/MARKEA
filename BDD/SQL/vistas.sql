CREATE VIEW vista_inventario_vendedor AS
SELECT u.usuario AS vendedor, p.nombre AS producto, p.stock
FROM Productos p
JOIN Usuarios u ON p.id_vendedor = u.id_usuario;


CREATE VIEW vista_estado_pedidos_usuario AS
SELECT v.id_venta, v.id_comprador, v.fecha, v.estado, 
       SUM(dv.cantidad * dv.precio_unitario) AS total
FROM Ventas v
JOIN DetalleVenta dv ON v.id_venta = dv.id_venta
GROUP BY v.id_venta, v.id_comprador, v.fecha, v.estado;
