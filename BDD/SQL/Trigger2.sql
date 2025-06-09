CREATE TRIGGER trg_evitar_correos_duplicados
ON Usuarios
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si algún correo ya existe
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN Usuarios u ON i.correo = u.correo
    )
    BEGIN
        RAISERROR('Ya existe un usuario con ese correo electrónico.', 16, 1);
        RETURN;
    END

    -- Insertar solo si no hay conflicto
    INSERT INTO Usuarios (usuario, correo, contrasena, es_vendedor)
    SELECT usuario, correo, contrasena, es_vendedor
    FROM INSERTED;
END;
