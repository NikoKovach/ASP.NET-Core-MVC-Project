using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services;
using Payroll.Services.Services.ServiceContracts;

internal class Program
{
     private static async Task Main( string[] args )
     {
          var context = new PayrollContext();

          var config = new AutoMapperBuilder().CreateMapperConfig();
          var mapper = config.CreateMapper();

//**************************************************************
          var modelDto = GetDto();
          //var entity = GetLast(context);

          //context.Entry( entity ).State = EntityState.Deleted;

          //var companyDto = GetMapperDto(entity,mapper);
          //companyDto.UniqueIdentifier = "0000000000";

          IAddUpdateEntity service = new AddUpdateEntity(context,mapper);

          await service.AddEntityAsync<Person,PersonDto>( modelDto as PersonDto );

          Console.WriteLine();
     }

     private static object GetDto()
     {
          PersonDto dto = new PersonDto() 
          { 
               FirstName		= "Dara",
			MiddleName	= "Smith",
			LastName		= "Bergendorf",
			EGN			= "7703102225",
			GenderType	= "woman",
			PhotoFilePath	= "/css/someFile.css"
          };

          return dto;
     }

      private static CompanyDto GetMapperDto( Company entity, IMapper mapper )
     {
          return mapper.Map<CompanyDto>(entity);
     }

     private static Company GetLast( PayrollContext context )
     {
          var entity = context.Companies
               .OrderBy(x => x.RepresentedBy)
               .LastOrDefault();

          return entity;
     }
}