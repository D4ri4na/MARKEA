
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ProductoService _productoService;
    private readonly ProductoRepositorio _productoRepositorio; 
    private readonly MongoDbService _mongoDbService;

    public ProductosController(ProductoService productoService, ProductoRepositorio productoRepositorio, MongoDbService mongoDbService)
    {
        _productoService = productoService;
        _productoRepositorio = productoRepositorio;
        _mongoDbService = mongoDbService;
    }

    // Endpoint para publicar (ya lo tienes)
    [HttpPost("publicar")]
    public async Task<IActionResult> PublicarProducto([FromForm] PublicarProductoDto productoDto)
    {
        try
        {
            await _productoService.PublicarProductoCompletoAsync(productoDto);
            return Ok(new { message = "Producto publicado con éxito" });
        }
        catch (System.Exception ex)
        {
            // Capturamos cualquier error que pueda ocurrir en el proceso
            return StatusCode(500, new { message = "Error al publicar el producto.", error = ex.Message });
        }
    }

    // --- NUEVOS ENDPOINTS ---

    // GET: /api/Productos
    [HttpGet]
    public async Task<IActionResult> ObtenerProductos()
    {
        var productos = await _productoRepositorio.ObtenerProductosDisponiblesAsync();
        return Ok(productos);
    }

    // GET: /api/Productos/{id}/imagen
    [HttpGet("{id}/imagen")]
    public async Task<IActionResult> ObtenerImagen(int id)
    {
        var imagen = await _mongoDbService.ObtenerImagenAsync(id);
        if (imagen == null)
        {
            // Opcional: devolver una imagen placeholder por defecto
            return NotFound();
        }
        // Devuelve la imagen directamente como un archivo
        return File(imagen.ImagenData, imagen.ContentType);
    }
}