ALTER PROCEDURE sp_realizar_venta
    @id_comprador INT,
    @productos XML
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @id_venta INT;
        DECLARE @id_vendedor INT;

        SELECT @id_vendedor = id_vendedor FROM Productos WHERE id_producto = @productos.value('(/productos/producto)[1]/id_producto[1]', 'INT');

        INSERT INTO Ventas(id_comprador, id_vendedor) VALUES(@id_comprador, @id_vendedor);
        SET @id_venta = SCOPE_IDENTITY();

        DECLARE @id_producto INT, @cantidad INT, @precio DECIMAL(10,2);
        DECLARE productos_cursor CURSOR FOR
        SELECT
            Producto.value('(id_producto)[1]', 'INT'),
            Producto.value('(cantidad)[1]', 'INT'),
            Producto.value('(precio)[1]', 'DECIMAL(10,2)')
        FROM @productos.nodes('/productos/producto') AS X(Producto);

        OPEN productos_cursor;
        FETCH NEXT FROM productos_cursor INTO @id_producto, @cantidad, @precio;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF EXISTS (SELECT 1 FROM Productos WHERE id_producto = @id_producto AND stock >= @cantidad)
            BEGIN
                INSERT INTO DetalleVenta(id_venta, id_producto, cantidad, precio_unitario)
                VALUES (@id_venta, @id_producto, @cantidad, @precio);
                UPDATE Productos SET stock = stock - @cantidad WHERE id_producto = @id_producto;
            END
            ELSE
            BEGIN
                RAISERROR('Stock insuficiente para el producto.', 16, 1);
                ROLLBACK;
                RETURN;
            END
            FETCH NEXT FROM productos_cursor INTO @id_producto, @cantidad, @precio;
        END
        CLOSE productos_cursor;
        DEALLOCATE productos_cursor;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END;

EXEC sp_realizar_venta
    @id_comprador = 1, 
    @productos = '<productos>
                    <producto>
                        <id_producto>1</id_producto>
                        <cantidad>2</cantidad>
                        <precio>100.00</precio>
                        <id_vendedor>5</id_vendedor>
                    </producto>
                    <producto>
                        <id_producto>3</id_producto>
                        <cantidad>1</cantidad>
                        <precio>250.00</precio>
                        <id_vendedor>2</id_vendedor>
                    </producto>
                 </productos>';

select * from Usuarios
select * from Productos
select * from Ventas
select* from DetalleVenta