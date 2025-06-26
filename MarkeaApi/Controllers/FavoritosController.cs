using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class FavoritosController : ControllerBase
{
    private readonly FavoritoRepositorio _favoritoRepo;

    public FavoritosController(FavoritoRepositorio favoritoRepo)
    {
        _favoritoRepo = favoritoRepo;
    }

    [HttpGet("{idUsuario}")]
    public IActionResult ObtenerFavoritos(int idUsuario)
    {
        return Ok(_favoritoRepo.ObtenerPorUsuario(idUsuario));
    }

    [HttpPost]
    public IActionResult AgregarFavorito([FromBody] FavoritoDto favorito)
    {
        _favoritoRepo.Agregar(favorito);
        return Ok();
    }

    [HttpDelete]
    public IActionResult EliminarFavorito([FromBody] FavoritoDto favorito)
    {
        _favoritoRepo.Eliminar(favorito);
        return Ok();
    }
}