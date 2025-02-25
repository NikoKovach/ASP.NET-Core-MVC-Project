﻿namespace Payroll.Services.UtilitiesServices.Messages
{
       public static class OutputMessages
       {
              public const string SuccessfullyCreateNewRecord = "A new record was create successfully in table : {0}.";

              public const string SuccessfullyUpdateRecord = "Record in table : {0} was update successfully .";

              public const int MinImageSizeInBytes = 1024;

              public const string ErrorValueExists = "The number {0} already exists in database !";

              public const string ErrorNotANumber = "The field value must be a number !";

              public const string ErrorNumberIsNegative = "The field value must be a positive number !";

              public const string ErrorInvalidFile = "File is invalid !";

              public const string ErrorFileFormat = "Invalid file type.Please upload {0} file.";

              public const string ErrorFileSize = "The file size must be between ({0} KB) and ({1} MB).";

              public const string ErrorFileContent = "File contains invalid content !";

              public const string ErrorFieldIsRequired = "The field '{0}' is required !";

              public const string ErrorAddressIdNotExists = "There is no record with the given 'ID' in Address table !";

              public const string ErrorAddressType = "The type of address is not selected !";

              public const string ErrorIDIsNotZero = "The property value is invalid  !";

              public const string ErrorContractCannotBeEdit = @"Contract with No :{0} cannot be edited directly.
              To do this, use an annex or another type of document !";

       }
}