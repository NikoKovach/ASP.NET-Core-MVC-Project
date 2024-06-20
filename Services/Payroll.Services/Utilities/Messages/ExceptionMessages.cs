namespace Payroll.Services.Utilities.Messages
{
     public static class ExceptionMessages
     {
          public const string UpdateCompanyExceptionMessages = "An error occurred while updating table   {0} !";

          public const string CreateCompanyExceptionMessages = "An error occurred while creating company   => {0} !";

          public const string CreateUpdatePersonExceptionMessages = "An error occurred while {0} person   with name => {1} !";

          public const string CreateUpdateDiplomaExceptionMessages = "An error occurred while {0}  diploma with number => {1} !";

          public const string EducationTypeExceptionMessages = "An error occurred while add or update    record in 'EducationTypes' table !";
          
          public const string InvalidPersonIdGetContacts = "Invalid parameter {0} !";

          //***************************************************************************

          public const string NullModelViewExceptionString = "Argument '{0}' cann't be null !\n\rMethod : {1} in object {2} .";

          public const string NullEntityExceptionString = "Entity {0} is null !\n\rMethod : {1} in object {2} .";

          public const string AddOrUpdateError = "An error occurred while adding or updating a record in database !";

          public const string InvalidEmployeeId = "Parameter : '{0}' is invalid !";

          public const string NotValidEntityId = "In table : {0} hasn't record with id : {1} .";
     }
}

    //public const string InvalidVesselName = "Vessel name cannot be null or empty.";
    //public const string InvalidCaptainToVessel = "Captain cannot be null.";
    //public const string InvalidTarget = "Target cannot be null.";
    //public const string InvalidVesselForCaptain = "Null vessel cannot be added to the captain.";

