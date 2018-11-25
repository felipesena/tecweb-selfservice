using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Prato
    {
        [Key]
        [Required]
        public int PratoId { get; set; }
        [Required]
        public string NomePrato { get; set; }
        [ForeignKey("UsuarioId")]
        [Required]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("CategoriaId")]
        [Required]
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<AlimentoPrato> Alimentos { get; set; }
        

    }
}
