using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class ContactInfo
     {
          [Key]
          public int Id { get; set; }

          [StringLength(20,MinimumLength = 3)]
          public string? PhoneNumberOne { get; set; }


          [StringLength(20,MinimumLength = 3)]
          public string? PhoneNumberTwo { get; set; }


          [StringLength(200,MinimumLength = 3)]
          public string? E_MailAddress1 { get; set; }


          [StringLength(200,MinimumLength = 3)]
          public string? E_MailAddress2 { get; set; }


          [StringLength(200,MinimumLength = 3)]
          public string? WebSite { get; set; }

          [ForeignKey("Person")]
          public int PersonId { get; set; }
          public Person? Person { get; set; }

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
     }
}