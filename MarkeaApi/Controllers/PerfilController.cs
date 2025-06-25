using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PerfilController : ControllerBase
{
    private readonly ProductoRepositorio _productoRepo;
    private readonly VentaRepositorio _ventaRepo;

    public PerfilController(ProductoRepositorio productoRepo, VentaRepositorio ventaRepo)
    {
        _productoRepo = productoRepo;
        _ventaRepo = ventaRepo;
    }

    [HttpGet("{usuarioId}")]
    public IActionResult ObtenerDatosPerfil(int usuarioId)
    {
        var perfilDto = new PerfilDto
        {
            ProductosEnVenta = _productoRepo.ObtenerPorVendedor(usuarioId),
            ComprasRecientes = _ventaRepo.ObtenerPorComprador(usuarioId)
        };
        return Ok(perfilDto);
    }
}
