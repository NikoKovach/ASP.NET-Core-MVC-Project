using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Test.EntitiesTests.Initial_Data;
using Payroll.Data;

using Payroll.ModelsDto;

using Payroll.Models;
using Payroll.Services.Services;
using Payroll.Mapper.AutoMapper;
using AutoMapper;
using Payroll.ViewModels;


namespace Payroll.Services.Test
{
	[TestFixture]
     public class CreateUpdateCompanyTest
     {
          private readonly DbContextOptions<PayrollContext> options;
          private IMapper mapper;
          private List<CompanyViewModel> companyViews = CompanyInitialData.SetCompaniesDTO();

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

			//var service = new AddUpdateEntity( context, mapper);
   //            for ( int i = 0; i < companyViews.Count - 1; i++ )
   //            {
   //                 service.AddEntityAsync<Company,CompanyDto>( this.companyViews[ i ] )
			//		.GetAwaiter().GetResult();
   //            }
          }

          //[Test]
          public void CreateUpdateEntityServiceShouldCreateCompanyRecord()
          {
    //           //Arrange
    //           using var context = CreateContext();

    //           var service = new AddUpdateEntity( context, mapper);

    //            //Act

    //           service.AddEntityAsync<Company,CompanyDto>( this.companyViews[ 2 ] )
				//.GetAwaiter().GetResult();

    //           var company = context.Companies
    //                .Where( x => x.Name == "CCC Company" )
    //                .FirstOrDefault();

    //           //Assert
    //           Assert.That( company.Name, Is.EqualTo( "CCC Company" ) );
    //           Assert.That( company.Id, Is.EqualTo( 3 ) );
          }

          //[Test]
          public void ConstructorCreateTwoRecordsInCompaniesTable()
          {
               //Arrange
               using var context = CreateContext();

               //Act
               var numbers = context.Companies.Count();

               //Assert
               Assert.That( numbers, Is.EqualTo( 2 ) );
          }

          //[Test]
          public void CreateUpdateEntityServiceShouldUpdateCompanyRecord()
          {
   //            //Arrange
   //            using var context = CreateContext();

   //            Company? company = context.Companies
   //                 .Where( x => x.Name == "AAA Company" )
   //                 .FirstOrDefault();

   //            context.Entry( company ).State = EntityState.Detached;

   //            var entityState = context.Entry( company ).State;

			//CompanyDto companyDto = this.mapper.Map<CompanyDto>( company );
   //            companyDto.HasBeenDeleted = true;

   //            var service = new AddUpdateEntity( context, mapper);

   //             //Act
   //            service.UpdateEntityAsync<Company,CompanyDto>( companyDto )
			//	.GetAwaiter().GetResult();

   //            var updatedCompany = context.Companies
   //                 .Where( x => x.Name == "AAA Company" )
   //                 .FirstOrDefault();

   //            //Assert
   //            Assert.That( updatedCompany.HasBeenDeleted, Is.EqualTo( true ) );
   //            Assert.That( updatedCompany.Id, Is.EqualTo( 1 ) );
          }

          PayrollContext CreateContext()
          {         
               return new PayrollContext( options);
          }
         
     }
}
