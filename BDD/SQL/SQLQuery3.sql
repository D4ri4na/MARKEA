CREATE PROCEDURE sp_obtener_productos_por_vendedor
    @id_vendedor INT
AS
BEGIN
    SELECT 
        id_producto AS Id,
        nombre,
        precio,
        stock,
        CASE 
            WHEN stock > 0 THEN 'Activo'
            ELSE 'Vendido'
        END AS Estado
    FROM Productos
    WHERE id_vendedor = @id_vendedor
    ORDER BY id_producto DESC;
END

CREATE PROCEDURE sp_obtener_compras_por_comprador
    @id_comprador INT
AS
BEGIN
    SELECT TOP 5
        v.id_venta AS Id,
        p.nombre AS NombreProducto,
        dv.precio_unitario AS Precio,
        v.fecha AS Fecha,
        vendedor.nombre AS NombreVendedor
    FROM Ventas v
    JOIN DetalleVenta dv ON v.id_venta = dv.id_venta
    JOIN Productos p ON dv.id_producto = p.id_producto
    JOIN Usuarios vendedor ON p.id_vendedor = vendedor.id_usuario
    WHERE v.id_comprador = @id_comprador
    ORDER BY v.fecha DESC;
END