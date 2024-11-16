using System.Reflection;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class FactoryLaborAgreements : IFactorySortCollection<LaborAgreementVM>
       {
              private IQueryable<LaborAgreementVM> baseCollection;
              private Dictionary<string, IQueryable<LaborAgreementVM>> sortedContractsCollection;

              public IQueryable<LaborAgreementVM>? SortedCollection( string? sortParam, params object[] items )
              {
                     FilterAgreementVM? filter = ( items.Length > 0 ) ? (FilterAgreementVM?) items[ 0 ] : new FilterAgreementVM();

                     if ( items.Length >= 2 )
                     {
                            this.baseCollection = (IQueryable<LaborAgreementVM>) items[ 1 ];
                     }

                     IQueryable<LaborAgreementVM>? contracts = FilterAgreements( filter );

                     SetContractsDictionary( contracts );

                     if ( string.IsNullOrEmpty( sortParam ) )
                     {
                            return this.sortedContractsCollection[ "default" ];
                     }

                     return this.sortedContractsCollection[ sortParam ];
              }

              //###################################################################

              private void SetContractsDictionary( IQueryable<LaborAgreementVM>? contracts )
              {
                     this.sortedContractsCollection = new Dictionary<string, IQueryable<LaborAgreementVM>>();

                     this.sortedContractsCollection[ "EmployeeName_desc" ] = EmpNameDesc( contracts );
                     this.sortedContractsCollection[ "EmployeeName_asc" ] = EmpNameAsc( contracts );
                     this.sortedContractsCollection[ "ContractNumber_desc" ] = ContractNumberDesc( contracts );
                     this.sortedContractsCollection[ "ContractNumber_asc" ] = ContractNumberAsc( contracts );
                     this.sortedContractsCollection[ "ContractDate_desc" ] = ContractDateDesc( contracts );
                     this.sortedContractsCollection[ "ContractDate_asc" ] = ContractDateAsc( contracts );
                     this.sortedContractsCollection[ "JobTitle_desc" ] = JobTitleDesc( contracts );
                     this.sortedContractsCollection[ "JobTitle_asc" ] = JobTitleAsc( contracts );
                     this.sortedContractsCollection[ "Department_desc" ] = DepartmentDesc( contracts );
                     this.sortedContractsCollection[ "Department_asc" ] = DepartmentAsc( contracts );
                     this.sortedContractsCollection[ "LaborReward_desc" ] = LaborRewardDesc( contracts );
                     this.sortedContractsCollection[ "LaborReward_asc" ] = LaborRewardAsc( contracts );

                     this.sortedContractsCollection[ "default" ] = DefaultAgreementsCollection( contracts );
              }

              private IQueryable<LaborAgreementVM> DefaultAgreementsCollection
                                                                                         ( IQueryable<LaborAgreementVM>? contracts )
              {
                     IQueryable<LaborAgreementVM>? defaultCollection = contracts
                                                                                                                       .OrderByDescending( x => x.ContractDate )
                                                                                                                       .ThenByDescending( x => x.ContractNumber );

                     return defaultCollection;
              }

              private IQueryable<LaborAgreementVM> EmpNameDesc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderByDescending( x => x.LastName )
                                                                                  .ThenByDescending( x => x.FirstName );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> EmpNameAsc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderBy( x => x.LastName )
                                                                                   .ThenBy( x => x.FirstName );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> ContractNumberDesc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderByDescending( x => x.ContractNumber );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> ContractNumberAsc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderBy( x => x.ContractNumber );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> ContractDateDesc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderByDescending( x => x.ContractDate );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> ContractDateAsc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderBy( x => x.ContractDate );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> JobTitleDesc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderByDescending( x => x.JobTitle );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> JobTitleAsc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderBy( x => x.JobTitle );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> DepartmentDesc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderByDescending( x => x.DepartmentName );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> DepartmentAsc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderBy( x => x.DepartmentName );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> LaborRewardDesc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderByDescending( x => x.Salary );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> LaborRewardAsc( IQueryable<LaborAgreementVM>? contracts )
              {
                     var contractsCollection = contracts.OrderBy( x => x.Salary );

                     return contractsCollection;
              }

              private IQueryable<LaborAgreementVM> FilterAgreements( FilterAgreementVM? filter )
              {
                     if ( HasData( filter ) )
                     {
                            ParseSearchName( filter );

                            IQueryable<LaborAgreementVM>? resultCollection = this.baseCollection;

                            if ( !string.IsNullOrEmpty( filter.FirstName ) )
                            {
                                   if ( !string.IsNullOrEmpty( filter.LastName ) && filter.FirstName.Equals( filter.LastName ) )
                                   {
                                          resultCollection = resultCollection.Where( x => x.FirstName.StartsWith( filter.FirstName )
                                                                                                                 || x.LastName.StartsWith( filter.LastName ) );

                                   }
                                   else
                                   {
                                          resultCollection = resultCollection.Where( x => x.FirstName.StartsWith( filter.FirstName ) );
                                   }
                            }

                            if ( !string.IsNullOrEmpty( filter.LastName ) && !filter.LastName.Equals( filter.FirstName ) )
                                   resultCollection = resultCollection.Where( x => x.LastName.StartsWith( filter.LastName ) );

                            if ( !string.IsNullOrEmpty( filter.ContractNumber ) )
                                   resultCollection = resultCollection.Where( x => x.ContractNumber.Contains( filter.ContractNumber ) );

                            if ( !string.IsNullOrEmpty( filter.JobTitle ) )
                                   resultCollection = resultCollection.Where( x => x.JobTitle.StartsWith( filter.JobTitle ) );

                            if ( !string.IsNullOrEmpty( filter.Department ) )
                                   resultCollection = resultCollection.Where( x => x.DepartmentName.StartsWith( filter.Department ) );

                            if ( filter.StartContractDate != null )
                            {
                                   resultCollection = resultCollection.Where( x => x.ContractDate >= filter.StartContractDate );
                            }

                            if ( filter.EndContractDate != null )
                            {
                                   resultCollection = resultCollection.Where( x => x.ContractDate <= filter.EndContractDate );
                            }

                            return resultCollection;
                     }

                     return this.baseCollection;
              }

              private bool HasData( FilterAgreementVM? filter )
              {
                     PropertyInfo[]? propertiesList = filter.GetType().GetProperties();

                     List<object?>? propertyValues = propertiesList.Select( x => x.GetValue( filter ) ).ToList();

                     return propertyValues.Any( x => x != null );
              }

              private void ParseSearchName( FilterAgreementVM? filter )
              {
                     if ( string.IsNullOrEmpty( filter.SearchName ) )
                     {
                            filter.FirstName = null;
                            filter.LastName = null;

                            return;
                     }

                     string[] nameArray = filter.SearchName.Split( " ", 2, StringSplitOptions.RemoveEmptyEntries );

                     if ( nameArray.Length == 1 )
                     {
                            filter.LastName = nameArray[ 0 ].ToLower();

                            filter.FirstName = nameArray[ 0 ].ToLower();

                            return;
                     }

                     filter.FirstName = nameArray[ 0 ].ToLower();

                     string[] lastNameArray = nameArray[ 1 ].Split( " ", StringSplitOptions.RemoveEmptyEntries );

                     if ( lastNameArray.Length == 1 )
                     {
                            filter.LastName = lastNameArray[ 0 ].ToLower();

                            return;
                     }

                     filter.LastName = nameArray[ 1 ].ToLower();
              }
       }
}

