using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services.ServiceContracts;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
     public class CreateUpdateDiplomaService : CreateUpdateEntityService<DiplomaDto, Diploma>, ICreateUpdateEntity<DiplomaDto, Diploma>
     {
          public CreateUpdateDiplomaService(PayrollContext payrollContext, IMapper autoMapper) : base(payrollContext, autoMapper)
          {
          }

          public override Diploma CreateObject(DiplomaDto diplomaDto)
          {
               EducationType? eduType = SetEducationType(diplomaDto.EducationTypeName, diplomaDto.EducationId);

               Diploma diploma = Mapper.Map<Diploma>(diplomaDto);

               diploma.EducationType = eduType;
               diploma.EducationId = eduType.Id;

               return diploma;
          }

          public override bool UpdateRecord(Diploma diploma)
          {
               try
               {
                    var diplomaEntity = Context.Entry(diploma);
                    var eduTypeEntity = Context.Entry(diploma.EducationType);

                    if (diplomaEntity.State == EntityState.Detached)
                    {
                         DbSet<Diploma> diplomaDbSet = Context.Set<Diploma>();
                         diplomaDbSet.Attach(diploma);
                    }

                    if (eduTypeEntity.State == EntityState.Detached)
                    {
                         DbSet<EducationType> eduTypeDbSet = Context.Set<EducationType>();
                         eduTypeDbSet.Attach(diploma.EducationType);
                    }

                    diplomaEntity.State = EntityState.Modified;
                    eduTypeEntity.State = EntityState.Modified;

                    Context.SaveChanges();

                    return true;
               }
               catch (Exception)
               {
                    return false;
               }
          }

          private EducationType? SetEducationType(string? educationTypeName, int eduId)
          {
               if (educationTypeName == null)
               {
                    return null;
               }

               var eduType = Context.EducationTypes
                    .Where(x => x.Id == eduId)
                    .SingleOrDefault();

               if (eduType == null)
               {
                    EducationType newEduType = new EducationType { Type = educationTypeName };

                    return newEduType;
               }

               eduType.Type = educationTypeName;

               return eduType;
          }
     }
}
