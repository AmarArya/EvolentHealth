using EvolentHealth.Entities;
using EvolentHealth.Models;
using EvolentHealth.Repository.RepositoryInterface;
using EvolentHealth.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvolentHealth.Service
{
	/// <summary>
	/// 
	/// </summary>
	public class ContactService : IContactService
	{
		private readonly IContactRepository _contactRepository;

		public ContactService(IContactRepository contactRepository)
		{
			this._contactRepository = contactRepository;
		}

		public void Dispose()
		{
			_contactRepository.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ct"></param>
		/// <returns></returns>
		public async Task<IEnumerable<ContactModel>> GetContacts(CancellationToken ct = default(CancellationToken))
		{
			var contacts = await _contactRepository.GetAllAsync(ct);

			return contacts.Select(x => new ContactModel
			{
				ContactId = x.ContactId,
				FirstName = x.FirstName,
				LastName = x.LastName,
				PhoneNumber = x.PhoneNumber,
				Status = x.Status
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public async Task<ContactModel> GetContact(int id, CancellationToken ct = default(CancellationToken))
		{
			var contact = await _contactRepository.GetByContactIdAsync(id, ct);

			if (contact == null) return null;

			return new ContactModel
			{
				ContactId = contact.ContactId,
				FirstName = contact.FirstName,
				LastName = contact.LastName,
				PhoneNumber = contact.PhoneNumber,
				Status = contact.Status
			};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newContact"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public async Task<ContactModel> AddContact(ContactModel newContact, CancellationToken ct = default(CancellationToken))
		{
			var contact = new Contact
			{
				FirstName = newContact.FirstName,
				LastName = newContact.LastName,
				PhoneNumber = newContact.PhoneNumber,
				Status = newContact.Status
			};						  

			contact = await _contactRepository.AddAsync(contact, ct);
			newContact.ContactId = contact.ContactId;
			
			return newContact;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="contactModel"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public async Task<bool> UpdateContact(ContactModel contactModel, CancellationToken ct = default(CancellationToken))
		{
			 var contact = await _contactRepository.GetByContactIdAsync(contactModel.ContactId, ct);

            if (contact == null) return false;

            contact.ContactId = contactModel.ContactId;
            contact.FirstName = contactModel.FirstName;
            contact.LastName = contactModel.LastName;
			contact.PhoneNumber = contactModel.PhoneNumber;
			contact.Status = contactModel.Status;

            return await _contactRepository.UpdateAsync(contact, ct);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public async Task<bool> DeleteContact(int id, CancellationToken ct = default(CancellationToken))
		{
			var contact = await _contactRepository.GetByContactIdAsync(id, ct);

			if (contact == null) return false;

			return await _contactRepository.DeleteAsync(id, ct);
		}
	}
}
