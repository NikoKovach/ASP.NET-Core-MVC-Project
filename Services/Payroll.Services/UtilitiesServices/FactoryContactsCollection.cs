using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class FactoryContactsCollection : IFactorySortCollection<ContactInfoVM>
       {
              private IMapEntity mapper;
              private IRepository<ContactInfo> repository;
              private Dictionary<string, IQueryable<ContactInfoVM>> sortedContactsCollection;

              public FactoryContactsCollection( IMapEntity mapper, IRepository<ContactInfo> contactsRopository )
              {
                     this.mapper = mapper;

                     this.repository = contactsRopository;
              }

              public IQueryable<ContactInfoVM>? SortedCollection( string? sortParam, params object[] items )
              {
                     //if ( items.Length < 1 )
                     //{
                     //       return Enumerable.Empty<ContactInfoVM>().AsQueryable();
                     //}

                     int personId = (int) items[ 0 ];

                     IQueryable<ContactInfo>? personContacts = this.repository
                                                                                                            .AllAsNoTracking()
                                                                                                            .Where( x => x.PersonId == personId &&
                                                                                                                                    x.HasBeenDeleted == false );
                     SetContactsDictionary( personContacts );

                     if ( string.IsNullOrEmpty( sortParam ) )
                     {
                            return this.sortedContactsCollection[ "Default" ];
                     }

                     return this.sortedContactsCollection[ sortParam ];
              }

              //#######################################################################
              private void SetContactsDictionary( IQueryable<ContactInfo>? personContacts )
              {
                     this.sortedContactsCollection = new Dictionary<string, IQueryable<ContactInfoVM>>();

                     this.sortedContactsCollection[ "Phonenumber_desc" ] = PhoneNumberDesc( personContacts );
                     this.sortedContactsCollection[ "Phonenumber_asc" ] = PhoneNumberAsc( personContacts );
                     this.sortedContactsCollection[ "E-mail_desc" ] = EMailDesc( personContacts );
                     this.sortedContactsCollection[ "E-mail_asc" ] = EMailAsc( personContacts );
                     this.sortedContactsCollection[ "Default" ] = DefaultPersonContacts( personContacts );
              }

              private IQueryable<ContactInfoVM> DefaultPersonContacts( IQueryable<ContactInfo>? personContacts )
              {
                     var contacts = this.mapper.ProjectTo<ContactInfo, ContactInfoVM>( personContacts );

                     return contacts;
              }

              private IQueryable<ContactInfoVM> PhoneNumberDesc( IQueryable<ContactInfo>? personContacts )
              {
                     var contacts = this.mapper
                                                     .ProjectTo<ContactInfo, ContactInfoVM>( personContacts )
                                                     .OrderByDescending( x => x.PhoneNumberOne );

                     return contacts;
              }

              private IQueryable<ContactInfoVM> PhoneNumberAsc( IQueryable<ContactInfo>? personContacts )
              {
                     var contacts = this.mapper
                                                    .ProjectTo<ContactInfo, ContactInfoVM>( personContacts )
                                                    .OrderBy( x => x.PhoneNumberOne );

                     return contacts;
              }

              private IQueryable<ContactInfoVM> EMailDesc( IQueryable<ContactInfo>? personContacts )
              {
                     var contacts = this.mapper
                                                     .ProjectTo<ContactInfo, ContactInfoVM>( personContacts )
                                                     .OrderByDescending( x => x.E_MailAddress1 );

                     return contacts;
              }

              private IQueryable<ContactInfoVM> EMailAsc( IQueryable<ContactInfo>? personContacts )
              {
                     var contacts = this.mapper
                                                     .ProjectTo<ContactInfo, ContactInfoVM>( personContacts )
                                                     .OrderBy( x => x.E_MailAddress1 );

                     return contacts;
              }
       }
}

