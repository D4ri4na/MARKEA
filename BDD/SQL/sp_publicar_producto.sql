USE [MARKEA]
GO
/****** Object:  StoredProcedure [dbo].[sp_publicar_producto]    Script Date: 19/6/2025 19:28:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_publicar_producto]
    @id_vendedor INT,
    @id_categoria INT,
    @nombre VARCHAR(100),
    @descripcion TEXT,
    @precio DECIMAL(10,2),
    @stock INT
AS
BEGIN
    -- 1. Validar que el vendedor existe y es vendedor
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @id_vendedor AND es_vendedor = 1)
    BEGIN
        -- Opcional: Lanzar un error para que el backend lo sepa
        RAISERROR ('El ID de vendedor no es válido o no tiene permisos para vender.', 16, 1);
        RETURN;
    END

    -- 2. Insertar el producto
    INSERT INTO Productos(id_vendedor, id_categoria, nombre, descripcion, precio, stock)
    VALUES (@id_vendedor, @id_categoria, @nombre, @descripcion, @precio, @stock);

    -- 3. ¡IMPORTANTE! Devolver el ID del producto que acabamos de crear
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoProductoID;
END;
