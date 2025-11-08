using System;
using System.Collections.Generic;

namespace RickAdventureAPI.Data.Entidades;

public partial class Nivel
{
    public int IdNivel { get; set; }

    public string Nombre { get; set; } = null!;

    public string Dificultad { get; set; } = null!;

    public virtual ICollection<Partida> Partida { get; set; } = new List<Partida>();
}
