using EvolentHealth.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolentHealth.Repository.Context
{
	public class EvolentHealthContext : DbContext, IEvolentHealthContext
	{
		public EvolentHealthContext()
			: base("name=EvolentHealthDBConnectionString")
		{
			Database.SetInitializer<EvolentHealthContext>(new CreateDatabaseIfNotExists<EvolentHealthContext>());

		}

		public DbSet<Contact> Contacts { get; set; }
	}
}
