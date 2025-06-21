using System;
public class CompraRecienteDto
{
    public int Id { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public DateTime Fecha { get; set; }
    public string NombreVendedor { get; set; } = string.Empty;
}