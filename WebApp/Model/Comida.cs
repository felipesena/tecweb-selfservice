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
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int QuantidadeBase { get; set; }
        [Required]
        public string Unidade { get; set; }        
        [Required]
        public virtual Propriedade Propriedade { get; set; }
        [ForeignKey("CategoriaId")]
        [Required]
        public virtual Categoria Categoria { get; set; }

        
    }
}
