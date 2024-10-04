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

              [StringLength( 100, MinimumLength = 3 )]
              public string? AddressType { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              [Required]
              public string Country { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              [Required]
              public string Region { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string Municipality { get; set; }

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
              public ICollection<Person> PersonCurrentAddresesses { get; set; } = new List<Person>();

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
                     sb.Append( this.Country + Comma );
                     sb.Append( this.Region + Comma );

                     if ( this.Municipality != null )
                     {
                            sb.Append( this.Municipality + Comma );
                     }

                     sb.Append( this.City + Comma );
                     sb.Append( this.Street + Comma );

                     sb.Append( this.Number );

                     if ( this.Entrance != null || this.Floor != null ||
                            this.ApartmentNumber != null )
                     {
                            sb.Append( Comma );
                     }

                     if ( this.Entrance != null )
                     {
                            sb.Append( this.Entrance + Comma );
                     }

                     if ( this.Floor != null )
                     {
                            sb.Append( this.Floor + Comma );
                     }

                     if ( this.ApartmentNumber != null )
                     {
                            sb.Append( this.ApartmentNumber );
                     }

                     return sb.ToString();
              }
       }
}