using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        [Route("dashboard")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("dashboard/coletaAPICategory")]
        public IActionResult ColetaAPICategory(IFormFile arquivo)
        {
            using (StreamReader a = new StreamReader(arquivo.OpenReadStream()))
            {
                var json = a.ReadToEnd();

                dynamic categories = JsonConvert.DeserializeObject(json);

                foreach (var category in categories)
                {
                    var teste = "a";
                }

            }

            return View("index");
        }

        [Route("dashboard/coletaAPIFood")]
        public IActionResult ColetaAPIFood(IFormFile arquivo)
        {
            using (StreamReader a = new StreamReader(arquivo.OpenReadStream()))
            {
                var json = a.ReadToEnd();

            }

            return View("index");
        }
    }
}