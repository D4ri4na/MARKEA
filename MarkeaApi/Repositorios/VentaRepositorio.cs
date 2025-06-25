using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using System.Linq;

public class VentaRepositorio : BaseRepository
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

        var xmlParam = new SqlParameter("@productos", productosXml.ToString())
        {
            DbType = DbType.Xml
        };

        var parameters = new[]
        {
            new SqlParameter("@id_comprador", checkoutRequest.IdComprador),
            xmlParam
        };

        ExecuteNonQuery("sp_realizar_venta", parameters);
    }

    public IEnumerable<CompraRecienteDto> ObtenerPorComprador(int idComprador)
    {
        var parameters = new[] { new SqlParameter("@id_comprador", idComprador) };

        return Query("sp_obtener_compras_por_comprador", reader => new CompraRecienteDto
        {
            Id = (int)reader["Id"],
            NombreProducto = (string)reader["NombreProducto"],
            Precio = (decimal)reader["Precio"],
            Fecha = (System.DateTime)reader["fecha"],
            NombreVendedor = (string)reader["NombreVendedor"]
        }, parameters);
    }
}