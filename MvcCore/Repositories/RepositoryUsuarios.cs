using MvcCorePaco.Data;
using MvcCorePaco.Helpers;
using MvcCorePaco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Repositories
{
    public class RepositoryUsuarios
    {
        HospitalContext context;
        public RepositoryUsuarios(HospitalContext context)
        {
            this.context = context;
        }
        public void InsertarUsuario(int idusuario, String nombre, string username, string password)
        {
            Usuario user = new Usuario();
            user.IdUsuario = idusuario;
            user.Nombre = nombre;
            user.UserName = username;
            string salt = CypherService.GenerateSalt();
            user.Salt = salt;
            user.Pass = CypherService.CifradoSaltSHA256(password, salt);
            this.context.Usuarios.Add(user);
            this.context.SaveChanges();
        }
        public Usuario ValidarUsuario(string username, string password)
        {

            Usuario usuario = this.context.Usuarios.Where(x => x.UserName == username).FirstOrDefault();
            if (usuario==null)
            {
                return null;
            }
            else
            {
                string salt = usuario.Salt;
                byte[] passbbdd = usuario.Pass;
                byte[] passtemp = CypherService.CifradoSaltSHA256(password, salt);
                if (HelperToolkit.CompararArrayBytes(passbbdd, passtemp))
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
        }
        public string NormalizarNombre(string nombrearchivo)
        {
            string nombreNormal = FileUploadService.NormalizeName(nombrearchivo);
            return nombreNormal;
        }
    }
}
