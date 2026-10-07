namespace Evaluacion3.Models
{
    public class Salida
    {
        public int Id_Salida { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public int Id_Planta { get; set; }
        public string Cod_Camion { get; set; } = string.Empty;
        public int Id_Chofer { get; set; }
        public int Id_Estado_Salida { get; set; }
        public string Cod_Usuario { get; set; } = string.Empty;
    }
}