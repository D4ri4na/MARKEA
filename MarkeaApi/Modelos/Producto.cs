using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Producto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nombre { get; set; } = null!;
    public decimal Precio { get; set; }
    public decimal Stock { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string VendedorId { get; set; } = null!;
}