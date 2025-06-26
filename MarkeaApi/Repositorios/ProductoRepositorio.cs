using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;

public class ProductoRepositorio : BaseRepository
{
    public IEnumerable<ProductoDto> ObtenerProductosDisponibles()
    {
        return Query("sp_obtener_productos_disponibles", reader => new ProductoDto
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Category = reader.GetString(reader.GetOrdinal("Category")),
            IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoria"))
        });
    }

    public int CrearProducto(PublicarProductoDto producto)
    {
        var parameters = new[]
        {
            new SqlParameter("@id_vendedor", producto.IdVendedor),
            new SqlParameter("@id_categoria", producto.IdCategoria),
            new SqlParameter("@nombre", producto.Nombre),
            new SqlParameter("@descripcion", producto.Descripcion),
            new SqlParameter("@precio", producto.Precio),
            new SqlParameter("@stock", producto.Stock)
        };

        var result = ExecuteScalar("sp_publicar_producto", parameters);

        if (result != null && result != DBNull.Value)
        {
            return Convert.ToInt32(result); 
        }

        throw new Exception("No se pudo obtener el ID del nuevo producto desde la base de datos.");
    }

    public IEnumerable<ProductoVentaDto> ObtenerPorVendedor(int idVendedor)
    {
        var parameters = new[] { new SqlParameter("@id_vendedor", idVendedor) };

        return Query("sp_obtener_productos_por_vendedor", reader => new ProductoVentaDto
        {
            Id = (int)reader["Id"],
            Nombre = (string)reader["nombre"],
            Descripcion = (string)reader["descripcion"],
            Precio = (decimal)reader["precio"],
            Stock = (int)reader["stock"],
            Estado = (string)reader["Estado"]
        }, parameters);
    }

    public void ActualizarCompleto(int idProducto, ActualizarProductoDto dto)
    {
        var parameters = new[]
        {
            new SqlParameter("@id_producto", idProducto),
            new SqlParameter("@nombre", dto.Nombre),
            new SqlParameter("@descripcion", dto.Descripcion),
            new SqlParameter("@precio", dto.Precio),
            new SqlParameter("@stock", dto.Stock)
        };

        ExecuteNonQuery("sp_actualizar_producto_completo", parameters);
    }

    public void Eliminar(int idProducto)
    {
        var parameters = new[] { new SqlParameter("@id_producto", idProducto) };
        ExecuteNonQuery("sp_eliminar_producto", parameters);
    }
}