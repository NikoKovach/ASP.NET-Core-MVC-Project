using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class AddressEmpVM
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

              public int? ApartmentNumber { get; set; }
       }
}

/*
public override string ToString()
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

*/