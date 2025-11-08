namespace RickAdventureAPI.Web.Models.Responses;

public class PartidaResponse
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int NuevoPuntajeTotal { get; set; }
    public int NivelDesbloqueado { get; set; }
}
