using EvolentHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvolentHealth.ServiceInterface
{
	public interface IContactService : IDisposable
	{
		Task<IEnumerable<ContactModel>> GetContacts(CancellationToken ct = default(CancellationToken));
		Task<ContactModel> GetContact(int id, CancellationToken ct = default(CancellationToken));
		Task<ContactModel> AddContact(ContactModel contact, CancellationToken ct = default(CancellationToken));
		Task<bool> UpdateContact(ContactModel contact, CancellationToken ct = default(CancellationToken));
		Task<bool> DeleteContact(int id, CancellationToken ct = default(CancellationToken)); 
	}
}
