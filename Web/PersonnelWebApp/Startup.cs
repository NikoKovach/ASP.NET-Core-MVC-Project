namespace PersonnelWebApp
{
	using System.Reflection;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.FileProviders;
	using Payroll.Data;

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

			ServicesCollection.Collect(services);

		//*********** End Personel modifications  ************       

               services.AddControllersWithViews(options => 
				{
					options.Filters.Add( new AutoValidateAntiforgeryTokenAttribute() );
				}
			).AddRazorRuntimeCompilation();
          }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
		//  migrate any database changes on startup (includes initial db creation)
               using (var scope = app.ApplicationServices.CreateScope())
               {
				var dataContext = scope
                                    .ServiceProvider
                                    .GetRequiredService<PayrollContext>();

				dataContext.Database.Migrate();
               }
		// End migrate any database changes on startup (includes initial db creation)

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

// *********** Use files from multiple locations ***************
			app.UseStaticFiles();

			app.UseStaticFiles( new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine( env.ContentRootPath, 
					Configuration["PrimaryAppFolder:FolderName"] )),
				RequestPath = Configuration["PrimaryAppFolder:RequestPath"]
			} );

			app.UseStaticFiles( new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine( env.ContentRootPath, 
					Configuration["SecondaryAppFolder:FolderName"] )),
				RequestPath = Configuration["SecondaryAppFolder:RequestPath"]
			} );
// ******************************************************************

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

/*
 var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "zSystemFolder"));

			var options = new FileServerOptions
			{
			    FileProvider = fileProvider,
			    RequestPath = "/zsystemfolder",
			    EnableDirectoryBrowsing = true
			};
			
			app.UseFileServer(options);

//string rootFolder = env.ContentRootPath;
			//app.UseStaticFiles(new StaticFileOptions
			//{
			//    FileProvider = new PhysicalFileProvider(rootFolder + @"\zSystemFolder"),
			//});


			//app.UseStaticFiles(new StaticFileOptions
			//{
			//    FileProvider = new PhysicalFileProvider(
			//	    Path.Combine(env.ContentRootPath,"zSystemFolder")),
			//    RequestPath = "/zSystemFolder"
			//});

			//var webRootProvider = new PhysicalFileProvider(env.WebRootPath);

			//var newPathProvider = new PhysicalFileProvider(
			//	Path.Combine(env.ContentRootPath, @"zSystemFolder"));

			//var compositeProvider = new CompositeFileProvider(webRootProvider,
   //                         newPathProvider);

			//env.WebRootFileProvider = compositeProvider;
 
 */
			/*
			 .UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
                System.IO.Path.GetFullPath(wsApp.Configuration.ProductImageUploadFilePath)),
        RequestPath = new PathString("/product-images"),
        DefaultContentType = "application/octet-stream"
    });
			 FileProvider = new PhysicalFileProvider(
			   	  Path.Combine(Directory.GetCurrentDirectory(),"product-images")),
			     RequestPath = new PathString("/product-images"),
				DefaultContentType = "application/octet-stream"
			 */