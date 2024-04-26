using AutoMapper;
using AutoMapper.QueryableExtensions;
using Payroll.Data;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
     public class GetAddressService : IGetEntityById<AddressDto>
     {
          private PayrollContext context;
          private IMapper mapper;

          public GetAddressService(PayrollContext payrollContext, IMapper autoMapper) : this(payrollContext)
          {
               ArgumentNullConfirmation(autoMapper, nameof(autoMapper),
                    GetClassName(this), GetClassFullName(this));

               mapper = autoMapper;
          }

          public GetAddressService(PayrollContext payrollContext)
          {
               ArgumentNullConfirmation(payrollContext, nameof(payrollContext),
                    GetClassName(this), GetClassFullName(this));

               context = payrollContext;
          }

          public AddressDto GetEntityById(int personId)
          {
               AddressDto? view;

               int? presentAddressId = context.Persons
                    .Where(x => x.Id == personId)
                    .Select(x => x.CurrentAddressId)
                    .FirstOrDefault();

               if (presentAddressId == null || presentAddressId == 0)
               {
                    view = context.Persons
                         .Where(x => x.Id == personId)
                         .Select(x => x.PermanentAddress)
                         .ProjectTo<AddressDto>(mapper.ConfigurationProvider)
                         .FirstOrDefault();
                    return view;
               }

               view = context.Persons
                         .Where(x => x.Id == personId)
                         .Select(x => x.CurrentAddress)
                         .ProjectTo<AddressDto>(mapper.ConfigurationProvider)
                         .FirstOrDefault();

               return view;
          }
     }
}
