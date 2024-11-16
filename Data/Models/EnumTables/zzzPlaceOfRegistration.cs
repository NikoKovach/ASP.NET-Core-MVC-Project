//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Payroll.Models.EnumTables
//{
//       [Table( "PlaceOfRegistrationOrWork" )]
//       public class PlaceOfRegistration
//       {
//              [Key]
//              public int Id { get; set; }

//              [StringLength( 200, MinimumLength = 3 )]
//              [Required]
//              public string Region { get; set; }

//              [StringLength( 200, MinimumLength = 3 )]
//              public string Municipality { get; set; }

//              [StringLength( 200, MinimumLength = 3 )]
//              [Required]
//              public string City { get; set; }

//              [StringLength( 200, MinimumLength = 3 )]
//              public string? Street { get; set; }

//              public int? Number { get; set; }

//              [StringLength( 3, MinimumLength = 1 )]
//              public string? Entance { get; set; }

//              public int? Floor { get; set; }

//              public int? ApartmentNumber { get; set; }

//              //[InverseProperty( "PlaceOfRegistration" )]
//              //public ICollection<EmploymentContract> EmploymentContracts { get; set; } =
//              //           new HashSet<EmploymentContract>();

//              //[InverseProperty( "WorkPlace" )]
//              //public ICollection<EmploymentContract> WorkPlaceEmploymentContracts { get; set; } =
//              //           new HashSet<EmploymentContract>();

//              public bool HasBeenDeleted { get; set; }

//              public DateTime? DeletionDate { get; set; }
//       }
//}