using AutoMapper;
using AutoMapper.QueryableExtensions;
using Payroll.Data;
using Payroll.ModelsDto.PersonViewModels;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
    /// <summary>
    /// TODO : FOR TESTING
    /// </summary>
    public class GetContactInfoService : IGetEntityById<ContactInfoDto>
     {
          private PayrollContext context;
          private IMapper mapper;

          public GetContactInfoService(PayrollContext payrollContext, IMapper autoMapper) : this(payrollContext)
          {
               ArgumentNullConfirmation(autoMapper, nameof(autoMapper),
                    GetClassName(this), GetClassFullName(this));

               mapper = autoMapper;
          }

          public GetContactInfoService(PayrollContext payrollContext)
          {
               ArgumentNullConfirmation(payrollContext, nameof(payrollContext),
                    GetClassName(this), GetClassFullName(this));

               context = payrollContext;
          }

          public ContactInfoDto GetEntityById(int personId)
          {
               var contactDto = context.ContactInfos
                    .Where(x => x.Person.Id == personId && x.HasBeenDeleted == false)
                    .ProjectTo<ContactInfoDto>(mapper.ConfigurationProvider)
                    .FirstOrDefault();

               return contactDto;
          }
     }
}
