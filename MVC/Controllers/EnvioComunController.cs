using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Envios;
using MVC.Models.Usuarios;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class EnvioComunController : Controller
    {
        private string url = "";
        private string urlBase = "/api/";

        public EnvioComunController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("url");
            url = urlBase + "Envio/";
        }

		// GET: EnvioComunController/BuscarPorTracking
		public ActionResult BuscarPorTracking()
		{
			return View();
		}

		// POST: EnvioComunController/BuscarPorTracking
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BuscarPorTracking(EnvioTrackingViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				HttpClient cliente = new HttpClient();
				Task<HttpResponseMessage> tarea = cliente.GetAsync(url + model.codTracking);
				tarea.Wait();

				HttpResponseMessage respuesta = tarea.Result;

				if (!respuesta.IsSuccessStatusCode)
				{
					ViewBag.Mensaje = "No se encontró el envío con ese código";
					return View(model);
				}

				HttpContent contenido = respuesta.Content;
				Task<string> body = contenido.ReadAsStringAsync();
				body.Wait();
				string datos = body.Result;

				var envio = JsonConvert.DeserializeObject<DatoEnvioCompletoViewModel>(datos);
				return View("DetallesEnvio", envio);
			}
			catch (Exception ex)
			{
				ViewBag.Mensaje = $"Error al buscar: {ex.Message}";
				return View(model);
			}
		}

		// GET: EnvioComunController/DetallesEnvio
		public ActionResult DetallesEnvio(DatoEnvioCompletoViewModel model)
		{
			return View(model);
		}

        [HttpGet]
        public IActionResult ListarEnviosDeCliente()
        {
            List<EnvioClienteViewModel> vm = new List<EnvioClienteViewModel>();
            try
            {
                string email = HttpContext.Session.GetString("EmailUsuario");
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tarea = cliente.GetAsync(url + "email" + email);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                HttpContent contenido = respuesta.Content;
                Task<string> body = contenido.ReadAsStringAsync();
                body.Wait();
                string datos = body.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    vm = JsonConvert.DeserializeObject<List<EnvioClienteViewModel>>(datos);
                }
                else
                {
                    ViewBag.Mensaje = datos;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Hubo un error.";
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult ListarComentariosDeEnvio(int id)
        {
            List<ComentarioCompletoViewModel> vm = new List<ComentarioCompletoViewModel>();
            try
            {
                string email = HttpContext.Session.GetString("EmailUsuario");
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tarea = cliente.GetAsync(url + "email/" + email + "envio/" + id);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                HttpContent contenido = respuesta.Content;
                Task<string> body = contenido.ReadAsStringAsync();
                body.Wait();
                string datos = body.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    vm = JsonConvert.DeserializeObject<List<ComentarioCompletoViewModel>>(datos);
                }
                else
                {
                    ViewBag.Mensaje = datos;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Hubo un error.";
            }
            return View(vm);
        }

        public IActionResult FiltrarEnviosPorFecha()
        {
            return View();
        }

        public IActionResult ListarEnviosPorFecha(DateTime desde, DateTime hasta, string? estado)
        {
            List<EnvioClienteViewModel> vm = new List<EnvioClienteViewModel>();
            try
            {
                string email = HttpContext.Session.GetString("EmailUsuario");
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                string desdeReal = desde.ToShortDateString().Replace("/", "-");
                string hastaReal = hasta.ToShortDateString().Replace("/", "-");
                Task<HttpResponseMessage> tarea = cliente.GetAsync(url + "email/" + email + "desde/" + desdeReal + "hasta/" + hastaReal + "estado/" + estado);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                HttpContent contenido = respuesta.Content;
                Task<string> body = contenido.ReadAsStringAsync();
                body.Wait();
                string datos = body.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    vm = JsonConvert.DeserializeObject<List<EnvioClienteViewModel>>(datos);
                }
                else
                {
                    ViewBag.Mensaje = datos;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Hubo un error.";
            }
            return View(vm);
        }

        public IActionResult FiltrarEnviosPorComentario()
        {
            return View();
        }

        public IActionResult ListarEnviosPorComentario(string comentario)
        {
            List<EnvioClienteViewModel> vm = new List<EnvioClienteViewModel>();
            try
            {
                string email = HttpContext.Session.GetString("EmailUsuario");
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                Task<HttpResponseMessage> tarea = cliente.GetAsync(url + "email/" + email + "palabra/" + comentario);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                HttpContent contenido = respuesta.Content;
                Task<string> body = contenido.ReadAsStringAsync();
                body.Wait();
                string datos = body.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    vm = JsonConvert.DeserializeObject<List<EnvioClienteViewModel>>(datos);
                }
                else
                {
                    ViewBag.Mensaje = datos;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Hubo un error.";
            }
            return View(vm);
        }
    }
}
