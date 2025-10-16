namespace MVC.Models
{
    public class FiltroEnvioPorFecha
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string? Estado { get; set; }
    }
}
