using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Helpers
{
    public enum Folders
    {
        Images=0, Documents=1, Temp=2,
    }
    public class PathProvider
    {
        IWebHostEnvironment environment;
        public PathProvider(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public string MapPath(string filename, Folders folder)
        {
            string carpeta = "";//folder.ToString(); //Documents, Images
            if (folder == Folders.Documents)
            {
                carpeta = "documents";
            }else if (folder == Folders.Images)
            {
                carpeta = "images";
            }else if{
                carpeta = "temporal";
            }
            string path = Path.Combine(this.environment.WebRootPath, carpeta, filename);
            return path;
        }
    }
}
