using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDRG.Models
{
	public class Configuracao
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Usuario { get; set; }
		[Required]
		public string Senha { get; set; }
    }
}
