namespace RickAdventureAPI.Web.Models.Responses;

public class RankingResponse
{
    public int Posicion { get; set; }
    public string NombreJugador { get; set; } = string.Empty;
    public int PuntajeTotal { get; set; }
    public int NivelMaximoAlcanzado { get; set; }
}
