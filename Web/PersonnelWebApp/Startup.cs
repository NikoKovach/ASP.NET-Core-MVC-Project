namespace PersonnelWebApp
{
       using Microsoft.AspNetCore.Mvc;
       using Microsoft.EntityFrameworkCore;
       using Microsoft.Extensions.FileProviders;
       using Payroll.Data;
       using PersonnelWebApp.Filters;

       public class Startup
       {
              public Startup( IConfiguration configuration )
              {
                     Configuration = configuration;
              }

              IConfiguration Configuration { get; }

              // This method gets called by the runtime. Use this method to add services to the container.
              public void ConfigureServices( IServiceCollection services )
              {
                     ServicesCollection.Collect( services, Configuration );

                     services.AddControllersWithViews( options =>
                     {
                            options.Filters.Add( new AutoValidateAntiforgeryTokenAttribute() );
                            options.Filters.Add( new ExceptionFilter() );
                     } ).AddRazorRuntimeCompilation();
              }

              // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

              public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
              {
                     //  migrate any database changes on startup (includes initial db creation)
                     using ( var scope = app.ApplicationServices.CreateScope() )
                     {
                            var dataContext = scope
                                          .ServiceProvider
                                          .GetRequiredService<PayrollContext>();

                            dataContext.Database.Migrate();
                     }
                     // End migrate any database changes on startup (includes initial db creation)

                     if ( env.IsDevelopment() )
                     {
                            app.UseDeveloperExceptionPage();
                     }
                     else
                     {
                            app.UseExceptionHandler( "/Home/Error" );
                            app.UseStatusCodePagesWithRedirects( "/Home/StatusCodeError?errorCode={0}" );

                            app.UseHsts();
                            // The default HSTS value is 30 days. You may want to change this for	production scenarios, see https://aka.ms/aspnetcore-   hsts.
                     }

                     app.UseHttpsRedirection();

                     // *********** Use files from multiple locations ***************
                     app.UseStaticFiles();

                     app.UseStaticFiles( new StaticFileOptions
                     {
                            FileProvider = new PhysicalFileProvider(
                                   Path.Combine( env.ContentRootPath,
                                   Configuration[ "PrimaryAppFolder:FolderName" ] ) ),
                            RequestPath = Configuration[ "PrimaryAppFolder:RequestPath" ]
                     } );

                     app.UseStaticFiles( new StaticFileOptions
                     {
                            FileProvider = new PhysicalFileProvider(
                                   Path.Combine( env.ContentRootPath,
                                   Configuration[ "SecondaryAppFolder:FolderName" ] ) ),
                            RequestPath = Configuration[ "SecondaryAppFolder:RequestPath" ]
                     } );
                     // ******************************************************************

                     app.UseRouting();

                     app.UseAuthorization();

                     app.UseEndpoints( endpoints =>
                     {
                            endpoints.MapControllerRoute(
                               name: "areas",
                               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                             );
                     } );

                     app.UseEndpoints( endpoints =>
                     {
                            endpoints.MapControllerRoute(
                                        name: "default",
                                        pattern: "{controller=Home}/{action=Index}/{id?}" );
                     } );
              }
       }
}