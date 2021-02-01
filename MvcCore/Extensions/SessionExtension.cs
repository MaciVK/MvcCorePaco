using Microsoft.AspNetCore.Http;
using MvcCorePaco.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Extensions
{
    public static class SessionExtension
    {
        //NECESITAMOS:
        /*
         -Almacenar cualquier objeto en Session 
            Metodo para poder guardar objeto.
         -Debemos recibir( como primer parametro) el objeto que estamos extendiendo (ISession)

         -Devolver el objeto almacenado
            Tenemos el string (json) guardado, debemos devolver el objeto mapeado de dicho string
         */
        public static void SetObject(this ISession session, string key, object val)
        {
            string data = HelperToolkit.SerializeObject(val);
            session.SetString(key, data);
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            //var json = session.GetString<T>(key);
            string data=session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return HelperToolkit.DeserializeJSONObject<T>(data);
        }

    }
}
