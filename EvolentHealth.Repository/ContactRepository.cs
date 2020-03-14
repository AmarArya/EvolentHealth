using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EvolentHealth.Repository.RepositoryInterface;
using EvolentHealth.Repository.Context;
using EvolentHealth.Entities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace EvolentHealth.Repository
{
	public class ContactRepository : IContactRepository
	{
		private readonly EvolentHealthContext _context = new EvolentHealthContext();

		public ContactRepository(EvolentHealthContext context)
		{
			_context = context;
		}

		private async Task<bool> ContactExists(int id, CancellationToken ct = default(CancellationToken))
		{
			return await _context.Contacts.AnyAsync(a => a.ContactId == id, ct);
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task<IEnumerable<Contact>> GetAllAsync(CancellationToken ct = default(CancellationToken))
		{
			return await _context.Contacts.ToListAsync(ct);
		}

		public async Task<Contact> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
		{
			return await _context.Contacts.SingleOrDefaultAsync(x => x.ContactId == id, ct);
		}

		public IEnumerable<Contact> SearchBy(Expression<Func<Contact, bool>> predicate, CancellationToken ct = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public async Task<Contact> AddAsync(Contact contact, CancellationToken ct = default(CancellationToken))
		{
			_context.Contacts.Add(contact);
			await _context.SaveChangesAsync(ct);
			return contact;
		}

		public async Task<bool> UpdateAsync(Contact contact, CancellationToken ct = default(CancellationToken))
		{
			if (!await ContactExists(contact.ContactId, ct))
				return false;

			_context.Entry<Contact>(contact).State = EntityState.Modified;
			await _context.SaveChangesAsync(ct);
			return true;
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
		{
			if (!await ContactExists(id, ct))
				return false;

			var toRemove = _context.Contacts.Find(id);
			_context.Contacts.Remove(toRemove);
			await _context.SaveChangesAsync(ct);

			return true;
		}

	}
}
