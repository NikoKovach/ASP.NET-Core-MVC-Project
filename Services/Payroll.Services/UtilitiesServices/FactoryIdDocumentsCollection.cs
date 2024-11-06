using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class FactoryIdDocumentsCollection : IFactorySortCollection<IdDocumentVM>
       {
              private IMapEntity mapper;
              private IRepository<IdDocument> repository;
              private Dictionary<string, IQueryable<IdDocumentVM>> sortedDocumentsCollection;

              public FactoryIdDocumentsCollection( IMapEntity mapper, IRepository<IdDocument> repository )
              {
                     this.mapper = mapper;

                     this.repository = repository;
              }

              public IQueryable<IdDocumentVM>? SortedCollection( string? sortParam, params object[] items )
              {
                     int personId = (int) items[ 0 ];

                     IQueryable<IdDocument>? personDocuments = this.repository
                                                                                          .AllAsNoTracking()
                                                                                          .Where( x => x.PersonId == personId &&
                                                                                                                  x.HasBeenDeleted == false );

                     SetDiplomasDictionary( personDocuments );

                     if ( string.IsNullOrEmpty( sortParam ) )
                     {
                            return this.sortedDocumentsCollection[ "Default" ];
                     }

                     //return this.sortedDocumentsCollection[ "Default" ];
                     return this.sortedDocumentsCollection[ sortParam ];
              }

              //#######################################################################
              private void SetDiplomasDictionary( IQueryable<IdDocument>? personDocuments )
              {
                     this.sortedDocumentsCollection = new Dictionary<string, IQueryable<IdDocumentVM>>();

                     this.sortedDocumentsCollection[ "DocumentType_desc" ] = DocumentTypeDesc( personDocuments );
                     this.sortedDocumentsCollection[ "DocumentType_asc" ] = DocumentTypeAsc( personDocuments );
                     this.sortedDocumentsCollection[ "DateOfExpire_desc" ] = DateOfExpireDesc( personDocuments );
                     this.sortedDocumentsCollection[ "DateOfExpire_asc" ] = DateOfExpireAsc( personDocuments );
                     this.sortedDocumentsCollection[ "DateOfIssue_desc" ] = DateOfIssueDesc( personDocuments );
                     this.sortedDocumentsCollection[ "DateOfIssue_asc" ] = DateOfIssueAsc( personDocuments );
                     this.sortedDocumentsCollection[ "IsValid_desc" ] = IsValidDesc( personDocuments );
                     this.sortedDocumentsCollection[ "IsValid_asc" ] = IsValidAsc( personDocuments );

                     this.sortedDocumentsCollection[ "Default" ] = DefaultPersonIdDocuments( personDocuments );
              }

              private IQueryable<IdDocumentVM>? DefaultPersonIdDocuments( IQueryable<IdDocument>? personDocuments )
              {
                     var documentsCollection = mapper.ProjectTo<IdDocument, IdDocumentVM>( personDocuments );

                     return documentsCollection;
              }

              private IQueryable<IdDocumentVM> DocumentTypeDesc( IQueryable<IdDocument>? personDocuments )
              {
                     var documents = this.mapper
                                                     .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                     .OrderByDescending( x => x.DocumentName );

                     return documents;
              }

              private IQueryable<IdDocumentVM> DocumentTypeAsc( IQueryable<IdDocument>? personDocuments )
              {
                     var documents = this.mapper
                                                     .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                     .OrderBy( x => x.DocumentName );

                     return documents;
              }

              private IQueryable<IdDocumentVM> DateOfExpireDesc( IQueryable<IdDocument>? personDocuments )
              {
                     IQueryable<IdDocumentVM>? documents = this.mapper
                                                     .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                     .OrderByDescending( x => x.DateOfExpire );

                     return documents;
              }

              private IQueryable<IdDocumentVM> DateOfExpireAsc( IQueryable<IdDocument>? personDocuments )
              {
                     IQueryable<IdDocumentVM>? documents = this.mapper
                                                     .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                     .OrderBy( x => x.DateOfExpire );

                     return documents;
              }

              private IQueryable<IdDocumentVM> IsValidDesc( IQueryable<IdDocument>? personDocuments )
              {
                     IQueryable<IdDocumentVM>? documents = this.mapper
                                                     .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                     .OrderByDescending( x => x.IsValid )
                                                     .ThenByDescending( x => x.DateOfIssue );

                     return documents;
              }

              private IQueryable<IdDocumentVM> IsValidAsc( IQueryable<IdDocument>? personDocuments )
              {
                     IQueryable<IdDocumentVM>? documents = this.mapper
                                                     .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                     .OrderBy( x => x.IsValid )
                                                     .ThenByDescending( x => x.DateOfIssue );

                     return documents;
              }

              private IQueryable<IdDocumentVM> DateOfIssueDesc( IQueryable<IdDocument>? personDocuments )
              {
                     IQueryable<IdDocumentVM>? documents = this.mapper
                                                    .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                    .OrderByDescending( x => x.DateOfIssue )
                                                    .ThenByDescending( x => x.DocumentNumber );

                     return documents;
              }

              private IQueryable<IdDocumentVM> DateOfIssueAsc( IQueryable<IdDocument>? personDocuments )
              {
                     IQueryable<IdDocumentVM>? documents = this.mapper
                                                      .ProjectTo<IdDocument, IdDocumentVM>( personDocuments )
                                                      .OrderBy( x => x.DateOfIssue )
                                                      .ThenBy( x => x.DocumentNumber );

                     return documents;
              }
       }
}

