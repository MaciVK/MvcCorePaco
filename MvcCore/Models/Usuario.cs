using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Models
{
    [Table("USERHASH")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("USUARIO")]
        public string UserName { get; set; }
        [Column("PASS")]
        public byte[] Pass { get; set; }
        [Column("SALT")]
        public string Salt { get; set; }
    }
}
