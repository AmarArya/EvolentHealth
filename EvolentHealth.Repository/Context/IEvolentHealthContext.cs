using EvolentHealth.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EvolentHealth.Repository.Context
{
	public interface IEvolentHealthContext : IObjectContextAdapter, IDisposable
	{
		DbSet<Contact> Contacts { get; set; }

	}
}
