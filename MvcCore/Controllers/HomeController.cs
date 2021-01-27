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
            //TENEMOS QUE TRABAJAR A NIVEL DE Byte[]
            //CONVERTIMOS A byte[] EL CONTENIDO DE ENTRADA
            byte[] entrada;
            //EL CIFRADO SE HACE A NIVEL DE byte[] Y DEVUELVE OTRO byte[] DE SALIDA
            byte[] salida;
            //NECESITAMOS UN CONVERSOR PARA TRANSFORMAR DE byte[] A STRING Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS EL OBJETO QUE SE ENCARGA DE CIFRAR
            SHA1Managed sha = new SHA1Managed();
            //HAY QUE CONVERTIR EL CONTENIDO DE ENTRADA A byte[]
            entrada = encoding.GetBytes(contenido);
            //EL OBJETO SHA1Managed TIENE UN METODO PARA DEVOLVER LOS BYTES DE SALIDA REALIZANDO EL CIFRADO
            salida = sha.ComputeHash(entrada);
            string res = encoding.GetString(salida);
            //SOLAMENTE SI PONEMOS EL MISMO contenido TENDRÍAMOS LA MISMA SECUENCIA DE SALIDA

            if (accion.ToLower() == "cifrar")
            {
                ViewBag.Resultado = res;
            }
            else if(accion.ToLower()=="comparar")
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
    }
}
