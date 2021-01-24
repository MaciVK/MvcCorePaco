using MvcCorePaco.Helpers;
using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCorePaco.Repositories
{
    public class RepositoryAlumnos
    {
        PathProvider pathprovider;
        XDocument docxml;
        private string Path;
        public RepositoryAlumnos(PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;
            this.Path = pathprovider.MapPath("alumnos.xml", Folders.Documents);
            this.docxml = XDocument.Load(Path);
        }
        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           select new Alumno
                           {
                               idalumno = int.Parse(datos.Element("idalumno").Value),
                               nombre = datos.Element("nombre").Value,
                               apellidos = datos.Element("apellidos").Value,
                               nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.ToList();
        }
        public Alumno GetAlumno(int idalumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select new Alumno
                           {
                               idalumno = int.Parse(datos.Element("idalumno").Value),
                               nombre = datos.Element("nombre").Value,
                               apellidos = datos.Element("apellidos").Value,
                               nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.FirstOrDefault();
        }
        public void DeleteAlumno(int idalumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select datos;
            XElement elementAlumno = consulta.FirstOrDefault();
            elementAlumno.Remove();
            this.docxml.Save(this.Path);
        }
        public void InsertarAlumno(int idalumno, string nombre, string apellidos, int nota)
        {
            XElement ElementAlumno = new XElement("alumno");
            XElement ElementIdAlumno = new XElement("idalumno", idalumno);
            XElement ElementNombre = new XElement("nombre", nombre);
            XElement ElementApellidos = new XElement("apellidos", apellidos);
            XElement ElementNota = new XElement("nota", nota);
            ElementAlumno.Add(ElementIdAlumno, ElementNombre, ElementApellidos, ElementNota);
            //AGREGAMOS EL XELEMENT LA AGREGAMOS A DONDE CORRESPONDA
            this.docxml.Element("alumnos").Add(ElementAlumno);
            this.docxml.Save(this.Path);
        }
        public void ModificarAlumno(int idalumno, string nombre, string apellidos, int nota)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select datos;
            XElement element = consulta.FirstOrDefault();
            element.Element("nombre").Value = nombre;
            element.Element("apellidos").Value = apellidos;
            element.Element("nota").Value = nota.ToString();
            this.docxml.Save(this.Path);
        }

    }
}
