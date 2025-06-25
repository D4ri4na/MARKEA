CREATE TRIGGER trg_actualizar_stock
ON DetalleVenta
AFTER INSERT  -- Se activa después de que se inserte una nueva fila en la tabla DetalleVenta
AS
BEGIN
    SET NOCOUNT ON;  -- Evita que se devuelvan mensajes sobre el número de filas afectadas, optimizando el rendimiento

    -- Actualiza el stock en la tabla Productos, restando la cantidad vendida
    UPDATE p
    SET p.stock = p.stock - dv.cantidad  -- Reduce el stock disponible por la cantidad registrada en la venta
    FROM Productos p
    JOIN INSERTED dv ON p.id_producto = dv.id_producto;
    -- Se usa la tabla virtual INSERTED para acceder a las nuevas filas insertadas en DetalleVenta

    -- Verifica si algún producto ha quedado con stock negativo tras la actualización
    IF EXISTS (
        SELECT 1 FROM Productos WHERE stock < 0
    )
    BEGIN
        -- Si hay productos con stock negativo, lanza un error y revierte la transacción completa
        RAISERROR('La cantidad vendida excede el stock disponible.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;