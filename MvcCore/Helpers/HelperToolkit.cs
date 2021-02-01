using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MvcCorePaco.Helpers
{
    public class HelperToolkit
    {
        public static bool CompararArrayBytes(byte[] A, byte[] B)
        {
            bool iguales = true;
            if (A.Length != B.Length)
            {
                return false;
            }
            for (int i = 0; i < A.Length; i++)
            {
                if (!(A[i].Equals(B[i])))
                {
                    return false;
                }
            }
            return iguales;

        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            if (arrBytes == null)
            {
                return null;
            }
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }
        }
        //METODO QUE RECIBE UN OBJETO Y LO TRANSFORMA EN STRING JSON
        public static String SerializeObject(Object obj)
        {
            string respuesta = JsonConvert.SerializeObject(obj);
            return respuesta;
        }
        //METODO QUE RECIBE UN STRING JSON Y LO TRANSFORMA AL OBJETO
        //QUE RERESENTA DICHO JSON
        public static T DeserializeJSONObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
