﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Model;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        [Route("dashboard")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userPerfil") != null)
            {
                return View("CalculoBasal");
            }
            else if (User.Identity.IsAuthenticated)
            {
                CreateUser();
                return View("CalculoBasal");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public void CreateUser()
        {
            using (var context = new AplicacaoDbContext())
            {
                var users = context.Usuarios.ToList();
                string userEmail = User.FindFirst(c => c.Type.Contains("email")).Value;
                var exist = users.Exists(u => u.Email == userEmail);

                if (exist)
                {
                    var user = users.Find(u => u.Email == userEmail);

                    HttpContext.Session.SetString("userName", user.Nome);
                    HttpContext.Session.SetString("userPerfil", user.Perfil);
                    HttpContext.Session.SetString("userEmail", user.Email);
                }
                else
                {
                    Usuario usuario = new Usuario()
                    {
                        Nome = User.Identity.Name,
                        Perfil = "Usuário",
                        Email = userEmail,
                        Senha = "",
                        Provedor = "Google"
                    };

                    HttpContext.Session.SetString("userName", usuario.Nome);
                    HttpContext.Session.SetString("userPerfil", usuario.Perfil);
                    HttpContext.Session.SetString("userEmail", usuario.Email);

                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                }
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userPerfil");
            HttpContext.Session.Remove("userEmail");
            return RedirectToAction("Index", "Home");
        }
        [Route("CalculoBasal")]
        public IActionResult CalculoBasal()
        {
            return View();
        }
        [Route("Relatorio")]
        public IActionResult Relatorio()
        {
            using (var context = new AplicacaoDbContext())
            {
                var pratos = context.Pratos.Where(p => p.Usuario.Email == HttpContext.Session.GetString("userEmail")).ToList();
                ViewBag.Pratos = pratos;
                return View();
            }
        }
        [Route("Refeicao")]
        public IActionResult Refeicao()
        {
            using (var context = new AplicacaoDbContext())
            {
                var pratos = context.Pratos.ToList();
                ViewBag.Pratos = pratos;
                return View();
            }

        }
        [Route("addAlimento")]
        [HttpPost]
        public IActionResult AdicionarAlimento(string descricao, string quantidadeBase, string calorias, string fibras, string ferro,
            string potassio, string zinco, string carboidratos, string fosforo, string sodio, string proteinas, string lipidios)
        {
            using (var context = new AplicacaoDbContext())
            {
                Comida c = new Comida
                {
                    Descricao = descricao,
                    QuantidadeBase = int.Parse(quantidadeBase),
                    Unidade = "g",
                    QuantidadeCalorias = double.Parse(calorias),
                    QuantidadeFibra = double.Parse(fibras),
                    UnidadeFibra = "g",
                    QuantidadeFerro = double.Parse(ferro),
                    UnidadeFerro = "mg",
                    QuantidadePotassio = double.Parse(potassio),
                    UnidadePotassio = "mg",
                    QuantidadeZinco = double.Parse(zinco),
                    UnidadeZinco = "mg",
                    QuantidadeCarboidrato = double.Parse(carboidratos),
                    UnidadeCarboidrato = "g",
                    QuantidadeFosforo = double.Parse(fosforo),
                    UnidadeFosforo = "mg",
                    QuantidadeProteina = double.Parse(proteinas),
                    UnidadeProteina = "g",
                    QuantidadeSodio = double.Parse(sodio),
                    UnidadeSodio = "mg",
                    QuantidadeLipidio = double.Parse(lipidios),
                    UnidadeLipidio = "g"
                };
                context.Comidas.Add(c);
                context.SaveChanges();
                return RedirectToAction("TabelaNutricional");
            }
        }
        [Route("Alimento")]
        public IActionResult Alimento()
        {
            return View();

        }
        [Route("TabelaNutricional")]
        public IActionResult TabelaNutricional()
        {
            using (var context = new AplicacaoDbContext())
            {
                var comidas = context.Comidas.ToList();
                ViewBag.Comidas = comidas;
                return View();
            }

        }
        [Route("dashboard/coletaAPICategory")]
        public IActionResult ColetaAPICategory(IFormFile arquivo)
        {

            using (var context = new AplicacaoDbContext())
            {
                using (StreamReader a = new StreamReader(arquivo.OpenReadStream()))
                {
                    var json = a.ReadToEnd();

                    dynamic categories = JsonConvert.DeserializeObject(json);

                    Categoria c;

                    foreach (var category in categories)
                    {
                        c = new Categoria
                        {
                            Descricao = category.category.Value
                        };

                        context.Categorias.Add(c);
                    }
                    context.SaveChanges();
                }
            }
            return View("index");
        }

        [Route("dashboard/coletaAPIFood")]
        public IActionResult ColetaAPIFood(IFormFile arquivo)
        {
            using (var context = new AplicacaoDbContext())
            {
                using (StreamReader a = new StreamReader(arquivo.OpenReadStream()))
                {
                    var json = a.ReadToEnd();

                    dynamic foods = JsonConvert.DeserializeObject(json);

                    Comida c;

                    c = new Comida();
                    c.Descricao = foods[0].description.Value;
                    int idCategoria = Convert.ToInt32(foods[0].category_id.Value);
                    c.Categoria = context.Categorias.FirstOrDefault(i => i.CategoriaId == idCategoria);
                    c.QuantidadeBase = Convert.ToInt32(foods[0].base_qty.Value);
                    c.Unidade = foods[0].base_unit.Value;
                    c.QuantidadeCalorias = foods[0].attributes.energy.kcal.Value is string || foods[0].attributes.energy.kcal.Value == null ? null : Convert.ToDouble(foods[0].attributes.energy.kcal.Value);
                    c.QuantidadeCarboidrato = foods[0].attributes.carbohydrate.qty.Value is string || foods[0].attributes.carbohydrate.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.carbohydrate.qty.Value);
                    c.QuantidadeFerro = foods[0].attributes.iron.qty.Value is string || foods[0].attributes.iron.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.iron.qty.Value);
                    c.QuantidadeFibra = foods[0].attributes.fiber.qty.Value is string || foods[0].attributes.fiber.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.fiber.qty.Value);
                    c.QuantidadeFosforo = foods[0].attributes.phosphorus.qty.Value is string || foods[0].attributes.phosphorus.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.phosphorus.qty.Value);
                    c.QuantidadeLipidio = foods[0].attributes.lipid.qty.Value is string || foods[0].attributes.lipid.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.lipid.qty.Value);
                    c.QuantidadePotassio = foods[0].attributes.potassium.qty.Value is string || foods[0].attributes.potassium.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.potassium.qty.Value);
                    c.QuantidadeProteina = foods[0].attributes.protein.qty.Value is string || foods[0].attributes.protein.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.protein.qty.Value);
                    c.QuantidadeSodio = foods[0].attributes.sodium.qty.Value is string || foods[0].attributes.sodium.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.sodium.qty.Value);
                    c.QuantidadeZinco = foods[0].attributes.zinc.qty.Value is string || foods[0].attributes.zinc.qty.Value == null ? null : Convert.ToDouble(foods[0].attributes.zinc.qty.Value);
                    c.UnidadeCarboidrato = foods[0].attributes.carbohydrate.unit.Value;
                    c.UnidadeFerro = foods[0].attributes.iron.unit.Value;
                    c.UnidadeFibra = foods[0].attributes.fiber.unit.Value;
                    c.UnidadeFosforo = foods[0].attributes.phosphorus.unit.Value;
                    c.UnidadeLipidio = foods[0].attributes.lipid.unit.Value;
                    c.UnidadePotassio = foods[0].attributes.potassium.unit.Value;
                    c.UnidadeProteina = foods[0].attributes.protein.unit.Value;
                    c.UnidadeSodio = foods[0].attributes.sodium.Value;
                    c.UnidadeZinco = foods[0].attributes.zinc.unit.Value;


                    context.Comidas.Add(c);

                    context.SaveChanges();
                }
            }

            return View("index");
        }
    }
}
