using Microsoft.AspNetCore.Mvc;
using MvcCorePaco.Extensions;
using MvcCorePaco.Models;
using MvcCorePaco.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MvcCorePaco.Controllers
{
    public class EmpleadosSessionController : Controller
    {
        IRepositoryHospital repo;
        public EmpleadosSessionController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult AlmacenarEmpleados(int? idempleado)
        {
            if (idempleado != null)
            {
                List<int> sessionemp;
                //Si existe la session, recuperamos la lista
                //Si no existe, la creamos
                if (HttpContext.Session.GetObject<List<int>>("EMPLEADOS") == null)
                {
                    sessionemp = new List<int>();

                }
                else
                {
                    sessionemp = HttpContext.Session.GetObject<List<int>>("EMPLEADOS");
                }
                //if (sessionemp.Contains(idempleado.Value) == false)
                //{
                sessionemp.Add(idempleado.GetValueOrDefault());
                HttpContext.Session.SetObject("EMPLEADOS", sessionemp);
                //}
            }
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }
        public IActionResult MostrarEmpleados(int? eliminar)
        {
            List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("EMPLEADOS");
            if (sessionemp == null)
            {
                return View();

            }
            else
            {
                if (eliminar != null)
                {
                    sessionemp.Remove(eliminar.Value);
                    HttpContext.Session.SetObject("EMPLEADOS", sessionemp);

                }
                return View(this.repo.GetEmpleadosSession(sessionemp));
            }
        }
        [HttpPost]
        public IActionResult MostrarEmpleados(List<int> cantidades)
        {
            List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("EMPLEADOS");
            List<Empleado> empleados = this.repo.GetEmpleadosSession(sessionemp);
            TempData["EMPLEADOS"] = empleados;
            TempData["CANTIDADES"] = cantidades;
            return RedirectToAction("Pedidos");

        }

        public IActionResult Pedidos()
        {
            ViewBag.cantidades = TempData["CANTIDADES"];
            return View((List<Empleado>)TempData["EMPLEADOS"]);

        }
        //[HttpPost]
        //public IActionResult Pedidos(List<int> cantidades)
        //{

        //    ViewBag.cantidades = cantidades; ;
        //    List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("EMPLEADOS");
        //    List<Empleado> empleados = this.repo.GetEmpleadosSession(sessionemp);
        //    return View(empleados);

        //}
    }
}
