using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

public class ProductoRepositorio
{
    private readonly string _connectionString = "Server=MSI\\MSSQLSERVER01;Database=MARKEA;Integrated Security=True;TrustServerCertificate=True;";
    // En ProductoRepositorio.cs

    public async Task<IEnumerable<ProductoDto>> ObtenerProductosDisponiblesAsync()
    {
        var productos = new List<ProductoDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("sp_obtener_productos_disponibles", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productos.Add(new ProductoDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoria"))
                        });
                    }
                }
            }
        }
        return productos;
    }
    public async Task<int> CrearProductoAsync(PublicarProductoDto producto)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("sp_publicar_producto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_vendedor", producto.IdVendedor);
                command.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                command.Parameters.AddWithValue("@nombre", producto.Nombre);
                command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@precio", producto.Precio);
                command.Parameters.AddWithValue("@stock", producto.Stock);

               
                var result = await command.ExecuteScalarAsync();
                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                throw new System.Exception("No se pudo obtener el ID del nuevo producto desde la base de datos.");
            }
        }
    }
}