using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; // Para capturar la excepción específica
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly VentaRepositorio _ventaRepositorio;

    public VentasController(VentaRepositorio ventaRepositorio)
    {
        _ventaRepositorio = ventaRepositorio;
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto checkoutRequest)
    {
        if (checkoutRequest == null || checkoutRequest.Productos.Count == 0)
        {
            return BadRequest(new { message = "La solicitud de checkout está vacía." });
        }

        try
        {
            await _ventaRepositorio.RealizarVentaAsync(checkoutRequest);
            return Ok(new { message = "¡Compra realizada con éxito!" });
        }
        catch (SqlException ex)
        {
            // Capturamos errores específicos de SQL, como el RAISERROR del stock
            return Conflict(new { message = "Error al procesar la venta.", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
        }
    }
}