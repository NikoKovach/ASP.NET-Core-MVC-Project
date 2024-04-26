using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

namespace Payroll.Services.Services.EmployeeServices
{
     public class CreateUpdateEmployeeService : CreateUpdateEntityService<EmployeeDto, Employee>
     {
          /// <summary>
          /// TODO : FOR TESTING
          /// </summary>
          /// 

          public CreateUpdateEmployeeService( PayrollContext payrollContext, IMapper autoMapper) : base( payrollContext,autoMapper )
          {
          }

          public override Employee CreateObject( EmployeeDto empDto )
          {
               Gender? gender = SetGender( empDto.Person.GenderType );
               
               Employee emp = this.Mapper.Map<Employee>(empDto);

               emp.Person.Gender = gender;
               emp.Person.GenderId = gender.Id;

               return emp;
          }

          public override bool UpdateRecord( Employee emp )
          {
               try
               {
                    var updatedEntity = this.Context.Entry( emp );
                    var personEntity  = this.Context.Entry( emp.Person );

                    if (updatedEntity.State == EntityState.Detached)
                    {
                         DbSet<Employee> empDbSet = this.Context.Set<Employee>();
                         empDbSet.Attach(emp);
                    }

                    if (personEntity.State == EntityState.Detached)
                    {
                         DbSet<Models.Person> empDbSet = this.Context.Set<Models.Person>();
                         empDbSet.Attach(emp.Person);
                    }

                    updatedEntity.State = EntityState.Modified;
                    personEntity.State = EntityState.Modified;

                    this.Context.SaveChanges();

                    return true;
               }
               catch (Exception)
               {
                    return false;
               }
          }

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
          private Person? CreatePerson( PersonDto? personDto )
          {
               if ( personDto == null )
               {
                    return null;
               }

               Gender? gender = SetGender( personDto.GenderType );

               Person newPerson = this.Mapper.Map<Person>( personDto );

               newPerson.Gender = gender;

               return newPerson;
          }
               //Employee emp = new Employee
               //{
               //     Id = view.Id,
               //     IsActual = view.IsActual,
               //     NumberFromTheList = view.NumberFromTheList,
               //     Person = person,
               //     CompanyId = view.CompanyId,
               //     EmpContractId = view.EmpContractId
               //};

               //Person newPerson = new Person
               //{
               //     Id = personDto.Id,
               //     FirstName = personDto.FirstName,
               //     MiddleName = personDto.MiddleName,
               //     LastName = personDto.LastName,
               //     GenderId = genderId,
               //     EGN = personDto.EGN,
               //     PhotoFilePath = personDto.PhotoFilePath,
               //     CurrentAddress = personDto.CurrentAddress,
               //     PermanentAddress = personDto.PermanentAddress,
               //     HasBeenDeleted = personDto.HasBeenDeleted
               //};
               //newPerson.GenderId = gender.Id;
 
 */
               