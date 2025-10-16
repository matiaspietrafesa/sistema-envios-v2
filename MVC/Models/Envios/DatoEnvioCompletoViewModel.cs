namespace MVC.Models.Envios
{
    public class DatoEnvioCompletoViewModel
    {
        public int Id { get; set; }
        public int NroTracking { get; set; }
        public string EmpleadoId { get; set; }
        public string ClienteId { get; set; }
        public int Peso { get; set; }

        public string Estado { get; set; }

        public IEnumerable<ComentarioCompletoViewModel> Comentarios { get; set; }

    }
}
