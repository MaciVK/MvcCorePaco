using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCorePaco.Helpers;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        PathProvider pathprovider;
        IConfiguration Configuration;
        public HomeController(PathProvider pathprovider, IConfiguration configuration)
        {
            this.pathprovider = pathprovider;
            this.Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EjemploMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EjemploMail(string receptor, string asunto, string mensaje, IFormFile fichero)
        {
            MailMessage mail = new MailMessage();
            string usermail = this.Configuration["UsuarioMail"];
            string password = this.Configuration["PasswordMail"];

            return View();
        }
    }
}
