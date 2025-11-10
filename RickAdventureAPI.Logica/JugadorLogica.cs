using Microsoft.EntityFrameworkCore;
using RickAdventureAPI.Data.Entidades;
using System.Threading.Tasks;

namespace RickAdventureAPI.Logica;

public interface IJugadorLogica
{
    Task<Jugador> CrearOBuscarJugador(string nombre);
    Task<List<Jugador>> ObtenerRankingJugadores();
    Task ActualizarProgresoJugador(int id, int puntaje, int nivel);
}

public class JugadorLogica : IJugadorLogica
{
    private readonly RickAdventureGameContext _context;

    public JugadorLogica(RickAdventureGameContext context)
    {
        _context = context;
    }

    public async Task<Jugador> CrearOBuscarJugador(string nombre)
    {
        var jugadorExistente = await _context.Jugadores
            .FirstOrDefaultAsync(j => j.NombreJugador.ToLower() == nombre.ToLower());

        if (jugadorExistente != null)
        {
            return jugadorExistente;
        }

        var nuevoJugador = new Jugador
        {
            NombreJugador = nombre,
            PuntajeTotal = 0,
            NivelMaximoAlcanzado = 1,
            FechaCreacion = DateTime.Now
        };

        _context.Jugadores.Add(nuevoJugador);
        await _context.SaveChangesAsync();

        return nuevoJugador;
    }

    public async Task<List<Jugador>> ObtenerRankingJugadores()
    {
        return await _context.Jugadores
            .OrderByDescending(j => j.PuntajeTotal)
            .ThenBy(j => j.FechaCreacion)
            .Take(10)
            .ToListAsync();
    }

    public async Task ActualizarProgresoJugador(int id, int puntaje, int nivel)
    {
        var jugador = await _context.Jugadores.FindAsync(id);

        if (jugador != null)
        {
            jugador.PuntajeTotal = (jugador.PuntajeTotal ?? 0) + puntaje;

            if (nivel > (jugador.NivelMaximoAlcanzado ?? 0))
            {
                jugador.NivelMaximoAlcanzado = nivel;
            }

            await _context.SaveChangesAsync();
        }
    }
}
