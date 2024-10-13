namespace Payroll.Services.Services.ServiceContracts
{
       public interface IEntityName
       {
              Task<string?> GetEntityNameAsync( int? entityId );
       }
}
