USE [MARKEA]
GO
/****** Object:  StoredProcedure [dbo].[sp_iniciar_sesion]    Script Date: 16/6/2025 16:42:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_iniciar_sesion]
    @correo VARCHAR(50),
    @contrasena VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_usuario INT, @nombre VARCHAR(100);

    -- Buscar usuario y OBTENER su id y nombre
    SELECT 
        @id_usuario = id_usuario,
        @nombre = usuario 
    FROM Usuarios
    WHERE correo = @correo AND contrasena = @contrasena;

    -- Validar si el usuario existe
    IF @id_usuario IS NOT NULL
    BEGIN
        SELECT 'Autenticación exitosa' AS mensaje, @id_usuario AS id_usuario, @nombre AS nombre;
    END
    ELSE
    BEGIN
        SELECT 'Usuario o contraseña incorrectos' AS mensaje;
    END
END;