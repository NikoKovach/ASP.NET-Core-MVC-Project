using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payroll.Models
{
     public class Address
     {
		[Key]
          public int Id { get; set; }

          [StringLength(100,MinimumLength = 3)]
          public string? AddressType { get; set; }

          [StringLength(200,MinimumLength = 3)]
          [Required]
          public string Country { get; set; }

          [StringLength(200,MinimumLength = 3)]
          [Required]
          public string Region { get; set; }

          [StringLength(200,MinimumLength = 3)]
          public string Municipality { get; set; }

          [StringLength(200,MinimumLength = 3)]
          [Required]
          public string City { get; set; }

          [StringLength(200,MinimumLength = 3)]
          [Required]
          public string Street { get; set; }

          [Required]
          public int Number { get; set; }

          public string? Entrance { get; set; }

          public int? Floor { get; set; }

          public int? ApartmentNumber { get; set; }


          [InverseProperty("PermanentAddress")]
          public ICollection<Person> PersonPermanentAddresses { get; set; } = 
			new List<Person>();

          [InverseProperty("CurrentAddress")]
          public ICollection<Person> PersonCurrentAddresesses { get; set; } = 
			new List<Person>();

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
	}
}