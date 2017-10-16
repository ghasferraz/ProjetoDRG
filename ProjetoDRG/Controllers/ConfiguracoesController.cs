using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDRG.Data;
using ProjetoDRG.Models;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace ProjetoDRG.Controllers
{
	public class ConfiguracoesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ConfiguracoesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Configuracoes
		public async Task<IActionResult> Index()
		{

			return View(await _context.Configuracao.ToListAsync());
		}

		// GET: Configuracoes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var configuracao = await _context.Configuracao
				.SingleOrDefaultAsync(m => m.Id == id);
			if (configuracao == null)
			{
				return NotFound();
			}

			return View(configuracao);
		}

		// GET: Configuracoes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Configuracoes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Usuario,Senha")] Configuracao configuracao)
		{
			if (ModelState.IsValid)
			{
				_context.Add(configuracao);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(configuracao);
		}
		public ActionResult ControlePaginas(IList<Banco> banco, IList<Sistema> sistemas)
		{
			if (_context.Configuracao.FirstOrDefault() == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return RedirectToAction("Home");
			}

			return View(banco);
		}

		public ActionResult GeracaoXML()
		{
			LoteInternacao l = BuscaXmL();
			string a;
			using (StringWriter str = new StringWriter())
			using (XmlTextWriter writer = new XmlTextWriter(str))
			{
				writer.WriteStartDocument();
				writer.WriteStartElement("loteInternacao");
				foreach (var i in l.Internacoes)
				{
					writer.WriteStartElement("Internacao");
					writer.WriteElementString("situacao", i.Situacao);
					writer.WriteElementString("caraterInternacao", i.CaraterInternacao);
					writer.WriteElementString("numeroAtendimento", i.NumeroOperadora);
					writer.WriteElementString("numeroAutorizacao", null);
					writer.WriteElementString("dataInternacao", i.dataInternacao.ToString("yyyy-MM-ddTHH:mm:ss"));
					writer.WriteElementString("dataAlta", null);
					writer.WriteElementString("condicaoAlta", null);
					writer.WriteElementString("dataAutorizacao", null);
					writer.WriteElementString("codigoCidPrincipal", null);
					writer.WriteElementString("internadoOutrasVezes", null);
					writer.WriteElementString("reinternado", null);
					writer.WriteElementString("recaida", null);

					writer.WriteStartElement("Hospital");
					writer.WriteElementString("codigo", null);
					writer.WriteElementString("nome", null);
					writer.WriteEndElement();



					writer.WriteStartElement("Beneficiario");
					writer.WriteElementString("codigo", i.Beneficiarios.Codigo.ToString());
					writer.WriteElementString("nome", i.Beneficiarios.Nome);
					writer.WriteElementString("dataNascimento", null);
					writer.WriteElementString("sexo", null);
					writer.WriteElementString("nomeMae", null);
					writer.WriteElementString("cpf", null);
					writer.WriteElementString("endereco", null);
					writer.WriteElementString("recemNascido", null);
					writer.WriteEndElement();


					writer.WriteStartElement("Operadora");
					writer.WriteElementString("codigo", i.Operadoras.Codigo.ToString());
					writer.WriteElementString("nome", i.Operadoras.Nome);
					writer.WriteElementString("sigla", null);
					writer.WriteElementString("plano", null);
					writer.WriteElementString("numeroCarteira", null);
					writer.WriteElementString("dataValidade", null);
					writer.WriteEndElement();


					foreach (var m in i.Medicos)
					{

						writer.WriteStartElement("Medico");
						writer.WriteElementString("nome", m.Nome);
						writer.WriteElementString("ddd", null);
						writer.WriteElementString("telefone", null);
						writer.WriteElementString("email", null);
						writer.WriteElementString("uf", null);
						writer.WriteElementString("crm", m.Crm);
						writer.WriteElementString("especialidade", null);
						writer.WriteElementString("tipoAtuacao", null);
						writer.WriteEndElement();
					}


					writer.WriteStartElement("CidSecundario");
					do
					{
						writer.WriteElementString("codigoCidSecundario", null);

					} while (writer.Equals("CidSecundario"));
					writer.WriteEndElement();




					writer.WriteStartElement("Procedimento");
					do
					{
						writer.WriteElementString("codigoProcedimento", null);
						writer.WriteElementString("dataAutorizacao", null);
						writer.WriteElementString("dataExecucao", null);
					} while (writer.Equals("Procedimento"));
					writer.WriteEndElement();


					writer.WriteStartElement("Cti");
					writer.WriteElementString("dataInical", null);
					writer.WriteElementString("dataFinal", null);
					writer.WriteElementString("codigoCidPrincipal", null);
					writer.WriteElementString("condiaoAlta", null);
					writer.WriteElementString("uf", null);
					writer.WriteElementString("crm", null);
					writer.WriteElementString("codigoHospital", null);
					writer.WriteElementString("nomeHospital", null);
					writer.WriteEndElement();



					writer.WriteStartElement("SuporteVentilatorio");
					writer.WriteElementString("tipo", null);
					writer.WriteElementString("tipoInvasivo", null);
					writer.WriteElementString("local", null);
					writer.WriteElementString("dataInicial", null);
					writer.WriteElementString("dataFinal", null);
					writer.WriteStartElement("CondicaoAdquiridaSuporteVentilatorio");
					writer.WriteElementString("codigoCondicaoAdquirida", null);
					writer.WriteElementString("dataOcorrencia", null);
					writer.WriteEndElement();
					writer.WriteEndElement();



					writer.WriteStartElement("CondicaoAdquirida");
					writer.WriteElementString("codigoCondicaoAdquirida", null);
					writer.WriteElementString("dataOcorrencia", null);
					writer.WriteEndElement();

					writer.WriteStartElement("AltaAdministrativa");
					writer.WriteElementString("numeroAtendimento", null);
					writer.WriteElementString("numeroAutorizacao", null);
					writer.WriteEndElement();


					writer.WriteEndElement();



				}
				writer.WriteEndElement();

				writer.Close();

				a = str.ToString();



			}

			

			return Content(a);
		}
		public ActionResult GeracaoXML2()
		{
			LoteInternacao subReq = BuscaXmL();
			XmlSerializer xsSubmit = new XmlSerializer(typeof(LoteInternacao));
			var xml = "";

			using (var sww = new StringWriter())
			{
				using (XmlWriter writer = XmlWriter.Create(sww))
				{
					xsSubmit.Serialize(writer, subReq);
					xml = sww.ToString(); // Your XML
				}
			}


			return Content(xml);
		}



		private static LoteInternacao BuscaXmL()
		{
			LoteInternacao l = new LoteInternacao();
			var teste = new LoteInternacao.Internacao();




			teste.Situacao = "3";
			teste.CaraterInternacao = "1";
			teste.NumeroOperadora = "2";
			teste.dataInternacao = DateTime.Now;

			teste.Medicos.Add(new LoteInternacao.Internacao.Medico { Crm = "12_566", Nome = "Joao" });

			teste.Beneficiarios.Codigo = 1;
			teste.Beneficiarios.Nome = "Jose";

			teste.Operadoras.Codigo = 2;
			teste.Operadoras.Nome = "DRM";











			l.Internacoes.Add(teste);
			return l;
		}

		// GET: Configuracoes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var configuracao = await _context.Configuracao.SingleOrDefaultAsync(m => m.Id == id);
			if (configuracao == null)
			{
				return NotFound();
			}
			return View(configuracao);
		}

		// POST: Configuracoes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,Senha")] Configuracao configuracao)
		{
			if (id != configuracao.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(configuracao);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ConfiguracaoExists(configuracao.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(configuracao);
		}

		// GET: Configuracoes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var configuracao = await _context.Configuracao
				.SingleOrDefaultAsync(m => m.Id == id);
			if (configuracao == null)
			{
				return NotFound();
			}

			return View(configuracao);
		}

		// POST: Configuracoes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var configuracao = await _context.Configuracao.SingleOrDefaultAsync(m => m.Id == id);
			_context.Configuracao.Remove(configuracao);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ConfiguracaoExists(int id)
		{
			return _context.Configuracao.Any(e => e.Id == id);
		}
	}
}
