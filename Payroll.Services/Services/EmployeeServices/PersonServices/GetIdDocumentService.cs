using AutoMapper;
using AutoMapper.QueryableExtensions;
using Payroll.Data;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
     public class GetIdDocumentService : IGetEntitiesByEmployeeId<IdDocumentDto>,IGetEntityByNumber
     {
          private PayrollContext context;
          private IMapper mapper;

          public GetIdDocumentService(PayrollContext payrollContext, IMapper autoMapper) : this(payrollContext)
          {
               ArgumentNullConfirmation(autoMapper, nameof(autoMapper),
                    GetClassName(this), GetClassFullName(this));

               mapper = autoMapper;
          }

          public GetIdDocumentService(PayrollContext payrollContext)
          {
               ArgumentNullConfirmation(payrollContext, nameof(payrollContext),
                    GetClassName(this), GetClassFullName(this));

               context = payrollContext;
          }
          
          public ICollection<IdDocumentDto> GetAllEntitiesByEmployeeId( int employeeId )
          {
               var documentDtoList = this.context.IdDocuments
                    .Where(x => x.Person.Employee.Id == employeeId)
                    .ProjectTo<IdDocumentDto>(this.mapper.ConfigurationProvider)
                    .ToList();

               return documentDtoList;
          }

          public ICollection<IdDocumentDto> GetValidEntitiesByEmployeeId( int employeeId )
          {
               return GetAllEntitiesByEmployeeId( employeeId )
                      .Where(x => x.IsValid == true)
                      .ToList();
          }

          public IdDocumentDto? GetEntityByNumber<IdDocumentDto>( string docNumber )
          {
               IdDocumentDto? docDto = this.context.IdDocuments
                    .Where(x => x.DocumentNumber.Equals(docNumber))
                    .ProjectTo<IdDocumentDto>(this.mapper.ConfigurationProvider)
                    .FirstOrDefault();

               return docDto;
          }
     }
}
