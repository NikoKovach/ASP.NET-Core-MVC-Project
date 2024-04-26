namespace Payroll.Services.Services.ServiceContracts
{
     public interface IGetEntityByNumber
     {
          TEntityDto? GetEntityByNumber<TEntityDto>( string number );
     }
}
