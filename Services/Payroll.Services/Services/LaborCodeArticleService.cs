using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models.EnumTables;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services
{
       public class LaborCodeArticleService : ILaborCodeArticleService
       {
              private IRepository<LaborCodeArticle> repository;
              private IMapEntity mapper;

              public LaborCodeArticleService( IRepository<LaborCodeArticle> repository, IMapEntity mapper )
              {
                     this.repository = repository;

                     this.mapper = mapper;
              }

              public IQueryable<LaborCodeArticleVM>? AllArticles()
              {
                     IQueryable<LaborCodeArticle>? laborArticles = this.repository.AllAsNoTracking();

                     IQueryable<LaborCodeArticleVM>? result =
                            this.mapper.ProjectTo<LaborCodeArticle, LaborCodeArticleVM>( laborArticles );

                     return result;
              }

              public async Task AddAsync( LaborCodeArticleVM articleTypeVM )
              {
                     if ( string.IsNullOrEmpty( articleTypeVM.Article ) )
                            return;

                     LaborCodeArticle? article = this.mapper.Map<LaborCodeArticleVM, LaborCodeArticle>( articleTypeVM );

                     await this.repository.AddAsync( article );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( LaborCodeArticleVM articleTypeVM )
              {
                     if ( articleTypeVM.Id < 1 )
                            return;

                     if ( string.IsNullOrEmpty( articleTypeVM.Article ) )
                            return;

                     LaborCodeArticle? article = this.mapper.Map<LaborCodeArticleVM, LaborCodeArticle>( articleTypeVM );

                     this.repository.Update( article );

                     await this.repository.SaveChangesAsync();
              }

              public IQueryable<string?>? GetEntity( int? entityId )
              {
                     IQueryable<string?>? article = this.AllArticles()
                                                                                  .Where( x => x.Id == entityId )
                                                                                  .Select( x => x.Article );

                     return article;
              }
       }
}
