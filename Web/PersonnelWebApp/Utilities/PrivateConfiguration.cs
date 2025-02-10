namespace PersonnelWebApp.Utilities
{
	public class PrivateConfiguration : IPrivateConfiguration
	{
		private readonly IWebHostEnvironment env;

		public PrivateConfiguration(IWebHostEnvironment environment)
		{
			this.env = environment;
		}

		public IConfigurationRoot? PrivateConfig()
		{
			string appFolderPath = this.env.ContentRootPath;

			string jsonFileName = "appsettingsSecond.json";

			var jsonFilePath = Path.Combine(appFolderPath, jsonFileName);

			var config = new ConfigurationBuilder().AddJsonFile(jsonFilePath, true, true).Build();

			return config;
		}

		public void SetPagingVariables(ref int _pageIndex, ref int _pageSize, ref int _count)
		{
			IConfigurationRoot? privateConfig = PrivateConfig();

			if (!string.IsNullOrEmpty(privateConfig["Paging:PageSize"]))
			{
				_pageSize = int.Parse(privateConfig["Paging:PageSize"]);
			}

			if (!string.IsNullOrEmpty(privateConfig["Paging:PageIndex"]))
			{
				_pageIndex = int.Parse(privateConfig["Paging:PageIndex"]);
			}

			if (!string.IsNullOrEmpty(privateConfig["Paging:Count"]))
			{
				_count = int.Parse(privateConfig["Paging:Count"]);
			}
		}

		public void SetEmployeePagingVariables(ref int _pageIndex, ref int _pageSize, ref int _count)
		{
			IConfigurationRoot? privateConfig = PrivateConfig();

			if (!string.IsNullOrEmpty(privateConfig["EmployeePaging:PageSize"]))
			{
				_pageSize = int.Parse(privateConfig["EmployeePaging:PageSize"]);
			}

			if (!string.IsNullOrEmpty(privateConfig["EmployeePaging:PageIndex"]))
			{
				_pageIndex = int.Parse(privateConfig["EmployeePaging:PageIndex"]);
			}

			if (!string.IsNullOrEmpty(privateConfig["EmployeePaging:Count"]))
			{
				_count = int.Parse(privateConfig["EmployeePaging:Count"]);
			}
		}
	}
}
