using AutoMapper;
using AutoMapper.QueryableExtensions;

using Payroll.Data;
using Payroll.ModelsDto;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.CompanyServices
{
          /// <summary>
          /// TODO : FOR TESTING
          /// </summary>
     public class GetCompanyService : ICompany,IGetCompany,IGetEntities<CompanyDto>
     {
          private PayrollContext context;
          private IMapper mapper;

          public GetCompanyService( PayrollContext payrollContext, IMapper autoMapper ) : this(payrollContext)
          {
               ArgumentNullConfirmation( autoMapper,nameof(autoMapper ), 
                    GetClassName(this) ,GetClassFullName(this ));

               this.mapper = autoMapper;
          }

          public GetCompanyService( PayrollContext payrollContext )
          {
               ArgumentNullConfirmation( payrollContext,nameof(payrollContext ),
                    GetClassName(this) ,GetClassFullName( this));

               context = payrollContext;
          }

          public virtual ICollection<CompanyDto> GetAllEntities()
          {
               var companiesList = context.Companies
                                   .ProjectTo<CompanyDto>(this.mapper.ConfigurationProvider)
                                   .OrderBy( c => c.Name )
                                   .ThenBy( c => c.Id )
                                   .ToList();

               return companiesList;
          }

          public virtual ICollection<CompanyDto> GetAllValidEntities()
          {
               var companiesList = context.Companies
                                      .Where( x => x.HasBeenDeleted == false )
                                      .ProjectTo<CompanyDto>( this.mapper.ConfigurationProvider )
                                      .OrderBy( c => c.Name )
                                      .ThenBy( c => c.Id )
                                      .ToList();

               return companiesList;
          }

          public CompanyDto GetActiveCompanyByUniqueId( string companyUniqueId )
          {
               CompanyDto? company = context.Companies
                                   .Where( x => x.HasBeenDeleted == false 
                                    && x.UniqueIdentifier.Equals( companyUniqueId) )
                                   .ProjectTo<CompanyDto>( this.mapper.ConfigurationProvider )
                                   .FirstOrDefault();

               return company;
          }    
     }
}
