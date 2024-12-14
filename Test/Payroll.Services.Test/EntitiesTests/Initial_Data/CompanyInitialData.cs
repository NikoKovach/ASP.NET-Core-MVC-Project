using Payroll.ViewModels;

namespace Payroll.Services.Test.EntitiesTests.Initial_Data
{
       public static class CompanyInitialData
       {
              public static List<CompanyVM> SetCompaniesDTO()
              {
                     List<CompanyVM> compantDto = new List<CompanyVM>()
                     {
                            new CompanyVM
                            {
                                   Name                     = "AAA Company",
                                   CompanyHeadquarter       = "Plovdiv",
                                   AddressOfManagement      = "Plovdiv, ul.Kap.Raycho 56, fl.4,ap.30",
                                   UniqueIdentifier         = "111111111",
                                   VATRegNumber             = "BG111111111",
                                   RepresentedBy            = "A.A.",
                                   RepresentativeIdNumber   = "1212121212",
                                   CompanyCaseNumber        = "BO20010101",
                                   HasBeenDeleted           = false,
                            },
                            new CompanyVM
                            {
                                   Name                     = "BBB Company",
                                   CompanyHeadquarter       = "Plovdiv",
                                   AddressOfManagement      = "Plovdiv, bul.East 100, fl.2,ap.4",
                                   UniqueIdentifier         = "222222222",
                                   VATRegNumber             = "BG222222222",
                                   RepresentedBy            = "B.B.",
                                   RepresentativeIdNumber   = "2323232323",
                                   CompanyCaseNumber        = "XYXY",
                                   HasBeenDeleted           = false,
                            },
                            new CompanyVM
                            {
                                 Name                     = "CCC Company",
                                 CompanyHeadquarter       = "Varna",
                                 AddressOfManagement      = "Plovdiv, bul.East 100, fl.2,ap.4",
                                 UniqueIdentifier         = "555555555",
                                 VATRegNumber             = "BG555555555",
                                 RepresentedBy            = "N.K.",
                                 RepresentativeIdNumber   = "2222334444",
                                 CompanyCaseNumber        = "XXXXXXXX",
                                 HasBeenDeleted           = false,
                            },
               };

                     return compantDto;
              }
       }
}
