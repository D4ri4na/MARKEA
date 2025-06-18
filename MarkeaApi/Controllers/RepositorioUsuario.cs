using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

public class RepositorioUsuario
{
    private readonly string _connectionString = "Server=MSI\\MSSQLSERVER01;Database=MARKEA;Integrated Security=True;TrustServerCertificate=True;";

    public async Task<SesionUsuarioDto> AuthenticateUserAsync(string correo, string contrasena)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("sp_iniciar_sesion", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@contrasena", contrasena);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
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
