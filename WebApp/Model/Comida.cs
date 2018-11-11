using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Comida
    {
        [Key]
        [Required]
        public int ComidaId { get; set; }

        public string Descricao { get; set; }

        public int QuantidadeBase { get; set; }

        public string Unidade { get; set; }
        public double? QuantidadeCalorias { get; set; }

        public double? QuantidadeFibra { get; set; }
        public string UnidadeFibra { get; set; }

        public double? QuantidadeFerro { get; set; }
        public string UnidadeFerro { get; set; }

        public double? QuantidadePotassio { get; set; }
        public string UnidadePotassio { get; set; }

        public double? QuantidadeZinco { get; set; }
        public string UnidadeZinco { get; set; }

        public double? QuantidadeCarboidrato { get; set; }
        public string UnidadeCarboidrato { get; set; }

        public double? QuantidadeFosforo { get; set; }
        public string UnidadeFosforo { get; set; }

        public double? QuantidadeSodio { get; set; }
        public string UnidadeSodio { get; set; }

        public double? QuantidadeProteina { get; set; }
        public string UnidadeProteina { get; set; }

        public double? QuantidadeLipidio { get; set; }
        public string UnidadeLipidio { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }


    }
}
