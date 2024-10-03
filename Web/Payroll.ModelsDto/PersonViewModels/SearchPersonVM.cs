using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Payroll.ViewModels.PersonViewModels
{
       public class SearchPersonVM : ValidateBaseModel
       {
              [Display( Name = "Id" )]
              [Range( 1, int.MaxValue )]
              public int? PersonId { get; set; }

              public string? FirstName { get; set; }

              public string? MiddleName { get; set; }

              public string? LastName { get; set; }

              [Display( Name = "Civil Number" )]
              [StringLength( 10, MinimumLength = 2 )]
              public string? CivilID { get; set; }

              public override string ToString()
              {
                     StringBuilder sb = new StringBuilder();

                     sb.Append( FirstName + " " );

                     if ( !string.IsNullOrEmpty( MiddleName ) )
                     {
                            sb.Append( MiddleName + " " );
                     }

                     sb.Append( LastName + " " );
                     sb.Append( "-> Civil number : " + CivilID );

                     return sb.ToString();
              }
       }
}