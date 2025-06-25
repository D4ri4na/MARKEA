using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using System.Linq;

public class VentaRepositorio
{
    public void RealizarVenta(CheckoutRequestDto checkoutRequest)
    {
        var productosXml = new XElement("productos",
            checkoutRequest.Productos.Select(p =>
                new XElement("producto",
                    new XElement("id_producto", p.IdProducto),
                    new XElement("cantidad", p.Cantidad),
                    new XElement("precio", p.Precio)
                )
            )
        );

        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_realizar_venta", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_comprador", checkoutRequest.IdComprador);

                SqlParameter xmlParam = command.Parameters.AddWithValue("@productos", productosXml.ToString());
                xmlParam.DbType = DbType.Xml;

                command.ExecuteNonQuery();
            }
        }
    }

    public IEnumerable<CompraRecienteDto> ObtenerPorComprador(int idComprador)
    {
        var compras = new List<CompraRecienteDto>();
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_obtener_compras_por_comprador", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_comprador", idComprador);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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
