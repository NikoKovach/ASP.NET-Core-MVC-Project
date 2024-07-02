using System.Text.RegularExpressions;

namespace CommonServices
{
	public static class EnvironmentService
	{
		public static void CreateFolder( string rootPath,string folderName )
		{
			string modifiedFolderName = ModifyFolderName(folderName);

			if ( DirectoryExists(rootPath,modifiedFolderName) )
			{
				return;
			}

			Directory.CreateDirectory( NewFolderPath(rootPath,modifiedFolderName) );
		}

		public static string ModifyFolderName( string folderName )
		{
			string pattern = @"[\w+]+";

			MatchCollection? regexResult = Regex.Matches( folderName,pattern );

			string? result = string.Join( '-', regexResult );

			return result;
		}

		public static bool DirectoryExists(string rootPath,string modifiedFolderName)
		{ 
			string newFolderPath = NewFolderPath(rootPath,modifiedFolderName);

			if ( !Directory.Exists( newFolderPath ) )
				return false;

			return true;
		}

		public static string NewFolderPath(string rootPath,string modifiedFolderName)
		{
			string newFolderPath = Path.Combine(rootPath,modifiedFolderName);

			return newFolderPath;
		}
	}
}

/*
 int index = folderName.IndexOf('"');

			while ( index != -1 )
			{
				folderName = folderName.Remove(index,1);
				index = folderName.IndexOf('"');
			}

			List<string> folderNameList = folderName
							 .Trim()
							 .ToUpper()
							 .Split( ' ', StringSplitOptions.RemoveEmptyEntries )
							 .ToList();

			string newCompanyName = string.Join( '-' ,folderNameList);		

			return newCompanyName;
 
 */