using System.Threading.Tasks;

public class ServiciosUsuarios
{
    private readonly RepositorioUsuario _repositorioUsuario;

    public ServiciosUsuarios(RepositorioUsuario userRepository)
    {
        _repositorioUsuario = userRepository;
    }

    public async Task<SesionUsuarioDto?> Login(IniciarSesionDto loginRequest)
    {

        return await _repositorioUsuario.AuthenticateUserAsync(loginRequest.Correo, loginRequest.Contrasena);
    }
}