namespace Payroll.Services.UtilitiesServices.Messages
{
	public static class OutputMessages
	{
		public const string SuccessfullyCreateNewRecord = "A new record was create successfully in table : {0}.";

		public const string SuccessfullyUpdateRecord = "Record in table : {0} was update successfully .";

		public const string ErrorValueExists = "{0} already exists in database !";

		public const string ErrorNotANumber = "The property value must be a number !";

		public const string ErrorNumberIsNegative = "The property value must be a positive number !";
	}
}