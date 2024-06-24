using AutoMapper;
using AutoMapper.QueryableExtensions;
using Payroll.Data;
using Payroll.ModelsDto.PersonViewModels;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
    public class GetDiplomaService : IGetEntitiesByEmployeeId<DiplomaDto>
     {
          private PayrollContext context;
          private IMapper mapper;

          public GetDiplomaService(PayrollContext payrollContext, IMapper autoMapper) : this(payrollContext)
          {
               ArgumentNullConfirmation(autoMapper, nameof(autoMapper),
                    GetClassName(this), GetClassFullName(this));

               mapper = autoMapper;
          }

          public GetDiplomaService(PayrollContext payrollContext)
          {
               ArgumentNullConfirmation(payrollContext, nameof(payrollContext),
                    GetClassName(this), GetClassFullName(this));

               context = payrollContext;
          }

		//For Change
          public ICollection<DiplomaDto> GetAllEntitiesByEmployeeId(int employeeId)
          {
               var diplomaDtoList = context.Diplomas
                    .Where(x => x.Person.Id == employeeId)
                    .ProjectTo<DiplomaDto>(mapper.ConfigurationProvider)
                    .OrderBy(x => x.PersonId)
                    .ThenBy(x => x.EducationTypeName)
                    .ToList();

               return diplomaDtoList;
          }

          public ICollection<DiplomaDto> GetValidEntitiesByEmployeeId(int employeeId)
          {
               var diplomaDtoList = GetAllEntitiesByEmployeeId(employeeId);

               diplomaDtoList.Where(x => x.HasBeenDeleted == false);

               return diplomaDtoList;
          }
     }
}
