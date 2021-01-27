using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MvcCorePaco.Data;

namespace MvcCorePaco.Repositories
{
    public class RepositoryDepartamentosMySQL : IRepositoryDepartamentos

    {
        DepartamentosContext context;

        public RepositoryDepartamentosMySQL(DepartamentosContext context)
        {
            this.context = context;

        }
        public void CreateDepartamento(int iddepart, string nombre, string loc)
        {
            Departamento dep = new Departamento();
            dep.IdDepartamento = iddepart;
            dep.Nombre = nombre;
            dep.Localidad = loc;
            this.context.Add(dep);
            this.context.SaveChanges();
        }

        public void CreateDepartamento(int iddepart, string nombre, string loc, string imagen)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartamento(int iddepart)
        {
            Departamento dep = this.GetDepartamento(iddepart);
            this.context.Departamentos.Remove(dep);
            this.context.SaveChanges();
        }

        public void EditDepartamento(int iddepart, string nombre, string loc)
        {
            Departamento dep = this.GetDepartamento(iddepart);
            dep.Nombre = nombre;
            dep.Localidad = loc;
            this.context.SaveChanges();
        }

        public Departamento GetDepartamento(int iddepart)
        {
            return this.context.Departamentos.Where(x => x.IdDepartamento == iddepart).FirstOrDefault();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from depts in this.context.Departamentos
                           select depts;
            return consulta.ToList();
        }
    }
}
