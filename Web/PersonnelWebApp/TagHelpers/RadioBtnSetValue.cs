using Microsoft.AspNetCore.Razor.TagHelpers;

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

//output.Attributes.SetAttribute( "value", "Edit" );
//output.Attributes.SetAttribute( "value", "Create" );
//output.Content.SetContent( "Create Employee" );

//else
//{
//	if ( attrValue.Equals( "Edit" ) )
//	{
//		output.Attributes.RemoveAll( "checked" );
//		output.Attributes.SetAttribute( "checked", "" );
//	}
//}

//if ( attrValue.Equals( "Create" ) )
//{
//	output.Attributes.RemoveAll( "checked" );
//	output.Attributes.SetAttribute( "checked", "" );
//}