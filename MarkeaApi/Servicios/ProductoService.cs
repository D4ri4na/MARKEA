using System.Threading.Tasks;

public class ProductoService
{
    private readonly ProductoRepositorio _productoRepositorio;
    private readonly MongoDbService _mongoDbService;

    // Recibe las dependencias que necesita para trabajar
    public ProductoService(ProductoRepositorio productoRepositorio, MongoDbService mongoDbService)
    {
        _productoRepositorio = productoRepositorio;
        _mongoDbService = mongoDbService;
    }

    public async Task PublicarProductoCompletoAsync(PublicarProductoDto productoDto)
    {
        // 1. Guardar datos en SQL Server y obtener el nuevo ID
        int nuevoProductoId = await _productoRepositorio.CrearProductoAsync(productoDto);

        // Si el ID es 0 o menor, algo falló
        if (nuevoProductoId <= 0)
        {
            throw new System.Exception("La creación del producto en SQL Server falló.");
        }

        // 2. Si hay una imagen, guardarla en MongoDB
        if (productoDto.Imagen != null && productoDto.Imagen.Length > 0)
        {
            await _mongoDbService.GuardarImagenProductoAsync(nuevoProductoId, productoDto.Imagen);
        }
    }
}