using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class AddressEmpVM : ValidateBaseModel
       {
              private const string Comma = ", ";

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Country { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Region { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string? Municipality { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? City { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Street { get; set; }

              [Required]
              public int? Number { get; set; }

              public string? Entrance { get; set; }

              public int? Floor { get; set; }

              [Display( Name = "Apt Number" )]
              public int? ApartmentNumber { get; set; }

              protected string FullAddress()
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
