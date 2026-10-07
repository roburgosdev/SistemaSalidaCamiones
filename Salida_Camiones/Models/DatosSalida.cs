namespace Evaluacion3.Models;

public class DatosSalida
{
    public int id_salida { get; set; }
    public DateTime fecha_salida { get; set; }
    public string nom_planta { get; set; } = string.Empty;
    public string cod_camion { get; set; } = string.Empty;
    public string nom_chofer { get; set; } = string.Empty;
    public string nom_estado_salida { get; set; } = string.Empty;
    public string nom_persona { get; set; } = string.Empty;
    public int id_estado_salida { get; set; }
}