using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.PagingViewModels;
using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Areas.Contracts.Controllers
{
       [Area( "LaborAgreements" )]
       public class AgreementController : Controller
       {
              private int _pageSize;
              private int _pageIndex;
              private int _count;

              private readonly ILaborAgreementService service;
              //private readonly IValidate<ValidateBaseModel> validateService;

              public AgreementController( ILaborAgreementService agreementService, IPrivateConfiguration configuration )
              {
                     this.service = agreementService;

                     configuration.SetPagingVariables( ref this._pageIndex, ref this._pageSize, ref this._count );
              }

              [HttpPost]
              public async Task<IActionResult> Index( int? companyId, int? pageIndex, int? pageSize,
                                                              string? sortParam, FilterAgreementVM? filter )
              {
                     if ( !ModelState.IsValid )
                            return await ResultAsync( companyId, pageIndex, pageSize, sortParam, new FilterAgreementVM() );

                     return await ResultAsync( companyId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public ActionResult Create( int? companyId )
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

              [HttpPost]
              public ActionResult Edit( int? companyId, int? employeeId )
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

              [HttpPost]
              public ActionResult Delete( int? companyId, int? agreementId )
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

              //*********************************************************

              private async Task<IActionResult> ResultAsync( int? companyId, int? pageIndex, int? pageSize,
                                                                                                  string? sortParam, FilterAgreementVM filter )
              {
                     var sortedList = await GetAgreementListOfPagesAsync( companyId, pageIndex, pageSize, sortParam, filter );

                     string? controllerName = this.RouteData.Values[ "controller" ].ToString();

                     sortedList.RouteEdit = $"/{controllerName}/{nameof( Edit )}";

                     return View( "Index", sortedList );
              }

              private async Task<PagingListForContracts<LaborAgreementVM, FilterAgreementVM>>
                     GetAgreementListOfPagesAsync
                     ( int? companyId, int? pageIndex, int? pageSize, string? sortParam, FilterAgreementVM? filter )
              {
                     var sortedAgreementsList = this.service.AllActive( companyId, sortParam, filter );

                     var sortedList = await PagingListForContracts<LaborAgreementVM, FilterAgreementVM>
                                                           .CreateAgreementsCollectionAsync( sortedAgreementsList,
                                                           pageIndex ?? this._pageIndex, pageSize ?? this._pageSize, sortParam, filter );


                     sortedList.CompanyId = ( companyId == null || companyId <= 0 ) ? -1 : companyId;

                     if ( sortedList.ItemsCollection.Count == 0 )
                            return EmptyAgreementsSortedCollection();

                     return sortedList;
              }

              private PagingListForContracts<LaborAgreementVM, FilterAgreementVM>
                     EmptyAgreementsSortedCollection()
              {
                     List<LaborAgreementVM> defaultAgreementsList = new List<LaborAgreementVM>()
                     {
                            new LaborAgreementVM()
                     };

                     FilterAgreementVM agreementsFilter = new FilterAgreementVM();

                     var emptyPaginatedList = new PagingListForContracts<LaborAgreementVM, FilterAgreementVM>
                                                                            ( defaultAgreementsList, this._count, this._pageIndex,
                                                                            this._pageSize, string.Empty, agreementsFilter );

                     return emptyPaginatedList;
              }
       }
}

