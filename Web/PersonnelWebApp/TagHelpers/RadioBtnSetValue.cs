﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PersonnelWebApp.TagHelpers
{
       [HtmlTargetElement( "input", Attributes = "[name=create-update]" )]
       public class RadioBtnSetValue : TagHelper
       {
              public string? EmployeeId { get; set; }

              public override void Process( TagHelperContext context, TagHelperOutput output )
              {
                     string? attrValue = context.AllAttributes[ "value" ].Value.ToString();

                     if ( !string.IsNullOrEmpty( EmployeeId ) )
                     {
                            output.Attributes.RemoveAll( "checked" );

                            if ( attrValue.Equals( "Edit" ) )
                            {
                                   output.Attributes.SetAttribute( "checked", "" );
                            }
                     }
              }
       }
}
