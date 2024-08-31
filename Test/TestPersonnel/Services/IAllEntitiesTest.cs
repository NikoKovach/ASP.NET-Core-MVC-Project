namespace TestPersonnel.Demo.Services
{
       public interface IAllEntitiesTest
       {
              IQueryable<TEntity> GetAll<TEntity>( int parameter );

              IQueryable<TEntity> GetAllActive<TEntity>( int parameter );
       }
}
