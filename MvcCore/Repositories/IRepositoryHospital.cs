using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Repositories
{
    public interface IRepositoryHospital : IRepositoryDepartamentos
    {
        List<Empleado> GetEmpleados();
    }
}
