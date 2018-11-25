using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class HistoricoConsumo
    {
        [Key]
        [Required]
        public int HistoricoConsumoId { get; set; }
        [ForeignKey("UsuarioId")]
        [Required]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("PratoId")]
        [Required]
        public virtual Prato Prato { get; set; }
    }
}
