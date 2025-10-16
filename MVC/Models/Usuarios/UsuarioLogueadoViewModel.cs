namespace MVC.Models.Usuarios
{
    public class UsuarioLogueadoViewModel
    {
        public string Token { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email{ get; set; }
        public string Rol { get; set; }
    }
}
