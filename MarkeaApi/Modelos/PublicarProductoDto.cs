using Microsoft.AspNetCore.Http;

public class PublicarProductoDto
{
    public int IdVendedor { get; set; }
    public int IdCategoria { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public IFormFile? Imagen { get; set; } 
}