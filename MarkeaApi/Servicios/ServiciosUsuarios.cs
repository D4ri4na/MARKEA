using System.Threading.Tasks;

public class ServiciosUsuarios
{
    private readonly RepositorioUsuario _repositorioUsuario;

    public ServiciosUsuarios(RepositorioUsuario userRepository)
    {
        _repositorioUsuario = userRepository;
    }

    public SesionUsuarioDto? Login(IniciarSesionDto loginRequest)
    {

        return _repositorioUsuario.AuthenticateUser(loginRequest.Correo, loginRequest.Contrasena);
    }
}