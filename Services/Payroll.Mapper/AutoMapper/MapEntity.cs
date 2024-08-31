using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Payroll.Mapper.AutoMapper
{
       public class MapEntity : IMapEntity
       {
              private IMapper mapper;

              public MapEntity( IMapper autoMapper )
              {
                     this.mapper = autoMapper;
              }

              public TResult Map<TSource, TResult>( TSource view )
                     where TSource : class
                     where TResult : class
              {
                     try
                     {
                            TResult entity = mapper.Map<TResult>( view );

                            return entity;
                     }
                     catch ( Exception )
                     {
                            throw new InvalidOperationException();
                     }
              }

              public IQueryable<TResult> ProjectTo<TSource, TResult>
              ( IQueryable<TSource> dbSet )
                     where TSource : class
                     where TResult : class
              {
                     try
                     {
                            IQueryable<TResult> query = dbSet.ProjectTo<TResult>
                                                               ( mapper.ConfigurationProvider );

                            return query;
                     }
                     catch ( Exception )
                     {
                            throw new InvalidOperationException();
                     }
              }
       }
}
