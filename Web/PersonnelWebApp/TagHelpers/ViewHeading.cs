using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PersonnelWebApp.TagHelpers
{
	[HtmlTargetElement( "h3", Attributes = "create-edit-heading" )]
	public class ViewHeading : TagHelper
	{
		public string? EmployeeId { get; set; }

		public override void Process( TagHelperContext context, TagHelperOutput output )
		{
			if ( string.IsNullOrEmpty( EmployeeId ) )
			{
				output.Content.SetContent( "Create Employee" );
			}
			else
			{
				output.Content.SetContent( "Edit Employee" );
			};
		}
	}
}
