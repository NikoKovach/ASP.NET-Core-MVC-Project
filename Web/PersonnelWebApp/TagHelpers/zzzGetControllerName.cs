//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Razor.TagHelpers;

//namespace PersonnelWebApp.TagHelpers
//{
//       [HtmlTargetElement( "button", Attributes = "caller" )]
//       [HtmlTargetElement( "input", Attributes = "caller" )]
//       public class GetControllerName : TagHelper
//       {
//              public string? FieldName { get; set; }

//              [ViewContext]
//              [HtmlAttributeNotBound]
//              public ViewContext ViewContext { get; set; }

//              public override void Process( TagHelperContext context, TagHelperOutput output )
//              {
//                     //ModelStateDictionary mState = ViewContext.ModelState;
//                     string? controllerName = ViewContext.RouteData.Values[ "controller" ].ToString();
//                     string? actionName = ViewContext.RouteData.Values[ "action" ].ToString();


//                     output.Attributes.SetAttribute( "value", controllerName );
//              }
//       }
//}
