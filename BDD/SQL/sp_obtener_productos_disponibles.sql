CREATE PROCEDURE [dbo].[sp_obtener_productos_disponibles]
AS
BEGIN
    SELECT 
        p.id_producto AS Id,
        p.nombre AS Name,
        p.precio AS Price,
        p.descripcion AS Description,
        c.nombre AS Category
    FROM 
        Productos p
    LEFT JOIN 
        Categorias c ON p.id_categoria = c.id_categoria
    WHERE 
        p.stock > 0;
END