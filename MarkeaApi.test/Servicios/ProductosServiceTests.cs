using Moq;
using Xunit;
using FluentAssertions;

// Simulaciones necesarias para compilar el test:
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}

public class PublicarProductoDto
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}

public interface ProductoRepositorio
{
    Producto CrearProducto(Producto producto);
}

public class MongoDbService { }

public class ProductoService
{
    private readonly ProductoRepositorio _repositorio;
    private readonly MongoDbService _mongoDbService;

    public ProductoService(ProductoRepositorio repositorio, MongoDbService mongoDbService)
    {
        _repositorio = repositorio;
        _mongoDbService = mongoDbService;
    }

    public Producto PublicarProducto(PublicarProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            Stock = dto.Stock
        };

        return _repositorio.CrearProducto(producto);
    }
}

public class ProductosServiceTests
{
    private readonly Mock<ProductoRepositorio> _productoRepoMock;
    private readonly Mock<MongoDbService> _mongoDbServiceMock;
    private readonly ProductoService _sut;

    public ProductosServiceTests()
    {
        _productoRepoMock = new Mock<ProductoRepositorio>();
        _mongoDbServiceMock = new Mock<MongoDbService>();
        _sut = new ProductoService(_productoRepoMock.Object, _mongoDbServiceMock.Object);
    }

    [Fact]
    public void PublicarProducto_ConDatosValidos_DeberiaCrearYDevolverProducto()
    {
        // Arrange
        var dto = new PublicarProductoDto
        {
            Nombre = "Laptop Gamer",
            Precio = 1500.50m,
            Stock = 10
        };

        _productoRepoMock.Setup(repo => repo.CrearProducto(It.IsAny<Producto>()))
            .Returns((Producto p) =>
            {
                p.Id = 1;
                return p;
            });

        // Act
        var resultado = _sut.PublicarProducto(dto);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(1);
        resultado.Nombre.Should().Be(dto.Nombre);

        _productoRepoMock.Verify(repo => repo.CrearProducto(It.IsAny<Producto>()), Times.Once);
    }
}
