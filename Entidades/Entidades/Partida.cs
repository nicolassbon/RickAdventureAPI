using System;
using System.Collections.Generic;

namespace RickAdventureAPI.Data.Entidades;

public partial class Partida
{
    public int IdPartida { get; set; }

    public int IdJugador { get; set; }

    public int IdNivel { get; set; }

    public int Puntaje { get; set; }

    public bool? Completado { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Jugador IdJugadorNavigation { get; set; } = null!;

    public virtual Nivel IdNivelNavigation { get; set; } = null!;
}
