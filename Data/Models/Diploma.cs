using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Payroll.Models.EnumTables;

namespace Payroll.Models
{
	public class Diploma
	{
		[Key]
		public int Id { get; set; }

		[StringLength(20, MinimumLength = 3)]
		public string DiplomaRegNumber { get; set; }

		[Column(TypeName = "date")]
		public DateTime? DateOfIssue { get; set; }

		[StringLength(20, MinimumLength = 3)]
		public string? Seria { get; set; }


		[StringLength(20, MinimumLength = 3)]
		public string? SerialNumber { get; set; }


		[ForeignKey("EducationType")]
		public int EducationId { get; set; }

		public EducationType EducationType { get; set; }


		[StringLength(250, MinimumLength = 3)]
		public string? Speciality { get; set; }


		[StringLength(250, MinimumLength = 3)]
		public string? Profession { get; set; }


		[ForeignKey("Person")]
		public int? PersonId { get; set; }
		public Person? Person { get; set; }

		public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }

		//TODO : public string? EducationalInstitution { get; set; }
	}
}