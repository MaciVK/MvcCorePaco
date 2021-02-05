using Microsoft.Extensions.Caching.Memory;
using MvcCorePaco.Data;
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
        IMemoryCache MemoryCache;

        

        public RepositoryHospital(HospitalContext context, IMemoryCache MemoryCache)
        {
            this.context = context;
            this.MemoryCache = MemoryCache;
        }
        #region TABLA DEPARTAMENTOS
        public List<Departamento> GetDepartamentos()
        {

            List<Departamento> lista;
            if (this.MemoryCache.Get("DEPARTAMENTOS") == null)
            {
                var consulta = from departamentos in this.context.Departamentos
                               select departamentos;
                lista = consulta.ToList();
                this.MemoryCache.Set("DEPARTAMENTOS", lista);

            }
            else
            {
                lista = this.MemoryCache.Get("DEPARTAMENTOS") as List<Departamento>;
            }
            return lista;
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
        #region TABLA EMPLEADOS
        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.OrderBy(emp => emp.NumDept).ToList();
        }
        public List<Empleado> GetEmpleadosDepartamentos(List<int> iddepts)
        {
            var consulta = from empleados in this.context.Empleados
                           where iddepts.Contains(empleados.NumDept)
                           orderby empleados.NumDept
                           select empleados;
            return consulta.ToList();
        }

        public void CreateDepartamento(int iddepart, string nombre, string loc, string imagen)
        {
            Departamento dep = new Departamento();
            dep.IdDepartamento = iddepart;
            dep.Nombre = nombre;
            dep.Localidad = loc;
            dep.Imagen = imagen;
            this.context.Departamentos.Add(dep);
            this.context.SaveChanges();

        }

        public List<Empleado> GetEmpleadosSession(List<int> idempleados)
        {
            var consulta = from datos in this.context.Empleados
                           where idempleados.Contains(datos.IdEmpleado)
                           select datos;
            return consulta.OrderBy(x=>x.IdEmpleado).ToList();


        }

        #endregion
    }
}
