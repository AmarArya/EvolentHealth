using EvolentHealth.Models;
using EvolentHealth.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Threading;

namespace EvolentHealth.API.Controllers
{
	public class ContactController : ApiController
	{
		private readonly IContactService _contactService;

		public ContactController(IContactService contactService)
		{
			this._contactService = contactService;
		}

		// GET: api/contact
		public async Task<HttpResponseMessage> Get(CancellationToken ct = default(CancellationToken))
		{
			try
			{
				var contacts = await _contactService.GetContacts(ct);
				return this.Request.CreateResponse(HttpStatusCode.OK, contacts);
			}
			catch (Exception ex)
			{
				return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		// GET: api/Contact/5
		[Route("api/contact/{id:int:min(1)}")]
		public async Task<HttpResponseMessage> Get(int id, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				var contact = await _contactService.GetContact(id, ct);

				if (contact == null) return this.Request.CreateResponse(HttpStatusCode.NotFound);

				return this.Request.CreateResponse(HttpStatusCode.OK, contact);
			}
			catch (Exception ex)
			{
				return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		// POST: api/Contact
		public async Task<HttpResponseMessage> Post([FromBody]ContactModel contactModel, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				if (contactModel == null)
					return this.Request.CreateResponse(HttpStatusCode.BadRequest);

				return this.Request.CreateResponse(HttpStatusCode.Created, await _contactService.AddContact(contactModel, ct));
			}
			catch (Exception ex)
			{
				return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		// PUT: api/Contact/5
		[Route("api/contact/{id:int:min(1)}")]
		public async Task<HttpResponseMessage> Put(int id, [FromBody]ContactModel contactModel, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				if (contactModel == null)
					return this.Request.CreateResponse(HttpStatusCode.BadRequest);
				if (await _contactService.GetContact(id, ct) == null)
				{
					return this.Request.CreateResponse(HttpStatusCode.NotFound);
				}

				//var errors = JsonConvert.SerializeObject(ModelState.Values
				//	.SelectMany(state => state.Errors)
				//	.Select(error => error.ErrorMessage));
				//Debug.WriteLine(errors);

				if (await _contactService.UpdateContact(contactModel, ct))
				{
					return this.Request.CreateResponse(HttpStatusCode.OK, contactModel);
				}

				return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

		}

		// DELETE: api/Contact/5
		[Route("api/contact/{id:int:min(1)}")]
		public async Task<HttpResponseMessage> Delete(int id, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				if (await _contactService.GetContact(id, ct) == null)
				{
					return this.Request.CreateResponse(HttpStatusCode.NotFound);
				}

				if (await _contactService.DeleteContact(id, ct))
				{
					return this.Request.CreateResponse(HttpStatusCode.OK);
				}

				return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
