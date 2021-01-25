﻿using MvcCorePaco.Data;
using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Repositories
{
    public class RepositoryHospital : IRepositoryHospital
    {
        HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }
        #region TABLA EMPLEADOS
        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }
        #endregion
        #region TABLA DEPARTAMENTOS
        public List<Departamento> GetDepartamentos()
        {

            var consulta = from departamentos in this.context.Departamentos
                           select departamentos;
            return consulta.ToList();
        }

        public Departamento GetDepartamento(int iddepart)
        {
            return this.context.Departamentos.Where(x => x.IdDepartamento == iddepart).FirstOrDefault();
        }

        public void CreateDepartamento(int iddepart, string nombre, string loc)
        {
            Departamento dept = new Departamento();
            dept.IdDepartamento = iddepart;
            dept.Nombre = nombre;
            dept.Localidad = loc;
            this.context.Departamentos.Add(dept);
            this.context.SaveChanges();
        }

        public void DeleteDepartamento(int iddepart)
        {
            Departamento dept = this.GetDepartamento(iddepart);
            this.context.Departamentos.Remove(dept);
            this.context.SaveChanges();
        }

        public void EditDepartamento(int iddepart, string nombre, string loc)
        {

            Departamento dept = this.GetDepartamento(iddepart);
            dept.Nombre = nombre;
            dept.Localidad = loc;
            this.context.SaveChanges();
        }
        #endregion
    }
}