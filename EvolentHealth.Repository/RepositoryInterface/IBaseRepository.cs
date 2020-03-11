using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EvolentHealth.Entities;
using System.Linq.Expressions;


namespace EvolentHealth.Repository.RepositoryInterface
{
	public interface IBaseRepository : IDisposable
	{

		Task<List<Contact>> GetAllAsync(CancellationToken ct = default(CancellationToken));
		Task<Contact> GetByContactIdAsync(int id, CancellationToken ct = default(CancellationToken));
		IEnumerable<Contact> SearchBy(Expression<Func<Contact, bool>> predicate, CancellationToken ct = default(CancellationToken));
		Task<Contact> AddAsync(Contact newAlbum, CancellationToken ct = default(CancellationToken));
		Task<bool> UpdateAsync(Contact album, CancellationToken ct = default(CancellationToken));
		Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
	}
}
