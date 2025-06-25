using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; 
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
    public IActionResult Checkout([FromBody] CheckoutRequestDto checkoutRequest)
    {
        if (checkoutRequest == null || checkoutRequest.Productos.Count == 0)
        {
            return BadRequest(new { message = "La solicitud de checkout está vacía." });
        }

        try
        {
             _ventaRepositorio.RealizarVenta(checkoutRequest);
            return Ok(new { message = "¡Compra realizada con éxito!" });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "Error al procesar la venta.", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
        }
    }
}