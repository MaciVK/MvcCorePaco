using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache MemoryCache;
        public CachingController(IMemoryCache MemoryCache)
        {
            this.MemoryCache = MemoryCache;
        }
        public IActionResult HoraSistema(int? tiempo)
        {
            if (tiempo == null)
            {
                tiempo = 5;
            }
            string fecha = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
            //PREGUNTAMOS SI EXISTE ALGO EN CACHE
            if (this.MemoryCache.Get("FECHA") == null)
            {
                //NO EXISTE, PUES LO CREAMOS
                this.MemoryCache.Set("FECHA", fecha
                    , new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.GetValueOrDefault())));
                ViewBag.fecha = fecha;
                ViewBag.mensaje = "Almacenando en cache... "+tiempo.Value+" segundos";
            }
            else
            {
                fecha = "Ultimos datos almacenados: " + this.MemoryCache.Get("FECHA").ToString();
                ViewBag.mensaje = "Recuperando del cache...";
                ViewBag.fecha = fecha;


            }
            return View();
        }
        [ResponseCache(Duration =10,
            VaryByQueryKeys =new string[] { "*" })]
        public IActionResult HoraSistemaDistribuida(string dato)
        {
            string fecha = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
            ViewBag.fecha = fecha;
            return View();
        }
    }
}
