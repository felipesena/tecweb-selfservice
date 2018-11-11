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

                    foreach (var food in foods)
                    {
                        c = new Comida();
                        c.Descricao = food.description.Value;
                        int idCategoria = Convert.ToInt32(food.category_id.Value);
                        c.Categoria = context.Categorias.FirstOrDefault(i => i.CategoriaId == idCategoria);
                        c.QuantidadeBase = Convert.ToInt32(food.base_qty.Value);
                        c.Unidade = food.base_unit.Value;
                        c.QuantidadeCalorias = food.attributes.energy.kcal.Value is string || food.attributes.energy.kcal.Value == null ? null : Convert.ToDouble(food.attributes.energy.kcal.Value);
                        c.QuantidadeCarboidrato = food.attributes.carbohydrate.qty.Value is string || food.attributes.carbohydrate.qty.Value == null ? null : Convert.ToDouble(food.attributes.carbohydrate.qty.Value);
                        c.QuantidadeFerro = food.attributes.iron.qty.Value is string || food.attributes.iron.qty.Value == null ? null : Convert.ToDouble(food.attributes.iron.qty.Value);
                        c.QuantidadeFibra = food.attributes.fiber.qty.Value is string || food.attributes.fiber.qty.Value == null ? null : Convert.ToDouble(food.attributes.fiber.qty.Value);
                        c.QuantidadeFosforo = food.attributes.phosphorus.qty.Value is string || food.attributes.phosphorus.qty.Value == null ? null : Convert.ToDouble(food.attributes.phosphorus.qty.Value);
                        c.QuantidadeLipidio = food.attributes.lipid.qty.Value is string || food.attributes.lipid.qty.Value == null ? null : Convert.ToDouble(food.attributes.lipid.qty.Value);
                        c.QuantidadePotassio = food.attributes.potassium.qty.Value is string || food.attributes.potassium.qty.Value == null ? null : Convert.ToDouble(food.attributes.potassium.qty.Value);
                        c.QuantidadeProteina = food.attributes.protein.qty.Value is string || food.attributes.protein.qty.Value == null ? null : Convert.ToDouble(food.attributes.protein.qty.Value);
                        c.QuantidadeSodio = food.attributes.sodium.qty.Value is string || food.attributes.sodium.qty.Value == null ? null : Convert.ToDouble(food.attributes.sodium.qty.Value);
                        c.QuantidadeZinco = food.attributes.zinc.qty.Value is string || food.attributes.zinc.qty.Value == null ? null : Convert.ToDouble(food.attributes.zinc.qty.Value);
                        c.UnidadeCarboidrato = food.attributes.carbohydrate.unit.Value;
                        c.UnidadeFerro = food.attributes.iron.unit.Value;
                        c.UnidadeFibra = food.attributes.fiber.unit.Value;
                        c.UnidadeFosforo = food.attributes.phosphorus.unit.Value;
                        c.UnidadeLipidio = food.attributes.lipid.unit.Value;
                        c.UnidadePotassio = food.attributes.potassium.unit.Value;
                        c.UnidadeProteina = food.attributes.protein.unit.Value;
                        c.UnidadeSodio = food.attributes.sodium.Value;
                        c.UnidadeZinco = food.attributes.zinc.unit.Value;
                        

                        context.Comidas.Add(c);
                    }
                    context.SaveChanges();
                }
            }

            return View("index");
        }
    }
}