using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class ContactInfoService : IContactInfoService
       {
              private IRepository<ContactInfo> repository;
              private IMapEntity mapper;
              private IContactInfoCollectionFactory contactsCollectionFactory;

              public ContactInfoService( IRepository<ContactInfo> contactRepository, IMapEntity mapper,
                                                                       IContactInfoCollectionFactory contactsCollectionFactory )
              {
                     repository = contactRepository;

                     this.mapper = mapper;

                     this.contactsCollectionFactory = contactsCollectionFactory;
              }

              public IQueryable<ContactInfoVM>? All( int? personId )
              {
                     IQueryable<ContactInfo>? contacts = repository.AllAsNoTracking()
                                                                                                      .Where( x => x.PersonId == personId );

                     IQueryable<ContactInfoVM>? contactsVM = mapper.ProjectTo<ContactInfo, ContactInfoVM>( contacts );

                     return contactsVM;
              }

              public IQueryable<ContactInfoVM>? AllNotDeleted( int? personId, string? sortParam )
              {
                     if ( personId == null )
                     {
                            return null;
                     }

                     IQueryable<ContactInfoVM>? sortedContacts =
                                                                      this.contactsCollectionFactory.SortedCollection( personId, sortParam );

                     return sortedContacts;
              }

              public async Task AddAsync( ContactInfoVM? viewModel )
              {
                     ContactInfo? contact = mapper.Map<ContactInfoVM, ContactInfo>( viewModel );

                     await repository.AddAsync( contact );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( ContactInfoVM viewModel )
              {
                     ContactInfo? contact = mapper.Map<ContactInfoVM, ContactInfo>( viewModel );

                     repository.Update( contact );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( ICollection<ContactInfoVM> viewModels )
              {
                     List<ContactInfo>? contactsList = mapper.Map<List<ContactInfoVM>, List<ContactInfo>>( viewModels.ToList() );

                     repository.Update( contactsList );

                     await repository.SaveChangesAsync();
              }

              public async Task DeleteAsync( int? id, int? personId )
              {
                     if ( id == null || personId == null )
                     {
                            return;
                     }

                     ContactInfo? contact = await this.repository.All()
                                                                                                    .Where( x => x.Id == id && x.PersonId == personId )
                                                                                                    .FirstOrDefaultAsync();

                     contact.HasBeenDeleted = true;
                     contact.DeletionDate = DateTime.UtcNow;

                     await this.repository.SaveChangesAsync();
              }
       }
}

