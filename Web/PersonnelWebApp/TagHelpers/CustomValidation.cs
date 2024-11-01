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

                     var invalidElement = mState.Where( x => x.Value.ValidationState == ModelValidationState.Invalid
                                                                                                 && x.Key.Contains( FieldName ) )
                                                                       .Select( x => x )
                                                                       .FirstOrDefault();

                     if ( invalidElement.Value != null && invalidElement.Value.Errors.Count > 0 )
                     {
                            string kvpKey = invalidElement.Key;

                            string? valueNameAttr = output.Attributes.Where( x => x.Name.Equals( "name" ) )
                                                                                                       .Select( x => x.Value )
                                                                                                       .FirstOrDefault()
                                                                                                       .ToString();

                            if ( valueNameAttr != null && valueNameAttr.Equals( kvpKey ) )
                            {
                                   string span = "<span  class=\"text-danger\"  style=\"color:#f44336;font-size: 14px;\" > *</span>";
                                   output.PostElement.AppendHtml( span );

                                   output.Attributes.RemoveAll( "style" );
                                   output.Attributes.Add( new TagHelperAttribute( "style", "background-color:rgb(255, 179, 179);" ) );
                            }
                     }
              }
       }
}

//background-color: #f44336;

/*
//ModelStateEntry? element = mState.Where( x => x.Value.ValidationState == ModelValidationState.Invalid
                     //                                                                                                         && x.Key.Contains( FieldName ) )
                     //                                        .Select( x => x.Value )
                     //                                        .FirstOrDefault();
*/