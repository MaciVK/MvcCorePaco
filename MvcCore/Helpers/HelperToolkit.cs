using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
