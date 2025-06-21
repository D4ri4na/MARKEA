using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PerfilController : ControllerBase
{
    // Asumiendo que tienes estos repositorios y están inyectados
    private readonly ProductoRepositorio _productoRepo;
    private readonly VentaRepositorio _ventaRepo;

    public PerfilController(ProductoRepositorio productoRepo, VentaRepositorio ventaRepo)
    {
        _productoRepo = productoRepo;
        _ventaRepo = ventaRepo;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> ObtenerDatosPerfil(int usuarioId)
    {
        var perfilDto = new PerfilDto
        {
            // Deberás implementar estos métodos en tus repositorios
            ProductosEnVenta = await _productoRepo.ObtenerPorVendedorAsync(usuarioId),
            ComprasRecientes = await _ventaRepo.ObtenerPorCompradorAsync(usuarioId)
        };
        return Ok(perfilDto);
    }
}