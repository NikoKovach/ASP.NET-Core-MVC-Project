using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PersonnelWebApp.TagHelpers
{
       [HtmlTargetElement( "h3", Attributes = "create-edit-heading" )]
       [HtmlTargetElement( "label", Attributes = "create-edit-heading", ParentTag = "legend" )]
       public class ViewHeading : TagHelper
       {
              public string? EntityId { get; set; }

              public string? ControllerName { get; set; }

              public override void Process( TagHelperContext context, TagHelperOutput output )
              {
                     if ( !string.IsNullOrEmpty( this.ControllerName ) )
                     {
                            if ( this.ControllerName.Equals( "Employees" ) )
                            {
                                   string text = ControllerNameWithoutS();

                                   ChangeOutputContent( output, text );
                            }

                            if ( this.ControllerName.Equals( "Agreements" ) )
                            {
                                   string text = ControllerNameWithoutS();

                                   ChangeOutputContent( output, text );
                            }
                     }
              }

              private void ChangeOutputContent( TagHelperOutput output, string? text )
              {
                     if ( string.IsNullOrEmpty( EntityId ) )
                     {
                            output.Content.SetContent( $"Create {text}" );
                     }
                     else
                     {
                            output.Content.SetContent( $"Edit {text}" );
                     };
              }

              private string ControllerNameWithoutS()
              {
                     var lastChar = this.ControllerName[ this.ControllerName.Length - 1 ].ToString();

                     if ( lastChar.Equals( "s" ) )
                     {
                            int lenght = this.ControllerName.Length - 1;

                            string text = this.ControllerName.Substring( 0, lenght );

                            return text;
                     }

                     return this.ControllerName;
              }
       }
}
