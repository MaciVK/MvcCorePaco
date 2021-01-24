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
            throw new NotImplementedException();
        }

        public void DeleteDepartamento(int iddepart)
        {
            throw new NotImplementedException();
        }

        public void EditDepartamento(int iddepart, string nombre, string loc)
        {
            throw new NotImplementedException();
        }

        public Departamento GetDepartamento(int iddepart)
        {
            var consulta = from dept in this.context.Departamentos
                           where dept.IdDepartamento == iddepart
                           select dept;
            return consulta.FirstOrDefault();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from depts in this.context.Departamentos
                           select depts;
            return consulta.ToList();
        }
    }
}
