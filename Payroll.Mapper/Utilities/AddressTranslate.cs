
namespace Payroll.Mapper.Utilities
{
     public class AddressTranslate : IAddressTranslate
     {
          public  IDictionary<string,string> GetAddressPrefix()
          {
               Dictionary<string, string> prefix = new Dictionary<string, string>()
               {
                    { "Country", string.Empty },
                    { "Region", "обл. " },
                    { "Municipality", "общ." },
                    { "City", "гр." },
                    { "Street", "ул." },
                    { "Number", "№" },
                    { "Entrance", "вх." },
                    { "Floor", "ет." },
                    { "ApartmentNumber", "ап." },
               };

               return prefix;
          }
     }
}
