namespace RickAdventureAPI.Web.Models.Request;

public class PartidaRequest
{
    public int IdJugador { get; set; }
    public int IdNivel { get; set; }
    public int Puntaje { get; set; }
    public bool Completado { get; set; }
}
