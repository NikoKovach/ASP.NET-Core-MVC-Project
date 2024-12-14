using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payroll.Models
{
       public class Address
       {
              private const string Comma = ", ";

              [Key]
              public int Id { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              [Required]
              public string Country { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              [Required]
              public string Region { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string? Municipality { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              [Required]
              public string City { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              [Required]
              public string Street { get; set; }

              [Required]
              public int Number { get; set; }

              public string? Entrance { get; set; }

              public int? Floor { get; set; }

              public int? ApartmentNumber { get; set; }

              [InverseProperty( "PermanentAddress" )]
              public ICollection<Person> PersonPermanentAddresses { get; set; } = new List<Person>();

              [InverseProperty( "CurrentAddress" )]
              public ICollection<Person> PersonCurrentAddresses { get; set; } = new List<Person>();


              [InverseProperty( "PlaceOfRegistration" )]
              public ICollection<EmploymentContract> EmploymentContracts { get; set; } =
                         new HashSet<EmploymentContract>();

              [InverseProperty( "WorkPlace" )]
              public ICollection<EmploymentContract> WorkPlaceEmploymentContracts { get; set; } =
                         new HashSet<EmploymentContract>();

              public bool HasBeenDeleted { get; set; }

              public DateTime? DeletionDate { get; set; }

              public string? GetAddress => this.FullAddress();

              private string FullAddress()
              {
                     if ( string.IsNullOrEmpty( this.Country ) || string.IsNullOrEmpty( this.City ) )
                     {
                            return string.Empty;
                     }

                     StringBuilder sb = new StringBuilder();

                     sb.Append( this.Street );
                     sb.Append( Comma + "No." + this.Number );

                     if ( this.Entrance != null )
                     {
                            sb.Append( Comma + "Ent." + this.Entrance );
                     }

                     if ( this.Floor != null )
                     {
                            sb.Append( Comma + "Fl." + this.Floor );
                     }

                     if ( this.ApartmentNumber != null )
                     {
                            sb.Append( Comma + "Apt." + this.ApartmentNumber );
                     }

                     sb.Append( Comma + this.City + " City" );
                     sb.Append( Comma + "Reg. " + this.Region );
                     sb.Append( Comma + this.Country );
                     //##################################################################
                     return sb.ToString();
              }
       }
}


