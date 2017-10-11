using ProjetoDRG.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDRG.Extensions
{
    public static class DbContextExtensions
	{
		public static void Seed(this ApplicationDbContext context)
		{
			// Perform database delete and create
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			// Perform seed operations
			/*	AddCountries(context);
				AddAreas(context);
				AddGrades(context);
				AddCrags(context);
				AddClimbs(context);
				*/
			context.Banco.Add(new Models.Banco { Nome = "Oracle" });
			context.Sistema.Add(new Models.Sistema {NomeSistema = "MV" });
			// Save changes and release resources
			context.SaveChanges();
			context.Dispose();
		}


	}
}
