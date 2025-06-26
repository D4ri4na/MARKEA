using Microsoft.Data.SqlClient;

using System.Collections.Generic;

public class FavoritoRepositorio : BaseRepository
{
    public void Agregar(FavoritoDto favorito)
    {
        var parameters = new[]
        {
            new SqlParameter("@id_usuario", favorito.IdUsuario),
            new SqlParameter("@id_producto", favorito.IdProducto)
        };
        ExecuteNonQuery("sp_agregar_favorito", parameters);
    }

    public void Eliminar(FavoritoDto favorito)
    {
        var parameters = new[]
        {
            new SqlParameter("@id_usuario", favorito.IdUsuario),
            new SqlParameter("@id_producto", favorito.IdProducto)
        };
        ExecuteNonQuery("sp_eliminar_favorito", parameters);
    }

    public IEnumerable<int> ObtenerPorUsuario(int idUsuario)
    {
        var parameters = new[] { new SqlParameter("@id_usuario", idUsuario) };

        return Query("sp_obtener_favoritos_por_usuario", reader => reader.GetInt32(0), parameters);
    }
}