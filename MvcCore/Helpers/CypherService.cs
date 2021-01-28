using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MvcCorePaco.Helpers
{
    public class CypherService
    {
        public static string CifradoSHA1(string contenido)
        {

            //TENEMOS QUE TRABAJAR A NIVEL DE Byte[]
            //CONVERTIMOS A byte[] EL CONTENIDO DE ENTRADA
            byte[] entrada;
            //EL CIFRADO SE HACE A NIVEL DE byte[] Y DEVUELVE OTRO byte[] DE SALIDA
            byte[] salida;
            //NECESITAMOS UN CONVERSOR PARA TRANSFORMAR DE byte[] A STRING Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS EL OBJETO QUE SE ENCARGA DE CIFRAR
            SHA1Managed sha = new SHA1Managed();
            //HAY QUE CONVERTIR EL CONTENIDO DE ENTRADA A byte[]
            entrada = encoding.GetBytes(contenido);
            //EL OBJETO SHA1Managed TIENE UN METODO PARA DEVOLVER LOS BYTES DE SALIDA REALIZANDO EL CIFRADO
            salida = sha.ComputeHash(entrada);
            string res = encoding.GetString(salida);
            //SOLAMENTE SI PONEMOS EL MISMO contenido TENDRÍAMOS LA MISMA SECUENCIA DE SALIDA
            return res;
        }
        //METODO PARA GENERAR SALT
        public static string GenerateSalt()
        {
            Random rand = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int numero = rand.Next(0, 255);
                char letra = Convert.ToChar(numero);
                salt += letra;
            }

            return salt;
        }

        public static byte[] CifradoSaltSHA256(string contenido, string salt)
        {
            //PARA EL SALT, SE ALMACENA ENTRE MEDIAS DEL CONTENIDO, EN POSICIONES EXACTAS,..... donde yo quiera
            
            string ContenidoSalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(ContenidoSalt);
            //CIFRAMOS LAS VECES QUE NOS PONGA EN LAS ITERACIONES
            for (int i = 1; i <= 100; i++)
            {
                //REALIZAMOS EL CIFRADO N VECES
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            
            return salida;
        }
        public static string CifradoSaltSHA256(string contenido, int iteraciones, string salt)
        {
            //PARA EL SALT, SE ALMACENA ENTRE MEDIAS DEL CONTENIDO, EN POSICIONES EXACTAS,.....
            string ContenidoSalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(ContenidoSalt);
            //CIFRAMOS LAS VECES QUE NOS PONGA EN LAS ITERACIONES
            for (int i=1;i<=iteraciones; i++)
            {
                //REALIZAMOS EL CIFRADO N VECES
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            string TextoSalida = Encoding.UTF8.GetString(salida);
            return TextoSalida;
        }
    }
}
