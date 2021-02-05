using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Extensions
{
    public static class TempDataExtension
    {
        public static void SetObject(this ITempDataDictionary tempdata, string key, object data)
        {
            tempdata[key] = JsonConvert.SerializeObject(data);
        }
        public static T GetObject<T>(this ITempDataDictionary tempdata, string key)
        {
            if (tempdata[key] != null)
            {
                string data = tempdata[key].ToString();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else { return default(T); }
        }
    }
}
