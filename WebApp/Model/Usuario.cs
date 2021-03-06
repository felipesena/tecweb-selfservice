﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Usuario
    {
        [Key]
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Perfil { get; set; }
        public string Provedor { get; set; }

        public virtual ICollection<Prato> Pratos { get; set; }
        public virtual ICollection<HistoricoConsumo> Consumos { get; set; }
    }
}
