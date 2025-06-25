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

    [HttpPost("publicar")]
    public IActionResult PublicarProducto([FromForm] PublicarProductoDto productoDto)
    {
        try
        {
             _productoService.PublicarProductoCompleto(productoDto);
            return Ok(new { message = "Producto publicado con éxito" });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { message = "Error al publicar el producto.", error = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult ObtenerProductos()
    {
        var productos =  _productoRepositorio.ObtenerProductosDisponibles();
        return Ok(productos);
    }

    [HttpGet("{id}/imagen")]
    public IActionResult ObtenerImagen(int id)
    {
        var imagen =  _mongoDbService.ObtenerImagen(id);
        if (imagen == null)
        {
            return NotFound();
        }
        return File(imagen.ImagenData, imagen.ContentType);
    }

    [HttpPut("{id}")]
    public IActionResult ActualizarProducto(int id, [FromBody] ActualizarProductoDto dto)
    {
        _productoRepositorio.ActualizarCompleto(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarProducto(int id)
    {
         _productoRepositorio.Eliminar(id);
        return NoContent();
    }

}