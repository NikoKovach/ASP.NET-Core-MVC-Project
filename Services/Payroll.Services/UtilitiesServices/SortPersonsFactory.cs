using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class SortPersonsFactory
       {
              private IMapEntity mapper;
              private IQueryable<Person> personsCollection;

              public SortPersonsFactory( IMapEntity mapper, IQueryable<Person> personsList )
              {
                     this.mapper = mapper;

                     this.personsCollection = personsList;
              }

              public IQueryable<PersonViewModel> Sort( string sortParameter )
              {
                     IQueryable<PersonViewModel> persons = default;
                     switch ( sortParameter )
                     {
                            case "FirstName_desc":
                            persons = this.mapper
                                                     .ProjectTo<Person, PersonViewModel>( this.personsCollection )
                                                     .OrderByDescending( x => x.FirstName );
                            break;
                            case "LastName_desc":
                            persons = this.mapper
                                                     .ProjectTo<Person, PersonViewModel>( this.personsCollection )
                                                     .OrderByDescending( x => x.LastName );
                            break;
                            case "date_desc":

                            break;
                            default:

                            break;
                     }

                     return persons;
              }
       }
}
