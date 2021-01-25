using Microsoft.AspNetCore.Mvc;
using MvcCorePaco.Models;
using MvcCorePaco.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Controllers
{
    public class DepartamentosController : Controller
    {
        IRepositoryHospital repo;
        public DepartamentosController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(this.repo.GetDepartamentos());
        }
        public IActionResult Details(int iddepart)
        {
            return View(this.repo.GetDepartamento(iddepart));
        }
        public IActionResult Delete(int iddepart)
        {
            this.repo.DeleteDepartamento(iddepart);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Departamento dep)
        {
            this.repo.CreateDepartamento(dep.IdDepartamento, dep.Nombre, dep.Localidad);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int iddepart)
        {
            return View(this.repo.GetDepartamento(iddepart));
        }
        [HttpPost]
        public IActionResult Edit(Departamento dep)
        {
            this.repo.EditDepartamento(dep.IdDepartamento, dep.Nombre, dep.Localidad);
            return RedirectToAction("Index");
        }
        public IActionResult SeleccionMultiple()
        {
            List<Departamento> depts = this.repo.GetDepartamentos();
            List<Empleado> emps = this.repo.GetEmpleados();
            ViewBag.Departamentos = depts;
            return View(emps);
        }
        [HttpPost]
        public IActionResult SeleccionMultiple(List<int> iddepts)
        {
            List<Departamento> depts = this.repo.GetDepartamentos();
            List<Empleado> empleados = this.repo.GetEmpleadosDepartamentos(iddepts);
            ViewBag.Departamentos = depts;
            return View(empleados);
        }
    }
}
