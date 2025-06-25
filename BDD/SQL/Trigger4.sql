CREATE TRIGGER trg_reducir_stock_al_pagar
ON Ventas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;  -- Evita mostrar el número de filas afectadas, mejorando la eficiencia del trigger

    -- Verifica si el estado de alguna venta cambió a 'pagado' (antes no lo era)
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN DELETED d ON i.id_venta = d.id_venta  -- Compara el nuevo estado vs el anterior
        WHERE i.estado = 'pagado' AND d.estado <> 'pagado'
    )
    BEGIN
        -- Si se confirma el cambio de estado a 'pagado', procede a descontar el stock
        UPDATE p
        SET p.stock = p.stock - dv.cantidad  -- Resta la cantidad vendida al stock disponible
        FROM Productos p
        JOIN DetalleVenta dv ON p.id_producto = dv.id_producto
        JOIN INSERTED i ON i.id_venta = dv.id_venta
        JOIN DELETED d ON i.id_venta = d.id_venta
        WHERE i.estado = 'pagado' AND d.estado <> 'pagado';  -- Solo si efectivamente hubo un cambio de estado a 'pagado'

        -- Verifica si tras la actualización algún producto quedó con stock negativo
        IF EXISTS (
            SELECT 1 FROM Productos WHERE stock < 0
        )
        BEGIN
            -- Si ocurre, lanza un error y revierte toda la transacción
            RAISERROR('La cantidad vendida excede el stock disponible.', 16, 1);
            ROLLBACK TRANSACTION;
        END
    END
END;