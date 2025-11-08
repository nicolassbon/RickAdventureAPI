using Microsoft.EntityFrameworkCore;
using RickAdventureAPI.Data.Entidades;

namespace RickAdventureAPI.Logica;

public interface IPartidaLogica
{
    Task<Partida> GuardarPartida(int idJugador, int idNivel, int puntaje, bool completado);
}

public class PartidaLogica : IPartidaLogica
{
    private readonly RickAdventureGameContext _context;
    private readonly IJugadorLogica _jugadorLogica;

    public PartidaLogica(RickAdventureGameContext context, IJugadorLogica jugadorLogica)
    {
        _context = context;
        _jugadorLogica = jugadorLogica;
    }

    public async Task<Partida> GuardarPartida(int idJugador, int idNivel, int puntaje, bool completado)
    {
        var nuevaPartida = new Partida
        {
            IdJugador = idJugador,
            IdNivel = idNivel,
            Puntaje = puntaje,
            Completado = completado,
            Fecha = DateTime.Now
        };

        _context.Partidas.Add(nuevaPartida);
        await _context.SaveChangesAsync();

        int nivelADesbloquear = completado ? idNivel + 1 : idNivel;
        await _jugadorLogica.ActualizarProgresoJugador(idJugador, puntaje, nivelADesbloquear);

        return nuevaPartida;
    }
}
