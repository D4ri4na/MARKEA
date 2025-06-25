CREATE TRIGGER trg_evitar_correos_duplicados
ON Usuarios
INSTEAD OF INSERT  -- El trigger se ejecutará en lugar de la operación INSERT sobre la tabla Usuarios
AS
BEGIN
    SET NOCOUNT ON;  -- Suprime los mensajes sobre el número de filas afectadas para optimizar el rendimiento

    -- Verifica si el correo que se intenta insertar ya existe en la tabla Usuarios
    IF EXISTS (
        SELECT 1
        FROM INSERTED i  -- INSERTED es una tabla virtual que contiene las nuevas filas que se están insertando
        JOIN Usuarios u ON i.correo = u.correo  -- Se compara el campo 'correo' de lo insertado con lo existente
    )
    BEGIN
        -- Si se detecta un correo duplicado, lanza un error personalizado
        RAISERROR('Ya existe un usuario con ese correo electrónico.', 16, 1);
        RETURN;  -- Termina el bloque evitando que se ejecute el INSERT
    END

    -- Si no hay conflicto de correos, se realiza la inserción normalmente
    INSERT INTO Usuarios (usuario, correo, contrasena, es_vendedor)
    SELECT usuario, correo, contrasena, es_vendedor
    FROM INSERTED;
END;