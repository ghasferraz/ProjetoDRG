﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDRG.Models
{
    public class Banco
    {
		[Key]
		public int Id { get; set; }
		[Required]
		public string Nome { get; set; }
		public virtual BancoSistema sistema { get; set; }

    }
}
