using Microsoft.AspNetCore.Mvc;
using MvcCorePaco.Models;
using MvcCorePaco.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Controllers
{
    public class AlumnosController : Controller
    {
        RepositoryAlumnos repo;
        public AlumnosController(RepositoryAlumnos repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {

            return View(this.repo.GetAlumnos());
        }
        public IActionResult Details(int idalumno)
        {
            return View(this.repo.GetAlumno(idalumno));

        }
        public IActionResult Delete(int idalumno)
        {
            this.repo.DeleteAlumno(idalumno);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            this.repo.InsertarAlumno(alumno.idalumno, alumno.nombre, alumno.apellidos, alumno.nota);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int idalumno)
        {
            return View(this.repo.GetAlumno(idalumno));
        }
        [HttpPost]
        public IActionResult Edit(Alumno alumno)
        {
            this.repo.ModificarAlumno(alumno.idalumno,alumno.nombre,alumno.apellidos,alumno.nota);
            return RedirectToAction("Details", new { idalumno = alumno.idalumno });
        }
    }

}
