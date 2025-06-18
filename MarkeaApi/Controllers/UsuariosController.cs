
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly string cadenaConexion = "Server=MSI\\MSSQLSERVER01;Database=MARKEA;Integrated Security=True;TrustServerCertificate=True;";
    private readonly ServiciosUsuarios _serviciosUsuarios;

    public UsuariosController(ServiciosUsuarios serviciosUsuarios)
    {
        _serviciosUsuarios = serviciosUsuarios;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] IniciarSesionDto loginRequest)
    {
        try
        {
            var userSession = await _serviciosUsuarios.Login(loginRequest);
            return Ok(userSession);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    [HttpPost("registrar")]
    public IActionResult RegistrarUsuario([FromBody] RegistroUsuarioDto nuevoUsuario)
    {
        try
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("sp_registrar_usuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@usuario", nuevoUsuario.Nombre);
                    comando.Parameters.AddWithValue("@correo", nuevoUsuario.Correo);
                    comando.Parameters.AddWithValue("@contrasena", nuevoUsuario.Contrasena);
                    comando.Parameters.AddWithValue("@es_vendedor", 1);

                    comando.ExecuteNonQuery();
                }
            }
            return Ok(new { message = "Usuario registrado exitosamente" });
        }
        catch (SqlException ex)
        {
            if (ex.Number == 50000)
            {
                return Conflict(new { message = "El correo electrónico ya está registrado." });
            }

            return StatusCode(500, new { message = "Error en la base de datos.", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
        }
    }
}
