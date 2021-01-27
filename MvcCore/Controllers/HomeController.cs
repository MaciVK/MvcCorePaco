using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;


using MvcCorePaco.Helpers;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        PathProvider pathprovider;

        IConfiguration Configuration;
        public HomeController(PathProvider pathprovider, IConfiguration configuration)
        {
            this.pathprovider = pathprovider;
            this.Configuration = configuration;

        public HomeController(PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;

        }
        public IActionResult Index()
        {
            return RedirectToAction("SubirFichero");
        }
        public IActionResult SubirFichero()
        {
            return View();
        }

        public IActionResult EjemploMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EjemploMail(string receptor, string asunto, string mensaje, IFormFile fichero)
        {
            MailMessage mail = new MailMessage();
            string usermail = this.Configuration["UsuarioMail"];
            string password = this.Configuration["PasswordMail"];

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            //EN LOS async HAY QUE DEVOLVER UN Task<IActionResult>, NO IActionResult
            //1-File Name
            string filename = fichero.FileName;
            //2-Ruta
            string Path = pathprovider.MapPath(filename, Folders.Images);
            using (var stream = new FileStream(Path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewBag.Mensaje = "Has subido el archivo! " + Path;

            return View();
        }




    }
}
