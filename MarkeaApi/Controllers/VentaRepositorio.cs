using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq; // Necesario para crear XML
using System.Linq; // Necesario para Select de LINQ

public class VentaRepositorio
{
    private readonly string _connectionString = "Server=MSI\\MSSQLSERVER01;Database=MARKEA;Integrated Security=True;TrustServerCertificate=True;";

    public async Task RealizarVentaAsync(CheckoutRequestDto checkoutRequest)
    {
        // --- Construcción del XML ---
        var productosXml = new XElement("productos",
            checkoutRequest.Productos.Select(p =>
                new XElement("producto",
                    new XElement("id_producto", p.IdProducto),
                    new XElement("cantidad", p.Cantidad),
                    new XElement("precio", p.Precio)
                )
            )
        );

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("sp_realizar_venta", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Añadir parámetros
                command.Parameters.AddWithValue("@id_comprador", checkoutRequest.IdComprador);

                // Añadir el parámetro XML
                SqlParameter xmlParam = command.Parameters.AddWithValue("@productos", productosXml.ToString());
                xmlParam.DbType = DbType.Xml;

                // Ejecutamos el SP. Si hay un error (ej. stock insuficiente), lanzará una SqlException.
                await command.ExecuteNonQueryAsync();
            }
        }
    }
    public async Task<IEnumerable<CompraRecienteDto>> ObtenerPorCompradorAsync(int idComprador)
    {
        var compras = new List<CompraRecienteDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("sp_obtener_compras_por_comprador", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_comprador", idComprador);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        compras.Add(new CompraRecienteDto
                        {
                            Id = (int)reader["Id"],
                            NombreProducto = (string)reader["NombreProducto"],
                            Precio = (decimal)reader["Precio"],
                            Fecha = (System.DateTime)reader["fecha"],
                            NombreVendedor = (string)reader["NombreVendedor"]
                        });
                    }
                }
            }
        }
        return compras;
    }
}