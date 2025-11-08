namespace RickAdventureAPI.Web.Models.Responses;

public class JugadorResponse
{
    public int IdJugador { get; set; }
    public string NombreJugador { get; set; } = string.Empty;
    public int PuntajeTotal { get; set; }
    public int NivelMaximoAlcanzado { get; set; }
}
