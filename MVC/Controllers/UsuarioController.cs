using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Usuarios;
using Newtonsoft.Json;
using System.Security.Policy;

namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {
        string url = "";

        public UsuarioController(IConfiguration configuration)
        {
            url = configuration.GetValue<string>("url") + "Usuario";
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUsuarioViewModel usuarioVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    Task<HttpResponseMessage> tarea = client.PostAsJsonAsync(url+"/Login", usuarioVM);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        UsuarioLogueadoViewModel usuLogueadoVM = JsonConvert.DeserializeObject<UsuarioLogueadoViewModel>(datos);
                        HttpContext.Session.SetString("Token", usuLogueadoVM.Token);
                        HttpContext.Session.SetString("Rol", usuLogueadoVM.Rol);
                        HttpContext.Session.SetString("NombreUsuario", usuLogueadoVM.Nombre);
                        HttpContext.Session.SetString("ApellidoUsuario", usuLogueadoVM.Apellido);
                        HttpContext.Session.SetString("EmailUsuario", usuLogueadoVM.Email);
                        return RedirectToAction("ListarEnviosDeCliente", "/EnvioComun");
                    }
                    else
                    {
                        ViewBag.Mensaje = datos;
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Hubo un error.";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CambiarPassword()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CambiarPassword(CambioPasswordViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vm.Email = HttpContext.Session.GetString("EmailUsuario");
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    Task<HttpResponseMessage> tarea = cliente.PutAsJsonAsync(url + "/CambiarPassword", vm);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;
                    HttpContent contenido = respuesta.Content;
                    Task<string> body = contenido.ReadAsStringAsync();
                    body.Wait();
                    string datos = body.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Contraseña cambiada";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Mensaje = datos;
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (Exception)
            {
                ViewBag.MensajeError = "Error al procesar la solicitud";
            }

            return View();
        }
    }
}
