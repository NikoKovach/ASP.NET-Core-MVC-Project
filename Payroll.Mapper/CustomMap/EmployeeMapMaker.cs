using Payroll.Data;
using Payroll.Mapper.CustomMap.Contracts;
using Payroll.Mapper.Utilities;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using System.Reflection;

namespace Payroll.Mapper.CustomMap
{
     public class EmployeeMapMaker : IEmployeeMapMaker
     {
          //public Func<PayrollContext, IEnumerable<GetEmployeeDto>> CreateMap = delegate ( PayrollContext db )
          //{
          //     throw new NotImplementedException();
          //};

          public ICollection<GetEmployeeDto> EmployeeDtosMaker( PayrollContext db )
          {
               List<GetEmployeeDto> empDtoList = db.Employees
               .Select( x => new GetEmployeeDto
               {
                    EmployeeId = x.Id,
                    //IsActual = x.IsActual,
                    NumberFromTheList = x.NumberFromTheList,
                    PersonId = x.Person.Id,
                    FirstName = x.Person.FirstName,
                    MiddleName = x.Person.MiddleName,
                    LastName = x.Person.LastName,
                    EGN = x.Person.EGN,
                    GenderType = x.Person.Gender.Type,
                    PhotoFilePath = x.Person.PhotoFilePath,
               }
                      )
               .OrderBy(x => x.FirstName)
               .ThenBy(x => x.LastName)
               .ToList();

               //AddAdditionalInfo( empDtoList, db );

               //db.ChangeTracker.Clear();

               return empDtoList;
          }

          public ICollection<GetEmployeeDto> EmployeeDtosMaker( PayrollContext db,string name )
          {

               string[] arrName = name.Split(' ',StringSplitOptions.RemoveEmptyEntries);

               string firstName = arrName[0];
               string middleName = GetMiddleName( arrName );
               string lastName = GetLastName( arrName );

               List<GetEmployeeDto> employeeDto = db.Employees
                                         .Where(x => x.Person.FirstName == firstName ||                                          x.Person.MiddleName == lastName ||                                          x.Person.LastName == lastName)
                                         .Select(x =>  new GetEmployeeDto ())
                                         .OrderBy(x => x.FirstName)
                                         .ThenBy(x => x.MiddleName)
                                         .ThenBy(x => x.LastName)
                                         .ToList();

               return employeeDto;
          }

          public GetEmployeeDto? SingleEmployeeDtoMaker( PayrollContext db,int empId )
          {
               GetEmployeeDto? employeeDto = db.Employees
                                         .Where(x => x.Id == empId)
                                         .Select(x =>  new GetEmployeeDto ())
                                         .FirstOrDefault();
               return employeeDto;

          }

          public GetEmployeeDto? SingleEmployeeDtoMaker( PayrollContext db,string egnNumber )
          {
               GetEmployeeDto? employeeDto = db.Employees
                                         .Where(x => x.Person.EGN.Equals(egnNumber))
                                         .Select(x =>  new GetEmployeeDto ())
                                         .FirstOrDefault();
               return employeeDto;

          }

          public GetEmployeeDto? SingleEmployeeDtoMakerByListNumber( PayrollContext db,string numberFromTheList )
          {

               GetEmployeeDto? employeeDto = db.Employees
                                         .Where(x => x.NumberFromTheList.Equals(numberFromTheList))
                                         .Select(x => new GetEmployeeDto () )
                                         .FirstOrDefault();
               return employeeDto;
          }


          

 //***************************************************************************************
          //private void AddAdditionalInfo( List<GetEmployeeDto> empList, PayrollContext db )
          //{
          //     foreach ( var item in empList )
          //     {
          //          GetContacts( item, db );
          //          item.PermanentAddress = GetAddress( item.EmployeeId, db,AddressType.Permanent.ToString());
          //          item.CurrentAddress = GetAddress( item.EmployeeId, db,AddressType.Present.ToString() );
          //          item.GetJobTitle( db, item.EmployeeId )
          //              .GetDocumentName( db, item.EmployeeId )
          //              .GetDocumentNumber( db, item.EmployeeId )
          //              .GetDepartmentName( db, item.EmployeeId );
          //     }
          //}

