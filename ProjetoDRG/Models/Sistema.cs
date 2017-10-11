using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDRG.Models
{
	public class Sistema
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string NomeSistema { get; set; }
		public virtual BancoSistema banco{ get; set; }
	}
}
