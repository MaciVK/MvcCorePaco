using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Helpers
{
    public class FileUploadService
    {
        PathProvider pathprovider;
        public FileUploadService(PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;
        }
        public async Task<string> UploadFileAsync(IFormFile fichero, Folders folder)
        {

            string filename = fichero.FileName;
            string Path = this.pathprovider.MapPath(filename, folder);
            using (var stream = new FileStream(Path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            return Path;
        }
    }
}
