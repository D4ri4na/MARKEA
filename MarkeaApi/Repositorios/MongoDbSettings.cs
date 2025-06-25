using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using System.IO;

public class MongoDbService
{
    public MongoDbService()
    {
    }

    public class ImagenDto
    {
        public byte[] ImagenData { get; set; } = [];
        public string ContentType { get; set; } = "application/octet-stream";
    }

    public ImagenDto? ObtenerImagen(int productoId)
    {
        var mongoClient = new MongoClient(ConexionMONGO.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(ConexionMONGO.DatabaseName);
        var collection = mongoDatabase.GetCollection<BsonDocument>(ConexionMONGO.CollectionName);

        var filter = Builders<BsonDocument>.Filter.Eq("ProductoId_SQL", productoId);
        var document = collection.Find(filter).FirstOrDefault();

        if (document != null && document.Contains("ImagenData") && document.Contains("ContentType"))
        {
            return new ImagenDto
            {
                ImagenData = document["ImagenData"].AsBsonBinaryData.Bytes,
                ContentType = document["ContentType"].AsString
            };
        }

        return null;
    }

    public void GuardarImagenProducto(int productoId, IFormFile imagen)
    {
        var mongoClient = new MongoClient(ConexionMONGO.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(ConexionMONGO.DatabaseName);
        var imagenesCollection = mongoDatabase.GetCollection<BsonDocument>(ConexionMONGO.CollectionName);

        byte[] imagenBytes;
        using (var memoryStream = new MemoryStream())
        {
            imagen.CopyTo(memoryStream); 
            imagenBytes = memoryStream.ToArray();
        }

        var filter = Builders<BsonDocument>.Filter.Eq("ProductoId_SQL", productoId);

        var update = Builders<BsonDocument>.Update
            .Set("ImagenData", new BsonBinaryData(imagenBytes, BsonBinarySubType.Binary))
            .Set("ContentType", imagen.ContentType)
            .SetOnInsert("ProductoId_SQL", productoId);

        imagenesCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
    }
}
