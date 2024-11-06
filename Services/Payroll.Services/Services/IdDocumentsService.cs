using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class IdDocumentsService : IDocumentsService
       {
              private IRepository<IdDocument> repository;
              private IMapEntity mapper;
              private IFactorySortCollection<IdDocumentVM> documentsSortFactory;

              public IdDocumentsService( IRepository<IdDocument> repository, IMapEntity mapper,
                     IFactorySortCollection<IdDocumentVM> documentsSortFactory )
              {
                     this.repository = repository;

                     this.mapper = mapper;

                     this.documentsSortFactory = documentsSortFactory;
              }

              public IQueryable<IdDocumentVM>? All( int? personId )
              {
                     IQueryable<IdDocument>? documents = repository.AllAsNoTracking()
                                                                                                     .Where( x => x.PersonId == personId );

                     IQueryable<IdDocumentVM>? documentsVM =
                                                                             mapper.ProjectTo<IdDocument, IdDocumentVM>( documents );

                     return documentsVM;
              }

              public IQueryable<IdDocumentVM>? AllNotDeleted( int? personId, string? sortParam )
              {
                     if ( personId == null )
                     {
                            return default;
                     }

                     IQueryable<IdDocumentVM>? sortedDocuments =
                            this.documentsSortFactory.SortedCollection( sortParam, personId );

                     return sortedDocuments;
              }

              public async Task AddAsync( IdDocumentVM viewModel )
              {
                     IdDocument? idDocument = mapper.Map<IdDocumentVM, IdDocument>( viewModel );

                     await this.SetExistingDocumentAsync( idDocument, viewModel.DocumentName );

                     await this.repository.AddAsync( idDocument );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( IdDocumentVM viewModel )
              {
                     IdDocument? document = mapper.Map<IdDocumentVM, IdDocument>( viewModel );

                     await this.SetExistingDocumentAsync( document, viewModel.DocumentName );

                     this.repository.Update( document );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( ICollection<IdDocumentVM> viewModels )
              {
                     List<IdDocumentVM> idDocumentsVMList = viewModels.ToList();

                     List<IdDocument>? documentsList =
                            mapper.Map<List<IdDocumentVM>, List<IdDocument>>( idDocumentsVMList );

                     for ( int i = 0; i < documentsList.Count; i++ )
                     {
                            await this.SetExistingDocumentAsync( documentsList[ i ], idDocumentsVMList[ i ].DocumentName );
                     }

                     this.repository.Update( documentsList );

                     await this.repository.SaveChangesAsync();
              }

              public async Task DeleteAsync( int? id, int? personId = null )
              {
                     if ( id == null || personId == null )
                     {
                            return;
                     }

                     IdDocument? idDocument = await this.repository.All()
                                                                                               .Where( x => x.Id == id && x.PersonId == personId )
                                                                                               .FirstOrDefaultAsync();
                     if ( personId == null )
                     {
                            idDocument = await this.repository.All().Where( x => x.Id == id )
                                                                                           .FirstOrDefaultAsync();
                     }

                     idDocument.HasBeenDeleted = true;
                     idDocument.DeletionDate = DateTime.UtcNow;

                     await this.repository.SaveChangesAsync();
              }

              public IQueryable<string>? IdDocumentTypes()
              {
                     IQueryable<string>? documentTypes = this.repository.Context.DocumentTypes
                                                                                                         .Select( x => x.DocumentName );

                     return documentTypes;
              }

              //*********************************************************************
              private async Task SetExistingDocumentAsync( IdDocument idDocument, string? documentName )
              {
                     DocumentType? documentType = await this.repository.Context.DocumentTypes
                                                                                        .Where( x => x.DocumentName == documentName )
                                                                                        .FirstOrDefaultAsync();

                     if ( documentType != null && documentType.Id > 0 )
                     {
                            idDocument.DocumentType = documentType;
                     }
              }
       }
}
