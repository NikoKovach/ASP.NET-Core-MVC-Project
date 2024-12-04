using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services
{
       public class DepartmentService : IDepartmentService
       {
              private IRepository<Department> repository;
              private IMapEntity mapper;

              public DepartmentService( IRepository<Department> repository, IMapEntity mapper )
              {
                     this.repository = repository;

                     this.mapper = mapper;
              }

              public IQueryable<DepartmentVM>? All()
              {
                     IQueryable<Department>? departments = this.repository.AllAsNoTracking();

                     IQueryable<DepartmentVM>? departmentList =
                            this.mapper.ProjectTo<Department, DepartmentVM>( departments );

                     return departmentList;
              }

              public async Task AddAsync( DepartmentVM departmentVM )
              {
                     if ( string.IsNullOrEmpty( departmentVM.Name ) )
                            return;

                     Department? department = this.mapper.Map<DepartmentVM, Department>( departmentVM );

                     await this.repository.AddAsync( department );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( DepartmentVM departmentVM )
              {
                     if ( departmentVM.DepartmentId < 1 )
                            return;

                     if ( string.IsNullOrEmpty( departmentVM.Name ) )
                            return;

                     Department? department = this.mapper.Map<DepartmentVM, Department>( departmentVM );

                     this.repository.Update( department );

                     await this.repository.SaveChangesAsync();
              }

              public IQueryable<DepartmentVM>? GetEntity( int? entityId )
              {
                     IQueryable<DepartmentVM>? departmentList = this.All().Where( x => x.DepartmentId == entityId );

                     return departmentList;
              }
       }
}
