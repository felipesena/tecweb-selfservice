﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Categoria
    {
        [Key]
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
