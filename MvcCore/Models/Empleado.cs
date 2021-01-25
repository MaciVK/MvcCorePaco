using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Models
{
    [Table("EMP")]
    public class Empleado
    {
        //CONVENIENTE EN LA BBDD CREAR VISTAS TAMBIEN CON LAS FOREIGN KEY PARA HACER SOLO UNA CONSULTA
        //SI AQUI LOS DECORAMOS CON [ForeignKey] HACE MÁS CONSULTAS Y PIERDE EFICACIA
        [Key]
        [Column("EMP_NO")]
        public int IdEmpleado { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("DEPT_NO")]
        public int NumDept { get; set; }
    }
}
