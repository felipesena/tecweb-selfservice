using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class AlimentoPrato
    {
        [Key]
        [Required]
        public int AlimentoPratoId { get; set; }
        [ForeignKey("PratoId")]
        [Required]
        public virtual Prato Prato { get; set; }
        [ForeignKey("ComidaId")]
        [Required]
        public virtual Comida Comida { get; set; }
    }
}
