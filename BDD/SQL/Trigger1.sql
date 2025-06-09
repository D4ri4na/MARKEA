CREATE TRIGGER trg_validar_vendedor_en_ventas
ON Ventas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN Usuarios u ON i.id_vendedor = u.id_usuario
        WHERE u.es_vendedor = 0
    )
    BEGIN
        RAISERROR('El usuario no está habilitado como vendedor.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
