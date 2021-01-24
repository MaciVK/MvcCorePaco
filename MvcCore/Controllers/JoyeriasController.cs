using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCorePaco.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Controllers
{
    public class JoyeriasController : Controller
    {
        RepositoryJoyerias repo;
        public JoyeriasController(RepositoryJoyerias repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            //string filename = "joyerias.xml";
            ////CUANDO SE HABLA DE RUTAS EN EL MARAVILLOSO MUNDO DE GÜINDOUS
            ////PENSAMOS EN LA TIPICA BARRA (C:\\...)
            ////PERO NO TODOS LOS S.O. USAN ESA BARRA, Y EN CORE, PODEMOS INSTALAR LAS APPS
            ////EN OTROS SISTEMAS QUE NO SON WINDOWS
            ////HAY UNA CLASE LLAMADA PATH DENTRO DE System.IO QUE SOLUCIONA LAS RUTAS 
            ////EN DIFERENTES PLATAFORMAS
            //string path = Path.Combine(this.environment.WebRootPath, "documents", filename);
            ////PARA PODER HACER CONSULTAS LinQtoXML SE USA EL OBJETO XDocument APUNTANDO A UNA RUTA
            ////O A UN STRING QUE LEAMOS DE ALGUN SITIO
            //XDocument docxml = XDocument.Load(path);
            /**************
            //Extraccion manual:
            List<Joyeria> joyerias = new List<Joyeria>();
            //PARA LEER UN DOC XML HAY QUE ACCEDER A LOS TAGS QUE NOS INTERESAN CON EL METODO 
            //.Descendants !!!!!ES CASE SENSITIVE!!!!
            var consulta = from datos in docxml.Descendants("joyeria")
                           select datos;
            foreach(var dato in consulta)
            {
                Joyeria j = new Joyeria();
                j.Nombre = dato.Element("nombrejoyeria").Value;
                j.Nombre = dato.Element("direccion").Value;
                j.Telefono = dato.Element("telf").Value;
                j.CIF = dato.Attribute("cif").Value;
            }
            *********/
            /*EXTRACCION CON METODOS DE LINQ*/
            //var consulta = from datos in docxml.Descendants("joyeria")
            //               select new Joyeria
            //               { Nombre = datos.Element("nombrejoyeria").Value,
            //                   Telefono = datos.Element("telf").Value,
            //                   Direccion = datos.Element("direccion").Value,
            //                   CIF=datos.Attribute("cif").Value
            //               };
            List<Joyeria> joyerias = repo.GetJoyerias();
            return View(joyerias);
        }
    }
}
