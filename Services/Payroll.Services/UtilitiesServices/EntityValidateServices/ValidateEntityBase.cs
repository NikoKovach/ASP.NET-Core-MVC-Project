using Payroll.Data.Common;
using Payroll.ViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateEntityBase<TEntity, TVModel>
              where TVModel : class where TEntity : class
       {
              private IRepository<TEntity> repository;

              public ValidateEntityBase( IRepository<TEntity> repository )
              {
                     this.repository = repository;

                     this.ModelState = new ViewModelState();
              }

              public ViewModelState ModelState { get; }

              public IQueryable<TEntity> AllEnities { get => repository.AllAsNoTracking(); }

              public virtual void PropertyIsValid( string propertyName, TVModel viewModel )
              {
                     this.ModelState.ModelIsValid = true;
                     this.ModelState.ErrorMessage = string.Empty;
              }
       }
}
