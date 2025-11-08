using Microsoft.AspNetCore.Mvc;
using RickAdventureAPI.Logica;
using RickAdventureAPI.Web.Models.Request;
using RickAdventureAPI.Web.Models.Responses;

namespace RickAdventureAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JugadorController : ControllerBase
{
    private readonly IJugadorLogica _jugadorLogica;

    public JugadorController(IJugadorLogica jugadorLogica)
    {
        _jugadorLogica = jugadorLogica;
    }

    /// Crear nuevo jugador (inicio de juego)
    [HttpPost]
    public async Task<ActionResult<JugadorResponse>> CrearJugador([FromBody] JugadorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NombreJugador))
        {
            return BadRequest("El nombre del jugador es obligatorio.");
        }

        var nuevoJugador = await _jugadorLogica.CrearJugador(request.NombreJugador);
        
        var response = new JugadorResponse
        {
            IdJugador = nuevoJugador.IdJugador,
            NombreJugador = nuevoJugador.NombreJugador,
            PuntajeTotal = nuevoJugador.PuntajeTotal ?? 0,
            NivelMaximoAlcanzado = nuevoJugador.NivelMaximoAlcanzado ?? 1
        };

        return Ok(response);
    }

    /// Obtener top 10 jugadores (ranking)
    [HttpGet("ranking")]
    public async Task<ActionResult<IEnumerable<RankingResponse>>> ObtenerRankingJugadores()
    {
        var jugadores = await _jugadorLogica.ObtenerRankingJugadores();
        
        var ranking = jugadores.Select((jugador, index) => new RankingResponse
        {
            Posicion = index + 1,
            NombreJugador = jugador.NombreJugador,
            PuntajeTotal = jugador.PuntajeTotal ?? 0,
            NivelMaximoAlcanzado = jugador.NivelMaximoAlcanzado ?? 1
        }).ToList();

        return Ok(ranking);
    }
}