          private void GetContacts( GetEmployeeDto empDto, PayrollContext db )
          {
               var contactDto = db.ContactInfos
                    .Where( x => x.PersonId == empDto.PersonId && x.HasBeenDeleted == false )
                    .Select( x => new ContactInfoDto
                    {
                         PhoneNumberOne = x.PhoneNumberOne,
                         PhoneNumberTwo = x.PhoneNumberTwo,
                         E_MailAddress1 = x.E_MailAddress1,
                         WebSite = x.WebSite,
                    }
                           )
                    .FirstOrDefault();

               if ( contactDto == null )
               {
                    return;
               }

               empDto.PhoneNumberOne = contactDto.PhoneNumberOne;
               empDto.PhoneNumberTwo = contactDto.PhoneNumberTwo;
               empDto.E_MailAddress1 = contactDto.E_MailAddress1;
               empDto.WebSite = contactDto.WebSite;
          }

          //private string? GetAddress( int empId, PayrollContext db,string addressType )//Address address
          //{
          //     //TODO : Refactoring
          //     List<string> itemsList = new List<string>();

          //     int? addressId = GetAddressId( empId, db,addressType );

          //     Address? address = db.Addresses
          //          .Where( x => x.Id == addressId )
          //          .FirstOrDefault();

          //     if ( address == null )
          //     {
          //          return string.Empty;
          //     }
          //     //*****************************************************************************
          //     PropertyInfo[] arrProperties = address.GetType().GetProperties();

          //     foreach ( var item in arrProperties )
          //     {
          //          if ( item.Name == "Id" || item.Name == "PersonPermanentAddresses" ||
          //               item.Name == "PersonCurrentAddresesses" || item.Name == "HasBeenDeleted" )
          //          {
          //               continue;
          //          }
          //          IDictionary<string, string> addressPrefix = new AddressTranslate().GetAddressPrefix();

          //          object? value = item.GetValue( address );

          //          if ( value == null )
          //          {
          //               continue;
          //          }

          //          itemsList.Add( addressPrefix[ item.Name ] + value.ToString() );
          //     }

          //     //var addressText = string.Join( " ,", itemsList );
          //     return string.Join( " ,", itemsList );
          //}

          //private int? GetAddressId( int empId, PayrollContext db,string addressType )
          //{
          //     int? addressId = 0;

          //     if ( addressType == "Permanent" )
          //     {
          //          addressId = db.Persons
          //          .Where( x => x.EmployeeId == empId )
          //          .Select( x => x.PermanentAddressId )
          //          .FirstOrDefault();
          //     }
          //     else if ( addressType == "Present" )
          //     {
          //          addressId = db.Persons
          //          .Where( x => x.EmployeeId == empId )
          //          .Select( x => x.CurrentAddressId )
          //          .FirstOrDefault();
          //     }

          //     return addressId;
          //}

          private string GetLastName( string[] arrName )
          {
               if ( arrName.Length < 2 )
               {
                    return string.Empty;
               }

               return arrName[ arrName.Length - 1 ];
          }

          private string GetMiddleName( string[] arrName )
          {
               throw new NotImplementedException();
          }
     }
}

          //public Func<Employee, GetEmployeeDto> CreateMap = delegate (Employee emp)
          //{
          //     var empDto = new GetEmployeeDto
          //     {
          //          EmployeeId          = emp.Id,
          //          NumberFromTheList   = emp.NumberFromTheList,
          //          PersonId            = emp.Person.Id,
          //          FirstName           = emp.Person.FirstName,
          //          MiddleName          = emp.Person.MiddleName,
          //          LastName            = emp.Person.LastName,
          //          EGN                 = emp.Person.EGN,
          //          GenderType          = emp.Person.Gender.Type,
          //          PhotoFilePath       = emp.Person.PhotoFilePath,
          //     };

          //     return empDto;
          //}; 
