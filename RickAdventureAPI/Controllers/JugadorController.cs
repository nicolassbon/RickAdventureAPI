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

    /// <summary>
    /// Crear o buscar jugador (inicio de juego)
    /// Si el nombre existe, devuelve el jugador existente.
    /// Si no existe, crea uno nuevo.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<JugadorResponse>> CrearOBuscarJugador([FromBody] JugadorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NombreJugador))
        {
            return BadRequest("El nombre del jugador es obligatorio.");
        }

        var jugador = await _jugadorLogica.CrearOBuscarJugador(request.NombreJugador);

        var response = new JugadorResponse
        {
            IdJugador = jugador.IdJugador,
            NombreJugador = jugador.NombreJugador,
            PuntajeTotal = jugador.PuntajeTotal ?? 0,
            NivelMaximoAlcanzado = jugador.NivelMaximoAlcanzado ?? 1
        };

        return Ok(response);
    }

    /// <summary>
    /// Obtener top 10 jugadores (ranking)
    /// </summary>
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
