using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjetoDRG.Models
{
	[XmlRootAttribute("loteInternacao")]
    public class LoteInternacao
    {
		public LoteInternacao()
		{
			this.Internacoes = new List<Internacao>();	
		}
		//[XmlArrayAttribute("internacao")]
		public List<Internacao> Internacoes { get; set; }

		public class Internacao
		{
			public Internacao()
			{
				this.Medicos = new List<Medico>();
				this.Operadoras = new Operadora();
				this.Beneficiarios = new Beneficiario();
			}
			public string Situacao { get; set; }
			public string CaraterInternacao { get; set; }
			public string NumeroOperadora { get; set; }
			public DateTime dataInternacao { get; set; }
			public Beneficiario Beneficiarios { get; set; }
			public Operadora Operadoras { get; set; }
			public List<Medico> Medicos { get; set; }

			public class Beneficiario
			{
				public int Codigo { get; set; }
				public string Nome { get; set; }
			}

			public class Operadora
			{
				public int Codigo { get; set; }
				public string Nome { get; set; }
			}

			public class Medico
			{
				public string Crm { get; set; }
				public string Nome { get; set; }

			}
		}
	}
}
