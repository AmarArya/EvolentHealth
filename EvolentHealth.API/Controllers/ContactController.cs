using EvolentHealth.Models;
using EvolentHealth.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Threading;
using Swagger.Net.Annotations;
using EvolentHealth.CrossCutting.Logging.LogInterface;

namespace EvolentHealth.API.Controllers
{
	/// <summary>
	///  Contact controller/endpoint to perfome CRUD operation of Contact
	/// </summary>
	public class ContactController : ApiController
	{
		private readonly IContactService _contactService;
		private readonly ILogger _logger;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contactService"></param>
		public ContactController(IContactService contactService, ILogger logger)
		{
			this._contactService = contactService;
			this._logger = logger;
		}

		// GET: api/contact
		/// <summary>
		/// retrieve the contact detail by their ID.
		/// </summary>
		/// <param name="ct"></param>
		/// <returns></returns>
		[SwaggerResponse(HttpStatusCode.OK, Type =  typeof(IEnumerable<ContactModel>))]
		[SwaggerResponse(HttpStatusCode.InternalServerError)]
		[SwaggerResponse(HttpStatusCode.NoContent)]
		public async Task<IHttpActionResult> Get(CancellationToken ct = default(CancellationToken))
		{
			try
			{
				var contacts = await _contactService.GetContacts(ct);
				if (contacts == null) return NotFound();

				return Ok(contacts);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(HttpStatusCode.InternalServerError);
			}
		}

		// GET: api/Contact/5
		/// <summary>
		///   etrieve the contacts details by their ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		[Route("api/contact/{id:int:min(1)}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ContactModel))]
		[SwaggerResponse(HttpStatusCode.InternalServerError)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		public async Task<IHttpActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				var contact = await _contactService.GetContact(id, ct);

				if (contact == null) return NotFound();

				return Ok(contact);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(HttpStatusCode.InternalServerError);
			}
		}

		// POST: api/Contact
		/// <summary>
		/// 	   add contact detail.
		/// </summary>
		/// <param name="contactModel"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ContactModel))]
		[SwaggerResponse(HttpStatusCode.InternalServerError)]
		[SwaggerResponse(HttpStatusCode.BadRequest)]
		public async Task<IHttpActionResult> Post([FromBody]ContactModel contactModel, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				if (contactModel == null || !ModelState.IsValid)
					return BadRequest(ModelState);

				return Ok(await _contactService.AddContact(contactModel, ct));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(HttpStatusCode.InternalServerError);
			}
		}

		
		// PUT: api/Contact/5
		/// <summary>
		/// update contact by their id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="contactModel"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		[Route("api/contact/{id:int:min(1)}")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ContactModel))]
		[SwaggerResponse(HttpStatusCode.InternalServerError)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		[SwaggerResponse(HttpStatusCode.BadRequest)]
		public async Task<IHttpActionResult> Put(int id, [FromBody]ContactModel contactModel, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				if (contactModel == null || ModelState.IsValid)
					return BadRequest(ModelState);
				if (await _contactService.GetContact(id, ct) == null)
				{
					return NotFound();
				}

				if (await _contactService.UpdateContact(contactModel, ct))
				{
					return Ok(contactModel);
				}

				return StatusCode(HttpStatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(HttpStatusCode.InternalServerError);
			}

		}

		// DELETE: api/Contact/5
		/// <summary>
		/// 	delete contact by their id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		[Route("api/contact/{id:int:min(1)}")]
		[SwaggerResponse(HttpStatusCode.OK)]
		[SwaggerResponse(HttpStatusCode.InternalServerError)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		public async Task<IHttpActionResult> Delete(int id, CancellationToken ct = default(CancellationToken))
		{
			try
			{
				if (await _contactService.GetContact(id, ct) == null)
				{
					return NotFound();
				}

				if (await _contactService.DeleteContact(id, ct))
				{
					return Ok();
				}

				return StatusCode(HttpStatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(HttpStatusCode.InternalServerError);
			}
		}
	}
}
