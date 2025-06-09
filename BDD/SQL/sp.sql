CREATE PROCEDURE sp_registrar_usuario
    @usuario VARCHAR(100),
    @correo VARCHAR(100),
    @contrasena VARCHAR(255),
    @es_vendedor BIT = 1
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Usuarios WHERE correo = @correo)
        RETURN;

    INSERT INTO Usuarios(usuario, correo, contrasena, es_vendedor)
    VALUES (@usuario, @correo, @contrasena, @es_vendedor);
END;


CREATE PROCEDURE obtener_productos_por_categoria
    @id_categoria INT
AS
BEGIN
    SELECT p.nombre AS producto, p.precio, p.stock, c.nombre AS categoria
    FROM Productos p
    JOIN Categorias c ON p.id_categoria = c.id_categoria
    WHERE p.stock > 0 AND c.id_categoria = @id_categoria;
END;


CREATE PROCEDURE sp_publicar_producto
    @id_vendedor INT,
    @id_categoria INT,
    @nombre VARCHAR(100),
    @descripcion TEXT,
    @precio DECIMAL(10,2),
    @stock INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @id_vendedor AND es_vendedor = 1)
        RETURN;
    INSERT INTO Productos(id_vendedor, id_categoria, nombre, descripcion, precio, stock)
    VALUES (@id_vendedor, @id_categoria, @nombre, @descripcion, @precio, @stock);
END;


CREATE PROCEDURE buscar_producto_por_nombre
    @nombre_producto NVARCHAR(255)
AS
BEGIN
    SELECT p.nombre AS producto, p.precio, p.stock, c.nombre AS categoria
    FROM Productos p
    JOIN Categorias c ON p.id_categoria = c.id_categoria
    WHERE p.nombre LIKE '%' + @nombre_producto + '%'
    AND p.stock > 0;
END;

EXEC buscar_producto_por_nombre @nombre_producto = 'a'


CREATE PROCEDURE sp_recuperar_historial_compras
    @id_usuario INT
AS
BEGIN
    SELECT v.id_venta, v.fecha, v.estado,
           dv.id_producto, p.nombre, dv.cantidad, dv.precio_unitario
    FROM Ventas v
    JOIN DetalleVenta dv ON v.id_venta = dv.id_venta
    JOIN Productos p ON dv.id_producto = p.id_producto
    WHERE v.id_comprador = @id_usuario
    ORDER BY v.fecha DESC;
END;


CREATE PROCEDURE sp_iniciar_sesion
    @usuario VARCHAR(50),
    @contraseña VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_usuario INT, @nombre VARCHAR(100);

    SELECT @id_usuario = id_usuario
    FROM Usuarios
    WHERE usuario = @usuario AND contrasena = @contraseña;

    IF @id_usuario IS NOT NULL
    BEGIN
        SELECT 'Autenticación exitosa' AS mensaje, @id_usuario AS id_usuario;
    END
    ELSE
    BEGIN
        SELECT 'Usuario o contraseña incorrectos' AS mensaje;
    END
END;