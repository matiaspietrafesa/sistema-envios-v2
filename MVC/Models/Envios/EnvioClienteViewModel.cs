namespace MVC.Models.Envios
{
    public class EnvioClienteViewModel
    {
        public int Id { get; set; }
        public int NroTracking { get; set; }
        public int Peso { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}
