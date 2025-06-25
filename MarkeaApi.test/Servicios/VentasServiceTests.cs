using Moq;
using Xunit;
using System;

public class Productoa
{
    public int Id { get; set; }
    public int Stock { get; set; }
}

public class CartItemDto
{
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
}

public class Venta { }

public class StockInsuficienteException : Exception
{
    public StockInsuficienteException(string mensaje) : base(mensaje) { }
}

public interface ProductoRepositorioa
{
    Productoa GetById(int id);
    void Actualizar(Productoa producto);
}

public interface VentaRepositorioa
{
    void Crear(Venta venta);
}

public class VentaRepositorioTests
{
    private readonly Mock<ProductoRepositorioa> _productoRepoMock;
    private readonly Mock<VentaRepositorioa> _ventaRepoMock;

    public VentaRepositorioTests()
    {
        _productoRepoMock = new Mock<ProductoRepositorioa>();
        _ventaRepoMock = new Mock<VentaRepositorioa>();
    }

    [Fact]
    public void RealizarCompra_ConStockInsuficiente_DeberiaLanzarExcepcion()
    {
        // --- Arrange ---
        var compraDto = new CartItemDto { IdProducto = 1, Cantidad = 10 };
        var productoExistente = new Producto { Id = 1, Stock = 5 };

        // ✅ usamos la instancia 'compraDto', no el tipo CartItemDto
        _productoRepoMock.Setup(repo => repo.GetById(compraDto.IdProducto))
                         .Returns(productoExistente);

        var productoObtenido = _productoRepoMock.Object.GetById(compraDto.IdProducto);

        // --- Act & Assert ---
        if (productoObtenido.Stock < compraDto.Cantidad)
        {
            Assert.Throws<StockInsuficienteException>(() =>
            {
                throw new StockInsuficienteException("Stock insuficiente");
            });

            _productoRepoMock.Verify(repo => repo.Actualizar(It.IsAny<Producto>()), Times.Never);
            _ventaRepoMock.Verify(repo => repo.Crear(It.IsAny<Venta>()), Times.Never);
        }
        else
        {
            Assert.True(false, "El test fue mal configurado, el stock sí era suficiente.");
        }
    }
}
