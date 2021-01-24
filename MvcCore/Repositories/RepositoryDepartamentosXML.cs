using MvcCorePaco.Helpers;
using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCorePaco.Repositories
{

    public class RepositoryDepartamentosXML:IRepositoryDepartamentos
    {
        public PathProvider pathprovider;
        public XDocument docxml;
        private string path;
        public RepositoryDepartamentosXML(PathProvider provider)
        {
            this.pathprovider = provider;
            this.path = this.pathprovider.MapPath("departamentos.xml", Folders.Documents);
            this.docxml = XDocument.Load(this.path);
        }
        public List<Departamento> GetDepartamentos()
        {
            var consulta = from departamentos in this.docxml.Descendants("DEPARTAMENTO")
                           orderby departamentos.Attribute("NUMERO").Value
                           select new Departamento
                           {
                               IdDepartamento = int.Parse(departamentos.Attribute("NUMERO").Value),
                               Nombre = departamentos.Element("NOMBRE").Value,
                               Localidad = departamentos.Element("LOCALIDAD").Value
                           };
            return consulta.ToList();

        }
        public Departamento GetDepartamento(int iddepart)
        {
            var consulta = from departamento in this.docxml.Descendants("DEPARTAMENTO")
                           where departamento.Attribute("NUMERO").Value == iddepart.ToString()
                           select new Departamento
                           {
                               IdDepartamento = int.Parse(departamento.Attribute("NUMERO").Value),
                               Nombre = departamento.Element("NOMBRE").Value,
                               Localidad = departamento.Element("LOCALIDAD").Value
                           };
            return consulta.FirstOrDefault();
        }
        public void DeleteDepartamento(int iddepart)
        {
            var consulta = from departamento in this.docxml.Descendants("DEPARTAMENTO")
                           where departamento.Attribute("NUMERO").Value == iddepart.ToString()
                           select departamento;
            XElement depart = consulta.FirstOrDefault();
            depart.Remove();
            this.docxml.Save(this.path);

        }
        public void CreateDepartamento(int iddepart, string nombre, string loc)
        {
            XElement depttag = new XElement("DEPARTAMENTO");
            depttag.SetAttributeValue("NUMERO", iddepart);
            XElement nombredept = new XElement("NOMBRE", nombre);
            XElement locdept = new XElement("LOCALIDAD", loc);
            depttag.Add(nombredept, locdept);
            this.docxml.Element("DEPARTAMENTOS").Add(depttag);
            this.docxml.Save(this.path);
        }
        public void EditDepartamento(int iddepart, string nombre, string loc)
        {
            var consulta = from departamento in this.docxml.Descendants("DEPARTAMENTO")
                           where departamento.Attribute("NUMERO").Value == iddepart.ToString()
                           select departamento;
            XElement depart = consulta.FirstOrDefault();
            depart.Element("NOMBRE").Value = nombre;
            depart.Element("LOCALIDAD").Value = loc;
            this.docxml.Save(this.path);
        }

    }
}
