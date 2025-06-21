using System.Collections.Generic;
public class PerfilDto
{
    public IEnumerable<ProductoVentaDto> ProductosEnVenta { get; set; } = new List<ProductoVentaDto>();
    public IEnumerable<CompraRecienteDto> ComprasRecientes { get; set; } = new List<CompraRecienteDto>();
}