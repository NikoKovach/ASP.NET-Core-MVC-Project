namespace PersonnelWebApp
{
     using AutoMapper;
     using Microsoft.EntityFrameworkCore;
	using Payroll.Data;
	using Payroll.Mapper.CustomMap;
	using Payroll.ModelsDto;
	using Payroll.Services.Services;
	using Payroll.Services.Services.CompanyServices;
	using Payroll.Services.Services.EmployeeServices;
	using Payroll.Services.Services.ServiceContracts;
	using System.Reflection;

     public class Startup
     {
          public Startup(IConfiguration configuration)
          {
              Configuration = configuration;
          }

           IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
          public void ConfigureServices(IServiceCollection services)
          {
//***********  Personel modifications are here  ************

               string? connString = Configuration
                                   .GetConnectionString( "DefaultConnection" );

			string? mapperAssembly = Configuration["AutoMapperAssembly"];

               services.AddDbContext<PayrollContext>( options => options
                                                  .UseSqlServer( connString ) );

			services.AddAutoMapper(Assembly.Load(mapperAssembly));

			services.AddScoped<IAddUpdateEntity, AddUpdateEntity>();

               services.AddScoped<ICompany, CompanyService>();

			services.AddTransient<IEmployeeService, EmployeeService>();

			services.AddTransient<IGetEmployeeMapping,GetEmployeeMapping>();
			
			services.AddTransient<IAllValidEntities<SearchCompanyDto>,
								ComponentSearchCompany>();

 //*********** End Personel modifications  ************       

               services.AddControllersWithViews().AddRazorRuntimeCompilation();

          }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
	// **** migrate any database changes on startup (includes initial db creation)
               using (var scope = app.ApplicationServices.CreateScope())
               {
				var dataContext = scope
                                    .ServiceProvider
                                    .GetRequiredService<PayrollContext>();

				dataContext.Database.Migrate();
               }
	// **** End migrate any database changes on startup (includes initial db creation)

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			    // The default HSTS value is 30 days. You may want to change this for	production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

               app.UseHttpsRedirection();
               app.UseStaticFiles();

               app.UseRouting();

               app.UseAuthorization();

               app.UseEndpoints( endpoints =>
               {
                    endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}" );
               } );
          }
     }
}
