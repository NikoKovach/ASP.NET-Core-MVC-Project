using System.ComponentModel.DataAnnotations;
using Payroll.ViewModels.PagingViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
       public class AddressesOfPersonVM
       {
              [Required]
              [Display( Name = "Person Id" )]
              public int? PersonId { get; set; }

              [Display( Name = "Permanent Address" )]
              public string? PermanentAddress { get; set; }

              [Display( Name = "Current Address" )]
              public string? CurrentAddress { get; set; }

              public PagingListSortedFiltered<AddressVM, SearchAddressVM>? Addresses { get; set; }

              //public AddressVM NewAddress { get; set; }
       }
}
