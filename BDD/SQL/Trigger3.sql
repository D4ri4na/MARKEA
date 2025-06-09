CREATE TRIGGER trg_actualizar_stock
ON DetalleVenta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE p
    SET p.stock = p.stock - dv.cantidad
    FROM Productos p
    JOIN INSERTED dv ON p.id_producto = dv.id_producto;

    IF EXISTS (
        SELECT 1 FROM Productos WHERE stock < 0
    )
    BEGIN
        RAISERROR('La cantidad vendida excede el stock disponible.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
