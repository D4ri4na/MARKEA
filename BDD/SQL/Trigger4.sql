CREATE TRIGGER trg_reducir_stock_al_pagar
ON Ventas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN DELETED d ON i.id_venta = d.id_venta
        WHERE i.estado = 'pagado' AND d.estado <> 'pagado'
    )
    BEGIN
       
        UPDATE p
        SET p.stock = p.stock - dv.cantidad
        FROM Productos p
        JOIN DetalleVenta dv ON p.id_producto = dv.id_producto
        JOIN INSERTED i ON i.id_venta = dv.id_venta
        JOIN DELETED d ON i.id_venta = d.id_venta
        WHERE i.estado = 'pagado' AND d.estado <> 'pagado';

       
        IF EXISTS (
            SELECT 1 FROM Productos WHERE stock < 0
        )
       
    END
END;
