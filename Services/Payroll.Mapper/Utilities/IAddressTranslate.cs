
namespace Payroll.Mapper.Utilities
{
       public interface IAddressTranslate
       {
              IDictionary<string, string> GetAddressBgPrefix();

              IDictionary<string, string> GetAddressEngPrefix();
       }
}
