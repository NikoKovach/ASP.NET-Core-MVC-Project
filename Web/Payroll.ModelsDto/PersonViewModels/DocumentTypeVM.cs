using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class DocumentTypeVM
       {
              public int Id { get; set; }

              [Display( Name = "Document type" )]
              public string? DocumentName { get; set; }

              [Display( Name = "Deleted" )]
              public bool HasBeenDeleted { get; set; }

       }
}
