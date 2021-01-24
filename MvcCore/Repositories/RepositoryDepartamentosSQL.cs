using MvcCorePaco.Data;
using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Repositories
{
    public class RepositoryDepartamentosSQL : IRepositoryDepartamentos
    {
        DepartamentosContext context;
        public RepositoryDepartamentosSQL(DepartamentosContext context)
        {
            this.context = context;
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

        public Departamento GetDepartamento(int iddepart)
        {
            return this.context.Departamentos.Where(x => x.IdDepartamento == iddepart).FirstOrDefault();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from departamentos in this.context.Departamentos
                           select departamentos;
            return consulta.ToList();

        }
    }
}
