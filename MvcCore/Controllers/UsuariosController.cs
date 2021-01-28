using Microsoft.AspNetCore.Mvc;
using MvcCorePaco.Models;
using MvcCorePaco.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Controllers
{
    public class UsuariosController : Controller
    {
        RepositoryUsuarios repo;
        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(int IdUsuario, string Nombre, string UserName, string Pass)
        {
            this.repo.InsertarUsuario(IdUsuario, Nombre, UserName, Pass);
            ViewBag.Creado = "Creado correctamente";
            return View();
        }
        public IActionResult Credenciales()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Credenciales(string username, string pass)
        {
            Usuario user = this.repo.ValidarUsuario(username, pass);
            if (user == null)
            {
                ViewBag.NotSuccess = "<h1 class='text-danger'>Revisa tu Usuario y tu Contraseña</h1>";
                return View();
            }
            else
            {

                return View(user);
            }
        }
        public IActionResult Normalizar()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Normalizar(string nombrearchivo)
        {
            ViewBag.Normalizado = this.repo.NormalizarNombre(nombrearchivo);
            return View();
        }
    }
}
