using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;


using MvcCorePaco.Helpers;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        PathProvider PathProvider;
        FileUploadService UploadService;
        MailService MailService;
        public HomeController(PathProvider pathprovider, FileUploadService uploadservice, MailService mailservice)
        {
            this.PathProvider = pathprovider;
            this.UploadService = uploadservice;
            this.MailService = mailservice;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult SubirFichero()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> SubirFichero(IFormFile fichero)
        //{
        //    //EN LOS async HAY QUE DEVOLVER UN Task<IActionResult>, NO IActionResult
        //    //1-File Name
        //    string filename = fichero.FileName;
        //    //2-Ruta
        //    string Path = PathProvider.MapPath(filename, Folders.Images);
        //    using (var stream = new FileStream(Path, FileMode.Create))
        //    {
        //        await fichero.CopyToAsync(stream);
        //    }
        //    ViewBag.Mensaje = "Has subido el archivo! " + Path;

        //    return View();
        //}
        public IActionResult EjemploMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EjemploMail(string receptor, string asunto, string mensaje, IFormFile fichero)
        {

            if (fichero != null)
            {
                string path = await this.UploadService.UploadFileAsync(fichero, Folders.Temp);

                this.MailService.SendMail(receptor, asunto, mensaje, path);
            }
            else
            {
                this.MailService.SendMail(receptor, asunto, mensaje);
            }
            ViewBag.Mensaje = "Mensaje Enviado";
            return View();

        }
        public IActionResult CifradoHash()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoHash(string contenido, string resultado, string accion)
        {
            string res = CypherService.CifradoSHA1(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewBag.Resultado = res;
            }
            else if (accion.ToLower() == "comparar")
            {
                //EN ESTE CASO COMPARAMOS LA CAJA resultado CON EL DATO YA CIFRADO DE NUEVO (res)
                if (resultado != res)
                {
                    ViewBag.Mensaje = "<h1 class='text-danger'>NO COINCIDE</h1>";
                }
                else
                {
                    ViewBag.Mensaje = "<h1 class='text-success'>Coincide!!</h1>";
                }

            }

            return View();
        }
        public IActionResult CifradoHashEficiente()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CifradoHashEficiente(string contenido, int iteraciones, string salt, string resultado, string action)
        {
            String cifrado = CypherService.CifradoSaltSHA256(contenido, iteraciones, salt);
            if (action.ToLower() == "cifrar")
            {
                ViewBag.Resultado = cifrado;
            }
            else if (action.ToLower() == "comparar")
            {
                if (resultado == cifrado)
                {
;
                    ViewBag.Mensaje = "<h1 class='text-success'>Son iguales!</h1>";
                }
                else
                {
                    ViewBag.Mensaje = "<h1 class='text-danger'>NO COINCIDEN!</h1>";
                }
            }
            return View();
        }
    }
}
