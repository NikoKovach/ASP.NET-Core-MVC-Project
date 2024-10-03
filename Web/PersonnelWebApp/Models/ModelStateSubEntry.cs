using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PersonnelWebApp.Models
{

       public class ModelStateSubEntry()
       {
              public string? Key { get; set; }

              public ModelStateEntry? Entry { get; set; }


       }
}
