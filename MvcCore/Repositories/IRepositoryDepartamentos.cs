using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Repositories
{
    public interface IRepositoryDepartamentos
    {
        List<Departamento> GetDepartamentos();
        public Departamento GetDepartamento(int iddepart);
        void DeleteDepartamento(int iddepart);
        void CreateDepartamento(int iddepart, string nombre, string loc);
        void EditDepartamento(int iddepart, string nombre, string loc);
        void CreateDepartamento(int iddepart, string nombre, string loc, string imagen);
    }
}
