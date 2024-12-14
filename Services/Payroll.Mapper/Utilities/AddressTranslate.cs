
namespace Payroll.Mapper.Utilities
{
       public class AddressTranslate : IAddressTranslate
       {
              public IDictionary<string, string> GetAddressBgPrefix()
              {
                     Dictionary<string, string> bgPrefix = new Dictionary<string, string>()
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

                     return bgPrefix;
              }

              public IDictionary<string, string> GetAddressEngPrefix()
              {
                     Dictionary<string, string> engPrefix = new Dictionary<string, string>()
                     {
                            //{ "Country", string.Empty },
                            //{ "Region", "обл. " },
                            //{ "Municipality", "общ." },
                            //{ "City", "гр." },
                            //{ "Street", "ул." },
                            //{ "Number", "№" },
                            //{ "Entrance", "вх." },
                            //{ "Floor", "ет." },
                            //{ "ApartmentNumber", "ап." },
                     };

                     return engPrefix;
              }
       }
}
