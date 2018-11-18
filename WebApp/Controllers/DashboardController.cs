using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
        [Route("dashboard/coletaAPICategory")]
        public IActionResult ColetaAPICategory(IFormFile arquivo)
        {

            using (var context = new AplicacaoDbContext())
            {
                using (StreamReader a = new StreamReader(arquivo.OpenReadStream()))
                {
                    var json = a.ReadToEnd();

                    List<Categoria> categorias = JsonConvert.DeserializeObject<List<Categoria>>(json);                    

                    foreach (var categoria in categorias)
                    {
                        context.Categorias.Add(categoria);                        
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