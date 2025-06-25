using Microsoft.Data.SqlClient;
using System;
using System.Data;

public class RepositorioUsuario
{
    public SesionUsuarioDto AuthenticateUser(string correo, string contrasena)
    {
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("sp_iniciar_sesion", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@contrasena", contrasena);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("El correo electrónico no está registrado.");
                    }

                    if (reader["id_usuario"] == DBNull.Value)
                    {
                        throw new Exception("La contraseña es incorrecta.");
                    }

                    return new SesionUsuarioDto
                    {
                        IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                        Nombre = reader["nombre"].ToString() ?? string.Empty
                    };
                }
            }
        }
    }
}
