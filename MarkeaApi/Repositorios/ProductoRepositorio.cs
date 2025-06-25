using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

public class ProductoRepositorio
{
    public IEnumerable<ProductoDto> ObtenerProductosDisponibles()
    {
        var productos = new List<ProductoDto>();
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_obtener_productos_disponibles", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

    public int CrearProducto(PublicarProductoDto producto)
    {
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_publicar_producto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_vendedor", producto.IdVendedor);
                command.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                command.Parameters.AddWithValue("@nombre", producto.Nombre);
                command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@precio", producto.Precio);
                command.Parameters.AddWithValue("@stock", producto.Stock);

                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                throw new System.Exception("No se pudo obtener el ID del nuevo producto desde la base de datos.");
            }
        }
    }

    public IEnumerable<ProductoVentaDto> ObtenerPorVendedor(int idVendedor)
    {
        var productos = new List<ProductoVentaDto>();
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_obtener_productos_por_vendedor", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_vendedor", idVendedor);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new ProductoVentaDto
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["nombre"],
                            Descripcion = (string)reader["descripcion"],
                            Precio = (decimal)reader["precio"],
                            Stock = (int)reader["stock"],
                            Estado = (string)reader["Estado"]
                        });
                    }
                }
            }
        }
        return productos;
    }

    public void ActualizarCompleto(int idProducto, ActualizarProductoDto dto)
    {
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_actualizar_producto_completo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_producto", idProducto);
                command.Parameters.AddWithValue("@nombre", dto.Nombre);
                command.Parameters.AddWithValue("@descripcion", dto.Descripcion);
                command.Parameters.AddWithValue("@precio", dto.Precio);
                command.Parameters.AddWithValue("@stock", dto.Stock);

                command.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int idProducto)
    {
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_eliminar_producto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_producto", idProducto);

                command.ExecuteNonQuery();
            }
        }
    }
}
