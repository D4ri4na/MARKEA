CREATE TRIGGER trg_validar_vendedor_en_ventas
ON Ventas
AFTER INSERT  -- Se ejecuta automáticamente después de una inserción en la tabla Ventas
AS
BEGIN
    SET NOCOUNT ON;  -- Evita que se devuelvan conteos innecesarios de filas afectadas, mejora el rendimiento

    -- Verifica si alguno de los vendedores involucrados en la inserción no está habilitado como vendedor
    IF EXISTS (
        SELECT 1
        FROM INSERTED i  -- INSERTED es una tabla virtual que contiene las nuevas filas insertadas
        JOIN Usuarios u ON i.id_vendedor = u.id_usuario  -- Se relaciona con la tabla Usuarios usando el id del vendedor
        WHERE u.es_vendedor = 0  -- Busca aquellos usuarios que NO están habilitados como vendedores
    )
    BEGIN
        -- Lanza un error personalizado y detiene la transacción si se intentó registrar una venta con un usuario no autorizado
        RAISERROR('El usuario no está habilitado como vendedor.', 16, 1);
        ROLLBACK TRANSACTION;  -- Revierte completamente la inserción en la tabla Ventas
    END
END;