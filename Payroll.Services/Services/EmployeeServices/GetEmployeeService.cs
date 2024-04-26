using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Mapper.CustomMap;
using Payroll.Mapper.CustomMap.Contracts;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.Services.Services.AuthenticServices;
using Payroll.Services.Services.ServiceContracts;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.EmployeeServices
{
     public class GetEmployeeService :
          IGetEntities<GetEmployeeDto>,IGetEntityById<GetEmployeeDto>,ISearchEmployee
          
     {
          /// <summary>
          /// TODO : FOR TESTING
          /// </summary>
          /// 
          private PayrollContext context;
          private IEmployeeMapMaker empMaker;
          private IMapper mapper;

          public GetEmployeeService( PayrollContext payrollContext,IEmployeeMapMaker maker ) : this(payrollContext)
          {
               ArgumentNullConfirmation( maker,nameof(maker ),
                                         GetClassName(this) ,GetClassFullName( this));

               this.empMaker = maker;
          }

          public GetEmployeeService( PayrollContext payrollContext )
          {
               ArgumentNullConfirmation( payrollContext,nameof(payrollContext ),
                                         GetClassName(this) ,GetClassFullName( this));

               context = payrollContext;   
          }
          
          public ICollection<GetEmployeeDto> GetAllEntities()
          {
               ICollection<GetEmployeeDto> empDtoList = this.empMaker.EmployeeDtosMaker( this.context );
               
               return empDtoList;
          }

          public ICollection<GetEmployeeDto> GetAllValidEntities()
          {
               ICollection<GetEmployeeDto> allEmployees = GetAllEntities();

               ICollection<GetEmployeeDto> validEmployees = allEmployees
                    .Where( x => x.IsActual == true ).ToList();
               
               return validEmployees;
          }

          public ICollection<GetEmployeeDto> GetEmployeeByName( string name )
          {
               ArgumentNullConfirmation( name, nameof( name ),
                    nameof(GetEmployeeByName) ,GetClassFullName(this ));

               ICollection<GetEmployeeDto> empDtoList = this.empMaker.EmployeeDtosMaker( this.context,name );

               return empDtoList;
          }

          public GetEmployeeDto GetEntityById( int entityId )
          {
               EmpConfirmation.ValidateEmpId( this.context, entityId );

                GetEmployeeDto? employeeDto = this.empMaker.SingleEmployeeDtoMaker( this.context,entityId );
               
               return employeeDto;
          }

          public GetEmployeeDto GetEmployeeByEGN( string egnNumber )
          {
               ArgumentNullConfirmation( egnNumber, nameof( egnNumber ),
                    nameof(GetEmployeeByEGN) ,GetClassFullName(this ));

               GetEmployeeDto? employeeDto = this.empMaker.SingleEmployeeDtoMaker( this.context,egnNumber );

               return employeeDto;
          }

          public GetEmployeeDto GetEmployeeByListNumber( string numberFromTheList )
          {
               ArgumentNullConfirmation( numberFromTheList, nameof( numberFromTheList ),
                    nameof(GetEmployeeByListNumber) ,GetClassFullName(this ));

               GetEmployeeDto? employeeDto = this.empMaker.SingleEmployeeDtoMakerByListNumber( this.context,numberFromTheList );

               return employeeDto;
          }

     }
}
