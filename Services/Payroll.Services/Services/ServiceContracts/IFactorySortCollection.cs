namespace Payroll.Services.Services.ServiceContracts
{
       public interface IFactorySortCollection<T>
       {
              IQueryable<T>? SortedCollection( string? sortParam, params object[] items );
       }
}
