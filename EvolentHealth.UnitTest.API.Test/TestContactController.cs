using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EvolentHealth.API.Controllers;
using EvolentHealth.Models;
using EvolentHealth.ServiceInterface;
using EvolentHealth.Repository.RepositoryInterface;
using EvolentHealth.Service;
using EvolentHealth.Entities;
using EvolentHealth.CrossCutting.Logging.LogInterface;

namespace EvolentHealth.UnitTest.API.Test
{
	[TestClass]
	public class TestContactController
	{
		#region Variables
		IContactService _contactService;
		IContactRepository _contactRepository;
		ILogger _logger;
		ContactController _contactController;
		List<Contact> _contactList;
		#endregion

		#region TestInitialize
		[TestInitialize]
		public void Initialize()
		{
			_contactList = InitializeContacts();

			_contactRepository = InitializeContactRepository();
			_contactService = new ContactService(_contactRepository);
			_contactController = new ContactController(_contactService, _logger);
		}

		public List<Contact> InitializeContacts()
		{
			List<Contact> _contacts = new List<Contact>();
			_contacts.Add(new Contact() { ContactId = 1, FirstName = "Amar", LastName = "Arya", PhoneNumber = "9860011165", Status = "Active" });
			_contacts.Add(new Contact() { ContactId = 2, FirstName = "Shiva", LastName = "Shakti", PhoneNumber = "9860011165", Status = "Active" });
			_contacts.Add(new Contact() { ContactId = 3, FirstName = "Aadi", LastName = "Shakti", PhoneNumber = "9860011165", Status = "Active" });

			return _contacts;
		}

		public IContactRepository InitializeContactRepository()
		{
			// Init repository
			var repo = new Mock<IContactRepository>();

			// Setup mocking behavior
			repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(InitializeContacts());

			repo.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
							.ReturnsAsync((int id, CancellationToken ct) =>
								_contactList.Find(x => x.ContactId.Equals(id)));

			repo.Setup(r => r.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
						   .Callback(new Action<Contact, CancellationToken>((newContact, ct) =>
						   {
							   _contactList.Add(newContact);
						   })).ReturnsAsync(new Contact());

			repo.Setup(r => r.UpdateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
				.Callback(new Action<Contact, CancellationToken>((x, ct) =>
				{
					var contact = _contactList.Find(a => a.ContactId == x.ContactId);
					contact.FirstName = x.FirstName;
					contact.LastName = x.LastName;
					contact.PhoneNumber = x.PhoneNumber;
					contact.Status = x.Status;
				}));

			repo.Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
				.Callback((int id, CancellationToken ct) =>
				{
					var _contact = _contactList.Find(a => a.ContactId == id);

					if (_contact != null)
						_contactList.Remove(_contact);
				}).ReturnsAsync(true);

			// Return mock implementation
			return repo.Object;
		}

		#endregion

		#region TestMethoda
		[TestMethod]
		public async Task TestGetOK()
		{

			var result = await _contactController.Get() as OkNegotiatedContentResult<IEnumerable<ContactModel>>;

			Assert.IsNotNull(result);
			//Assert.AreEqual(result, _contactList);

		}

		[TestMethod]
		public async Task TestGetByIdOK()
		{
			var result = await _contactController.Get(1) as OkNegotiatedContentResult<ContactModel>;

			Assert.IsNotNull(result);
			//Assert.AreEqual(result, _contactList);

		}


		[TestMethod]
		public async Task TestGetByIdNoFound()
		{
			var result = await _contactController.Get(5);

			Assert.IsInstanceOfType(result, typeof(NotFoundResult));

		}


		[TestMethod]
		public async Task TestPostOk()
		{
			IHttpActionResult result = await _contactController.Post(new ContactModel() { FirstName = "Unit", LastName = "Test" });

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task TestPutOk()
		{

			IHttpActionResult result = await _contactController.Put(2, new ContactModel() { ContactId = 2, FirstName = "Unit", LastName = "Test", PhoneNumber = "9871233218" });
			var contentResult = result as NegotiatedContentResult<ContactModel>;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task TestDeleteOk()
		{
			IHttpActionResult result = await _contactController.Delete(1);

			Assert.IsInstanceOfType(result, typeof(OkResult));
		}

		#endregion
	}
}
