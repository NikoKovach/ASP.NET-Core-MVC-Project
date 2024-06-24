//using Microsoft.EntityFrameworkCore;
//using Payroll.Data;
//using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

//namespace Payroll.Services.Services.EmployeeServices.PersonServices
//{
//     public class GetPersonService //: IGetEntities<Person>
//     {
//          PayrollContext db;
//          IEnumerable<PersonIdDto> personsDto;

//          public GetPersonService(PayrollContext context)
//          {
//               this.db = context;
//               GeneratePersons();           
//          }

//          public GetPersonService SearchByFirstName(string firstName)
//          {
//               this.personsDto = this.personsDto.Where( x => x.FirstName == firstName );

//               return this;
//          }

//          public GetPersonService SearchByLastName(string lastName)
//          {
//               this.personsDto = this.personsDto.Where( x => x.LastName == lastName );

//               return this;
//          }

//          public GetPersonService SearchByMiddleName(string middleName)
//          {
//               this.personsDto = this.personsDto.Where( x => x.MiddleName == middleName );
//               return this;
//          }

//          public GetPersonService SearchByGender(string genderType)
//          {
//               this.personsDto = this.personsDto.Where( x => x.GenderType == genderType );
//               return this;
//          }

//          public List<PersonIdDto> GetPersons()
//          {
//               return personsDto.ToList();
//          }

//          public IEnumerable<PersonIdDto> GetEntities()
//          {
//               return (IEnumerable<PersonIdDto>) personsDto;
//          }
////******************************************************************************
//          private void GeneratePersons()
//          {
//               this.personsDto = this.db.Persons
//                    .Include( x => x.Gender )
//                    .Select( p => new PersonIdDto
//                                   {
//                                        Id             = p.Id,
//                                        FirstName      = p.FirstName,
//                                        MiddleName     = p.MiddleName,
//                                        LastName       = p.LastName,
//                                        GenderType     = p.Gender.Type,
//                                        EGN            = p.EGN,
//                                   }
//                            )
//                    .OrderBy(x => x.LastName)
//                    .ThenBy(x => x.FirstName)
//                    .ThenBy(x => x.MiddleName)
//                    .ToList();
//          }
//     }
//}
