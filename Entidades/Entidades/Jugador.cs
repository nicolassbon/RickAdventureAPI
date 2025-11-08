using System;
using System.Collections.Generic;

namespace RickAdventureAPI.Data.Entidades;

public partial class Jugador
{
    public int IdJugador { get; set; }

    public string NombreJugador { get; set; } = null!;

    public int? PuntajeTotal { get; set; }

    public int? NivelMaximoAlcanzado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Partida> Partida { get; set; } = new List<Partida>();
}
