using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Model;
using WebApp.Utilities;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        [Route("")]
        public IActionResult Home()
        {
            return RedirectToAction("index");
        }
        [Route("home")]
        public IActionResult Index()
        {
            return View("index");
        }
        [Route("login")]
        public IActionResult Login()
        {

            HttpContext.Session.Remove("signInWarning");
            
            return View("Login");
        }
        [Route("cadastro")]
        public IActionResult Cadastro()
        {
            HttpContext.Session.Remove("signUpWarning");
            
            return View("Cadastro");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("signup")]
        public IActionResult SignUp(string username, string email, string pass, string passConfirm)
        {
            using (var context = new AplicacaoDbContext())
            {
                HttpContext.Session.Remove("signUpWarning");
                
                var users = context.Usuarios.ToList();
                var exist = users.Exists(u => u.Email == email);
                if (exist)
                {
                    HttpContext.Session.SetString("signUpWarning", "Usuário já existente");
                    return RedirectToAction("Cadastro");
                }
                else
                {
                    if (pass != passConfirm)
                    {
                        HttpContext.Session.SetString("signUpWarning", "A confirmação da senha deve ser a mesma que a senha normal");
                        return RedirectToAction("Cadastro");
                    }
                    else
                    {
                        string senhaHash = Criptografia.RetornarMD5(pass);
                        Usuario user = new Usuario
                        {
                            Nome = username,
                            Email = email,
                            Perfil = "Usuario",
                            Provedor = "",
                            Senha = senhaHash
                        };
                        context.Usuarios.Add(user);
                        context.SaveChanges();
                        HttpContext.Session.SetString("userName", user.Nome);
                        HttpContext.Session.SetString("userPerfil", user.Perfil);
                        HttpContext.Session.SetString("userEmail", user.Email);
                        return RedirectToAction("Index", "Dashboard");

                    }
                }
            }
        }

        [HttpGet]
        [Route("loginGoogle")]
        public IActionResult LoginGoogle(string returnUrl = "/dashboard")
        {                      
            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Route("signin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SignIn(string email, string pass)
        {
            using(var context = new AplicacaoDbContext())
            {
                HttpContext.Session.Remove("signInWarning");
                
                var user = context.Usuarios.FirstOrDefault(u => u.Email == email);
                if(user != null)
                {
                    if(Criptografia.ComparaMD5(pass, user.Senha))
                    {
                        HttpContext.Session.SetString("userName", user.Nome);
                        HttpContext.Session.SetString("userPerfil", user.Perfil);
                        HttpContext.Session.SetString("userEmail", user.Email);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        HttpContext.Session.SetString("signInWarning", "Senha incorreta");
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("signInWarning", "Usuário não cadastrado");
                    return RedirectToAction("Login");
                }
            }   
        }
    }
}