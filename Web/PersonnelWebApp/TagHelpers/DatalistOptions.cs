using Microsoft.AspNetCore.Razor.TagHelpers;
using Payroll.Models.EnumTables;

namespace PersonnelWebApp.TagHelpers
{
       [HtmlTargetElement( "datalist", Attributes = "options-list" )]
       public class DisplayAddresses : TagHelper
       {

              public override void Process( TagHelperContext context, TagHelperOutput output )
              {
                     List<AddressType>? addressTypesList = Enum.GetValues( typeof( AddressType ) ).Cast<AddressType>().ToList();

                     string datalistOptions = string.Empty;

                     foreach ( var item in addressTypesList )
                     {
                            datalistOptions += $"<option>{item}</option>";
                     }

                     output.Content.AppendHtml( datalistOptions );
              }
       }
}

//output.PostElement.AppendHtml( datalistOptions );
//<option>Permanent</option>
//<option>Current</option>
//string span = "<span  class=\"text-danger\"  style=\"color:#f44336;font-size: 14px;\" > *</span>";
//output.PostElement.AppendHtml( span );