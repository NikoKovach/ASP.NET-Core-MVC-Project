using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payroll.Data.ModelConfiguration;
using Payroll.Models;
using Payroll.Models.EnumTables;

namespace Payroll.Data
{
       public class PayrollContext : DbContext
       {
              public PayrollContext()
              {
              }

              public PayrollContext( DbContextOptions options )
                : base( options )
              {
              }

              public DbSet<Address> Addresses { get; set; }

              public DbSet<Company> Companies { get; set; }

              public DbSet<ContactInfo> ContactInfos { get; set; }

              public DbSet<Department> Departments { get; set; }

              public DbSet<Diploma> Diplomas { get; set; }

              public DbSet<Employee> Employees { get; set; }

              public DbSet<EmploymentContract> EmploymentContracts { get; set; }

              public DbSet<IdDocument> IdDocuments { get; set; }

              public DbSet<Person> Persons { get; set; }

              public DbSet<Annex> Annexes { get; set; }

              public DbSet<TemporaryDisability> TemporaryDisabilities { get; set; }

              public DbSet<Vacation> Vacations { get; set; }

              public DbSet<MonthlySalaryStatement> MonthlySalaryStatements { get; set; }

              public DbSet<DeductionElement> DeductionElements { get; set; }

              public DbSet<IncomeElement> IncomeElements { get; set; }

              public DbSet<IncomePartStatement> IncomePartStatements { get; set; }

              public DbSet<DeductionPartStatement> DeductionPartStatements { get; set; }

              public DbSet<RecapPartStatement> RecapPartStatements { get; set; }

              public DbSet<WorkingDaysByMonth> WorkingDaysByMonths { get; set; }

              public DbSet<PublicHolidayAndWeekday> PublicHolidayAndWeekdays { get; set; }

              //###############################################################################

              public DbSet<ContractType> ContractTypes { get; set; }

              public DbSet<DocumentType> DocumentTypes { get; set; }

              public DbSet<EducationType> EducationTypes { get; set; }

              public DbSet<Gender> Genders { get; set; }

              public DbSet<LaborCodeArticle> LaborCodeArticles { get; set; }

              //public DbSet<PlaceOfRegistration> PlaceOfRegistrations { get; set; }

              public DbSet<TypeSickSheet> TypeSickSheets { get; set; }

              public DbSet<ModeOfTreatment> ModeOfTreatments { get; set; }

              public DbSet<TypeVacation> TypeVacations { get; set; }

              public DbSet<PaymentType> PaymentTypes { get; set; }

              public DbSet<DeductionType> DeductionTypes { get; set; }

              public DbSet<IncomeType> IncomeTypes { get; set; }


              protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
              {
                     var config = CreateConnectionStringBuilder();

                     if ( !optionsBuilder.IsConfigured )
                     {
                            optionsBuilder.UseSqlServer( config.GetConnectionString( "DefaultConnection" ) );
                     }

                     optionsBuilder.EnableSensitiveDataLogging();
              }

              protected override void OnModelCreating( ModelBuilder modelBuilder )
              {
                     modelBuilder.ApplyConfiguration( new PersonConfiguration() );

                     modelBuilder.ApplyConfiguration( new DiplomaConfiguration() );

                     modelBuilder.ApplyConfiguration( new EmpContractConfiguration() );

                     modelBuilder.ApplyConfiguration( new IdDocumentConfiguration() );

                     modelBuilder.ApplyConfiguration( new CompanyConfiguration() );

                     modelBuilder.ApplyConfiguration( new AnnexConfiguration() );

                     modelBuilder.ApplyConfiguration( new VacationConfiguration() );

                     modelBuilder.ApplyConfiguration( new SalaryStatementConfiguration() );

                     modelBuilder.ApplyConfiguration( new DeductionElementConfiguration() );

                     modelBuilder.ApplyConfiguration( new IncomeElementConfiguration() );
              }

              private IConfigurationRoot CreateConnectionStringBuilder()
              {
                     var path = Path.GetFullPath( Assembly.GetExecutingAssembly().Location );

                     var dirPath = Path.GetFullPath( Assembly
                                  .GetExecutingAssembly()
                                  .Location + @"\..\..\..\..\..\..\Data\Payroll.Data\Services\" );

                     IConfigurationRoot builder = new ConfigurationBuilder()
                                             .SetBasePath( dirPath )
                                             .AddJsonFile( dirPath +
                                                           "Connection.json", true, true )
                                             .Build();
                     return builder;
              }
       }
}



