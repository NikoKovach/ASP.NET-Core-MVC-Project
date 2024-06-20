using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.ModelsDto;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.CompanyServices
{
	/// <summary>
     /// TODO : FOR TESTING
     /// </summary>
     public class CompanyService : ICompany,IGetCompany
     {
          private PayrollContext context;
          private IMapper mapper;

          public CompanyService( PayrollContext payrollContext, IMapper autoMapper ) : this(payrollContext)
          {
               ArgumentNullConfirmation( autoMapper,nameof(autoMapper ), 
                    GetClassName(this) ,GetClassFullName(this ));

               this.mapper = autoMapper;
          }

          public CompanyService( PayrollContext payrollContext )
          {
               ArgumentNullConfirmation( payrollContext,nameof(payrollContext ),
                    GetClassName(this) ,GetClassFullName( this));

               context = payrollContext;
          }

          public virtual async Task<ICollection<CompanyDto>> GetAllEntitiesAsync()
          {
               var companiesList = await context.Companies
                                   .ProjectTo<CompanyDto>(this.mapper.ConfigurationProvider)
                                   .OrderBy( c => c.Name )
                                   .ThenBy( c => c.Id )
                                   .ToListAsync();

               return companiesList;
          }

          public virtual async Task<ICollection<CompanyDto>> GetAllValidEntitiesAsync()
          {
               var companiesList = await context.Companies
                                      .Where( x => x.HasBeenDeleted == false )
                                      .ProjectTo<CompanyDto>( this.mapper.ConfigurationProvider )
                                      .OrderBy( c => c.Name )
                                      .ThenBy( c => c.Id )
                                      .ToListAsync();

               return companiesList;
          }

          public async Task<CompanyDto> GetActiveCompanyByUniqueIdAsync( string companyUniqueId )
          {
               CompanyDto? company = await context.Companies
                                   .Where( x => x.HasBeenDeleted == false 
                                    && x.UniqueIdentifier.Equals( companyUniqueId) )
                                   .ProjectTo<CompanyDto>( this.mapper.ConfigurationProvider )
                                   .FirstOrDefaultAsync();

               return company;
          }    
     }
}
