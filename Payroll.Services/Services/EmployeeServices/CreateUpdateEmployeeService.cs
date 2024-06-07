using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

namespace Payroll.Services.Services.EmployeeServices
{
     public class CreateUpdateEmployeeService : AddUpdateEntity
     {
          /// <summary>
          /// TODO : FOR TESTING
          /// </summary>
          /// 

          public CreateUpdateEmployeeService( PayrollContext payrollContext, IMapper autoMapper) : base( payrollContext,autoMapper )
          {
          }

          //public override Employee CreateObject<Employee,EmployeeDto>( EmployeeDto empDto )
          //{
          //     Gender? gender = SetGender( empDto.Person.GenderType );
               
          //     Employee emp = this.Mapper.Map<Employee>(empDto);

          //     emp.Person.Gender = gender;
          //     emp.Person.GenderId = gender.Id;

          //     return emp;
          //}

          //public override async Task<bool> UpdateRecordAsync<Employee>( Employee emp )
          //{
          //     try
          //     {
          //          var updatedEntity = this.Context.Entry( emp );
          //          var personEntity  = this.Context.Entry( emp.Person );

          //          if (updatedEntity.State == EntityState.Detached)
          //          {
          //               DbSet<Employee> empDbSet = this.Context.Set<Employee>();
          //               empDbSet.Attach(emp);
          //          }

          //          if (personEntity.State == EntityState.Detached)
          //          {
          //               DbSet<Models.Person> empDbSet = this.Context.Set<Models.Person>();
          //               empDbSet.Attach(emp.Person);
          //          }

          //          updatedEntity.State = EntityState.Modified;
          //          personEntity.State = EntityState.Modified;

          //          this.Context.SaveChanges();

          //          return true;
          //     }
          //     catch (Exception)
          //     {
          //          return false;
          //     }
          //}

          private Gender? SetGender( string genderType )
          {
               if ( genderType == null )
               {
                    return null;
               }

               var gender = this.Context.Genders
                    .Where(x => x.Type == genderType)
                    .SingleOrDefault();

               if ( gender == null )
               {
                    Gender newGender = new Gender { Type = genderType };

                    return newGender;
               }

               return gender;
          }
     }
}
/*
 
 */
               