using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        public IActionResult Index()
        {
            return View("index");
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [Route("cadastro")]
        public IActionResult Cadastro()
        {
            return View("Cadastro");
        }
    }
}