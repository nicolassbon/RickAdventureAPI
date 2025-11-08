using Microsoft.AspNetCore.Mvc;
using RickAdventureAPI.Logica;
using RickAdventureAPI.Web.Models.Request;
using RickAdventureAPI.Web.Models.Responses;

namespace RickAdventureAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartidaController : ControllerBase
{
    private readonly IPartidaLogica _partidaLogica;
    private readonly IJugadorLogica _jugadorLogica;

    public PartidaController(IPartidaLogica partidaLogica, IJugadorLogica jugadorLogica)
    {
        _partidaLogica = partidaLogica;
        _jugadorLogica = jugadorLogica;
    }

    /// Guardar resultado de una partida
    [HttpPost]
    public async Task<ActionResult<PartidaResponse>> GuardarPartida([FromBody] PartidaRequest request)
    {
        if (request.IdJugador <= 0 || request.IdNivel <= 0)
        {
            return BadRequest("El ID del jugador y el ID del nivel son obligatorios.");
        }

        var partida = await _partidaLogica.GuardarPartida(
            request.IdJugador,
            request.IdNivel,
            request.Puntaje,
            request.Completado
        );

        // Obtener datos actualizados del jugador
        var jugadores = await _jugadorLogica.ObtenerRankingJugadores();
        var jugador = jugadores.FirstOrDefault(j => j.IdJugador == request.IdJugador);

        var response = new PartidaResponse
        {
            Exito = true,
            Mensaje = request.Completado 
                ? $"¡Nivel {request.IdNivel} completado! Nivel {request.IdNivel + 1} desbloqueado."
                : $"Partida guardada. Sigue intentando el nivel {request.IdNivel}.",
            NuevoPuntajeTotal = jugador?.PuntajeTotal ?? 0,
            NivelDesbloqueado = jugador?.NivelMaximoAlcanzado ?? 1
        };

        return Ok(response);
    }
}
