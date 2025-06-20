USE [MARKEA]
GO
/****** Object:  StoredProcedure [dbo].[sp_obtener_productos_disponibles]    Script Date: 20/6/2025 12:09:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_obtener_productos_disponibles]
AS
BEGIN
    SELECT 
        p.id_producto AS Id,
        p.nombre AS Name,
        p.precio AS Price,
        p.descripcion AS Description,
        c.nombre AS Category,
        p.id_categoria AS IdCategoria -- <<< AÑADE ESTA LÍNEA
    FROM 
        Productos p
    LEFT JOIN 
        Categorias c ON p.id_categoria = c.id_categoria
    WHERE 
        p.stock > 0;
END