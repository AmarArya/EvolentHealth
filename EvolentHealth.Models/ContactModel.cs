using System.ComponentModel.DataAnnotations;

namespace EvolentHealth.Models
{
	public class ContactModel
	{
		[Key]
		public int ContactId { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[RegularExpression("^[0-9]*$")]
		public string PhoneNumber { get; set; }
		public string Status { get; set; }
	}
}
