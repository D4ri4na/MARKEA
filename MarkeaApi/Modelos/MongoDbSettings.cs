using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public class MongoDbService
{
    private readonly string _connectionString = "mongodb://localhost:27017";
    private readonly string _databaseName = "markea";
    private readonly string _collectionName = "productos";

    // Puedes dejar un constructor vacío si no necesitas inyectar IConfiguration
    public MongoDbService()
    {
    }
    
    public class ImagenDto
    {
        public byte[] ImagenData { get; set; } = [];
        public string ContentType { get; set; } = "application/octet-stream";
    }

    public async Task<ImagenDto?> ObtenerImagenAsync(int productoId)
    {
        var mongoClient = new MongoClient(_connectionString);
        var mongoDatabase = mongoClient.GetDatabase(_databaseName);
        var collection = mongoDatabase.GetCollection<BsonDocument>(_collectionName);

        var filter = Builders<BsonDocument>.Filter.Eq("ProductoId_SQL", productoId);
        var document = await collection.Find(filter).FirstOrDefaultAsync();

        if (document != null && document.Contains("ImagenData") && document.Contains("ContentType"))
        {
            return new ImagenDto
            {
                ImagenData = document["ImagenData"].AsBsonBinaryData.Bytes,
                ContentType = document["ContentType"].AsString
            };
        }

        return null; // Devuelve null si no se encuentra la imagen
    }
    public async Task GuardarImagenProductoAsync(int productoId, IFormFile imagen)
    {
        // 1. Conexión directa a MongoDB
        var mongoClient = new MongoClient(_connectionString);
        var mongoDatabase = mongoClient.GetDatabase(_databaseName);
        var imagenesCollection = mongoDatabase.GetCollection<BsonDocument>(_collectionName);

        // 2. Convierte la imagen (IFormFile) a un array de bytes
        byte[] imagenBytes;
        using (var memoryStream = new MemoryStream())
        {
            await imagen.CopyToAsync(memoryStream);
            imagenBytes = memoryStream.ToArray();
        }

        // 3. Define un filtro para encontrar el documento por el ID del producto de SQL
        var filter = Builders<BsonDocument>.Filter.Eq("ProductoId_SQL", productoId);

        // 4. Define la actualización para añadir o reemplazar la imagen
        var update = Builders<BsonDocument>.Update
            .Set("ImagenData", new BsonBinaryData(imagenBytes, BsonBinarySubType.Binary))
            .Set("ContentType", imagen.ContentType)
            .SetOnInsert("ProductoId_SQL", productoId); // Si el documento no existe, crea el ID

        // 5. Usa "UpdateOneAsync" con la opción "IsUpsert = true".
        // Esto buscará un documento con el ProductoId.
        // - Si lo encuentra, le AÑADE/ACTUALIZA los campos de la imagen.
        // - Si NO lo encuentra, CREA un nuevo documento con el ProductoId y la imagen.
        // Esta es la forma más robusta de manejarlo.
        await imagenesCollection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }
}