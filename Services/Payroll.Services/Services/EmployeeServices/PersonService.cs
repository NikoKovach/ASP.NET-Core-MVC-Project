using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ViewModels;

namespace Payroll.Services.Services.CompanyServices
{
       public class PersonService : IPerson
       {
              private IRepository<Person> repository;
              private IMapEntity mapper;

              public PersonService( IRepository<Person> personsRepo, IMapEntity mapper )
              {
                     this.repository = personsRepo;

                     this.mapper = mapper;
              }

              public IQueryable<SearchPersonVM> AllActive_SearchPersonVM()
              {
                     var persons = this.mapper
                            .ProjectTo<Person, SearchPersonVM>( this.repository.AllAsNoTracking() )
                            .OrderBy( x => x.FirstName )
                            .ThenBy( x => x.LastName );

                     return persons;
              }
       }
}