using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Payroll.Models.EnumTables;

namespace Payroll.Models
{
    public class Person
     {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(100,MinimumLength = 3)]
          public string FirstName { get; set; }

          [StringLength(100,MinimumLength =3)]
          public string? MiddleName { get; set; }

          [Required]
          [StringLength(100,MinimumLength =3)]
          public string LastName { get; set; }

          /// <summary>
          /// United civil number.
          /// </summary>
          [Required]
          [MaxLength(10)]
          public string EGN { get; set; }

		public string? PhotoFilePath { get; set; }


          [ForeignKey("Gender")]
          public int? GenderId { get; set; }
          public Gender? Gender { get; set; }


          [ForeignKey( "Address" )]
          public int? PermanentAddressId { get; set; }
          public  Address? PermanentAddress { get; set; }


          [ForeignKey( "Address" )]
          public int? CurrentAddressId { get; set; }
          public  Address? CurrentAddress { get; set; }

		public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

          public ICollection<ContactInfo> ContactInfoList { get; set; } = new HashSet<ContactInfo>();

          public ICollection<IdDocument> IdDocuments { get; set; } = new HashSet<IdDocument>();

          public ICollection<Diploma>? Diplomas { get; set; } = new HashSet<Diploma>();

     }
}
