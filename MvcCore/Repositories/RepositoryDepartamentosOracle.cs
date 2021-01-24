using MvcCorePaco.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Repositories
{
    public class RepositoryDepartamentosOracle : IRepositoryDepartamentos
    {
        OracleDataAdapter adapterDept;
        DataTable tablaDept;
        OracleCommandBuilder builder;
        public RepositoryDepartamentosOracle(string CadenaOracle)
        {
            this.adapterDept = new OracleDataAdapter("select * from dept", CadenaOracle);
            this.builder = new OracleCommandBuilder(this.adapterDept);
            this.tablaDept = new DataTable();
            this.adapterDept.Fill(tablaDept);

        }
        public void CreateDepartamento(int iddepart, string nombre, string loc)
        {
            DataRow fila = this.tablaDept.NewRow();
            fila["DEPT_NO"] = iddepart;
            fila["DNOMBRE"] = nombre;
            fila["LOC"] = loc;
            this.tablaDept.Rows.Add(fila);
            this.adapterDept.Update(this.tablaDept);
            this.tablaDept.AcceptChanges();
        }

        private DataRow GetDataRow(int iddepart)
        {
            DataRow fila = this.tablaDept.AsEnumerable().Where(x => x.Field<int>("DEPT_NO") == iddepart).FirstOrDefault();
            return fila;
        }
        public void DeleteDepartamento(int iddepart)
        {
            //PARA ELIMINAR HAY QUE HACERLO SOBRE EL DATATABLE
            //BUSCAMOS LA FILA POR EL ID
            //LA FILA, TIENE UN METODO DELETE QUE MARCARÁ EN LA TABLA EL VALOR PARA ELIMINAR
            //DESPUES, EL ADAPTADOR, AL IGUAL QUE TIENE UN OBJETO PARA TRAER LOS DATOS, EL .Fill()
            //TAMBIEN TIENE UNO PARA AUTOMATIZAR LOS CAMBIOS: .Update()
            DataRow fila = this.GetDataRow(iddepart);
            fila.Delete();
            this.adapterDept.Update(tablaDept);
            this.tablaDept.AcceptChanges();
        }

        public void EditDepartamento(int iddepart, string nombre, string loc)
        {
            DataRow fila = this.GetDataRow(iddepart);
            fila["DNOMBRE"] = nombre;
            fila["LOC"] = loc;
            this.adapterDept.Update(this.tablaDept);
            this.tablaDept.AcceptChanges();

        }

        public Departamento GetDepartamento(int iddepart)
        {
            var consulta = from departamento in this.tablaDept.AsEnumerable()
                           where departamento.Field<int>("DEPT_NO") == iddepart
                           select new Departamento
                           {
                               IdDepartamento = departamento.Field<int>("DEPT_NO"),
                               Nombre = departamento.Field<string>("DNOMBRE"),
                               Localidad = departamento.Field<string>("LOC")
                           };
            return consulta.FirstOrDefault();

        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from departamentos in this.tablaDept.AsEnumerable()
                           select new Departamento
                           {
                               IdDepartamento = departamentos.Field<int>("DEPT_NO"),
                               Nombre = departamentos.Field<string>("DNOMBRE"),
                               Localidad = departamentos.Field<string>("LOC")
                           };
            return consulta.ToList();

        }
    }
}
