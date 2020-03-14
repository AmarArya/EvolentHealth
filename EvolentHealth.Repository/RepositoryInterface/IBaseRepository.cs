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
	public interface IBaseRepository<T> : IDisposable where T : class
	{
		Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default(CancellationToken));
		Task<T> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
		IEnumerable<T> SearchBy(Expression<Func<T, bool>> predicate, CancellationToken ct = default(CancellationToken));
		Task<T> AddAsync(T entity, CancellationToken ct = default(CancellationToken));
		Task<bool> UpdateAsync(T entity, CancellationToken ct = default(CancellationToken));
		Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

	}
}
