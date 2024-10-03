using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PersonnelWebApp.TagHelpers
{
       [HtmlTargetElement( "input", Attributes = "validate" )]
       public class CustomValidation : TagHelper
       {
              public string? FieldName { get; set; }

              [ViewContext]
              [HtmlAttributeNotBound]
              public ViewContext ViewContext { get; set; }

              public override void Process( TagHelperContext context, TagHelperOutput output )
              {
                     ModelStateDictionary mState = ViewContext.ModelState;

                     ModelStateEntry? element = mState.Where( x => x.Value.ValidationState == ModelValidationState.Invalid
                                                                                                                              && x.Key.Contains( FieldName ) )
                                                             .Select( x => x.Value )
                                                             .FirstOrDefault();


                     if ( element != null && element.Errors.Count > 0 )
                     {
                            string span = "<span  class=\"text-danger\"  style=\"color:#f44336;font-size: 14px;\" > *</span>";
                            output.PostElement.AppendHtml( span );

                            output.Attributes.RemoveAll( "style" );
                            output.Attributes.Add( new TagHelperAttribute( "style", "background-color:rgb(255, 179, 179);" ) );
                     }
              }
       }
}

//background-color: #f44336;
/*

*/