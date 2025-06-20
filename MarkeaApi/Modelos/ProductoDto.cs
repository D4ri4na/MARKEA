public class ProductoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Mantenemos el nombre para mostrarlo
    public int IdCategoria { get; set; } // AÑADIMOS EL ID para filtrar
}