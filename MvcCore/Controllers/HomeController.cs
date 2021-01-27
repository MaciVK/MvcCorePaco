using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCorePaco.Helpers;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        PathProvider pathprovider;
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
