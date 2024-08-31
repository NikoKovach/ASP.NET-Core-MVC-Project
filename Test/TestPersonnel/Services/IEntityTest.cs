namespace TestPersonnel.Demo.Services

{
       public interface IEntityTest<TViewModel>
       {
              Task<TViewModel?> GetEntityAsync( int? parameter );
       }
}
