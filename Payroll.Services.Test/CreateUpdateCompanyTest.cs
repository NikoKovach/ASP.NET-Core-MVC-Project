using NUnit.Framework;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.ModelsDto;
using Payroll.Models;
using Payroll.Services.Services;
using Payroll.Services.Test.EntitiesTests.Initial_Data;
using Payroll.Mapper.AutoMapper;

namespace Payroll.Services.Test
{
     [TestFixture]
     public class CreateUpdateCompanyTest
     {
          private readonly DbContextOptions<PayrollContext> options;
          private IMapper mapper;
          private List<ModelsDto.CompanyDto> companyViews = CompanyInitialData.SetCompaniesDTO();

          public CreateUpdateCompanyTest()
          {
               var config = new AutoMapperBuilder().CreateMapperConfig();
               this.mapper = config.CreateMapper();

               this.options = new DbContextOptionsBuilder<PayrollContext>()
                             .UseInMemoryDatabase( databaseName: "PayrollDatabase" )
                             .Options;

               using var context = new PayrollContext(options);

               context.Database.EnsureDeleted();
               context.Database.EnsureCreated();

               var service = new CreateUpdateEntityService<ModelsDto.CompanyDto, Company>( context, mapper);

               for ( int i = 0; i < companyViews.Count - 1; i++ )
               {
                    service.CreateEntity( this.companyViews[ i ] );
               }
          }

          [Test]
          public void CreateUpdateEntityServiceShouldCreateCompanyRecord()
          {
               //Arrange
               using var context = CreateContext();

               var service = new CreateUpdateEntityService<ModelsDto.CompanyDto, Company>( context, mapper);

                //Act

               service.CreateEntity( this.companyViews[ 2 ] );

               var company = context.Companies
                    .Where( x => x.Name == "CCC Company" )
                    .FirstOrDefault();

               //Assert
               Assert.That( company.Name, Is.EqualTo( "CCC Company" ) );
               Assert.That( company.Id, Is.EqualTo( 3 ) );
          }

          [Test]
          public void ConstructorCreateTwoRecordsInCompaniesTable()
          {
               //Arrange
               using var context = CreateContext();

               //Act
               var numbers = context.Companies.Count();

               //Assert
               Assert.That( numbers, Is.EqualTo( 2 ) );
          }

          [Test]
          public void CreateUpdateEntityServiceShouldUpdateCompanyRecord()
          {
               //Arrange
               using var context = CreateContext();

               Company? company = context.Companies
                    .Where( x => x.Name == "AAA Company" )
                    .FirstOrDefault();

               context.Entry( company ).State = EntityState.Detached;

               var entityState = context.Entry( company ).State;

               ModelsDto.CompanyDto companyDto = this.mapper.Map<ModelsDto.CompanyDto>( company );
               companyDto.HasBeenDeleted = true;

               var service = new CreateUpdateEntityService<ModelsDto.CompanyDto, Company>( context, mapper);

                //Act
               service.UpdateEntity( companyDto );

               var updatedCompany = context.Companies
                    .Where( x => x.Name == "AAA Company" )
                    .FirstOrDefault();

               //Assert
               Assert.That( updatedCompany.HasBeenDeleted, Is.EqualTo( true ) );
               Assert.That( updatedCompany.Id, Is.EqualTo( 1 ) );
          }

          PayrollContext CreateContext()
          {         
               return new PayrollContext( options);
          }
         
     }
}
