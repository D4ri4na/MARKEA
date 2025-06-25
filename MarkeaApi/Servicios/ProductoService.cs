public class ProductoService
{
    private readonly ProductoRepositorio _productoRepositorio;
    private readonly MongoDbService _mongoDbService;

    public ProductoService(ProductoRepositorio productoRepositorio, MongoDbService mongoDbService)
    {
        _productoRepositorio = productoRepositorio;
        _mongoDbService = mongoDbService;
    }

    public void PublicarProductoCompleto(PublicarProductoDto productoDto)
    {
        int nuevoProductoId = _productoRepositorio.CrearProducto(productoDto);

        if (nuevoProductoId <= 0)
        {
            throw new System.Exception("La creación del producto en SQL Server falló.");
        }

        if (productoDto.Imagen != null && productoDto.Imagen.Length > 0)
        {
            _mongoDbService.GuardarImagenProducto(nuevoProductoId, productoDto.Imagen);
        }
    }
}
