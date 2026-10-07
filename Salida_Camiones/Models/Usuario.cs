namespace Evaluacion3.Models
{
    public class Usuario
    {
        public string Cod_Usuario { get; set; } = string.Empty;
        public string Nom_Persona { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Id_Estado { get; set; }
    }
}