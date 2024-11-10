using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;

namespace PersonnelWebApp.Areas.Contracts.Controllers
{
       [Area( "LaborContracts" )]
       public class ContractsController : Controller
       {
              private int _pageSize;
              private int _pageIndex;
              private int _count;

              private readonly ILaborContractService service;
              private readonly IValidate<ValidateBaseModel> validateService;
              private readonly IConfigurationRoot? privateConfig;

              public ContractsController( ILaborContractService contractService )
              {
                     this.service = contractService;
              }

              [HttpPost]
              public ActionResult Index( int? companyId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     // [Required(ErrorMessage = "Select company from the list !")]
                     if ( !ModelState.IsValid )
                     {
                            return View();
                     }

                     return View();
              }


              // POST: ContractsController/Create
              [HttpPost]
              public ActionResult Create( IFormCollection collection )
              {
                     try
                     {
                            return RedirectToAction( nameof( Index ) );
                     }
                     catch
                     {
                            return View();
                     }
              }

              // POST: ContractsController/Edit/5
              [HttpPost]
              public ActionResult Edit( int id, IFormCollection collection )
              {
                     try
                     {
                            return RedirectToAction( nameof( Index ) );
                     }
                     catch
                     {
                            return View();
                     }
              }

              // POST: ContractsController/Delete/5
              [HttpPost]
              public ActionResult Delete( int id, IFormCollection collection )
              {
                     try
                     {
                            return RedirectToAction( nameof( Index ) );
                     }
                     catch
                     {
                            return View();
                     }
              }
       }
}
