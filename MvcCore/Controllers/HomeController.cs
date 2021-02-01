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
using MvcCorePaco.Extensions;
using MvcCorePaco.Helpers;
using MvcCorePaco.Models;

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
        public IActionResult EjemploSession(string accion)
        {
            if (accion == "almacenar")
            {
                Persona person = new Persona();
                person.Nombre = "Paco";
                person.Edad = 40;
                person.Hora = DateTime.Now.ToLongTimeString();

                HttpContext.Session.SetString("Persona", HelperToolkit.SerializeObject(person));

                //HttpContext.Session.SetString("Autor", "Programator");
                //HttpContext.Session.SetString("Hora", DateTime.Now.ToLongTimeString());
                ViewBag.Mensaje = "Datos almacenados en Session a las " + DateTime.Now.ToLongTimeString();
            }
            else if (accion == "mostrar")
            {

                Persona person = HelperToolkit.DeserializeJSONObject<Persona>(HttpContext.Session.GetString("Persona"));

                //ViewBag.Autor = person.Nombre + ", de " + person.Edad + " años";
                //ViewBag.Hora = person.Hora;
                ViewBag.Autor = person.Nombre + ", de " + person.Edad + " años";
                ViewBag.hora = person.Hora;
                ViewBag.Mensaje = "Mostrando datos...";
            }
            return View();
        }
        public IActionResult SessionLista(string accion)
        {
            if (accion == "almacenar")
            {
                List<Persona> personas = new List<Persona>();
                for (int i = 1; i <= 5; i++)
                {
                    Persona person = new Persona();
                    person.Edad = i;
                    person.Nombre = "Persona" + i;
                    person.Hora = DateTime.Now.ToLongTimeString() + " " + i;
                    personas.Add(person);
                }
                HttpContext.Session.SetObject("Lista", personas);
                ViewBag.Mensaje = "Lista almacenada en Session a las " + DateTime.Now.ToLongTimeString();
            }
            else if (accion == "mostrar")
            {
                //List<Persona> personas = HelperToolkit.DeserializeJSONObject(HttpContext.Session.GetString("Lista"), typeof(List<Persona>)) as List<Persona>;
                //var personas = HelperToolkit.DeserializeJSONObject<List<Persona>>(HttpContext.Session.GetString("Lista"));
                ViewBag.Mensaje = "Mostrando datos...";
                return View(HttpContext.Session.GetObject<List<Persona>>("Lista"));
            }
            return View();
        }

    }
}
