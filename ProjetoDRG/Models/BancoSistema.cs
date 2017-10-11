using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDRG.Models
{
	public class BancoSistema
	{
		[ForeignKey("sistema")]
		public int IdSistema { get; set; }
		[ForeignKey("banco")]
		public int IdBanco { get; set; }
		public virtual Sistema sistema { get; set; }
		public virtual Banco banco {get;set;}
    }
}
