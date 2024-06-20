using AutoMapper;
using Payroll.Data;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services.ServiceContracts;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
     public class CreateUpdateAddressService : CreateUpdateEntityService<AddressDto, Address>, ICreateUpdateEntity<AddressDto, Address>
     {
          public CreateUpdateAddressService(PayrollContext payrollContext, IMapper autoMapper) : base(payrollContext, autoMapper)
          {
          }

          public override Address CreateObject(AddressDto addressDto)
          {
               Person? person = GetPerson(addressDto.PersonId);

               Address address = Mapper.Map<Address>(addressDto);



               if (person != null && address.AddressType == AddressType.Permanent.ToString())
               {
                    if (address.Id == 0 && person.PermanentAddressId == null)
                    {
                         address.PersonPermanentAddresses.Add(person);
                    }

                    //Друг случай
                    //Когато полето address.HasBeenDeleted от false стане true ,
                    //то в Person съответното AddressId трябра да стане NULL !!!!!!
                    if (!PermanentAddressesContainsPerson(address.Id, person))
                    {
                         if (CurrentAddresessesContainsPerson(address.Id, person))
                         {
                              person.CurrentAddressId = null;
                         }

                         person.PermanentAddressId = address.Id;
                    }
               }
               else if (person != null && address.AddressType == AddressType.Present.ToString())
               {
                    if (address.Id == 0 && person.CurrentAddressId == null)
                    {
                         address.PersonCurrentAddresesses.Add(person);
                    }

                    if (!CurrentAddresessesContainsPerson(address.Id, person))
                    {
                         if (PermanentAddressesContainsPerson(address.Id, person))
                         {
                              person.PermanentAddressId = null;
                         }

                         person.CurrentAddressId = address.Id;
                    }
               }

               return address;
          }

          private bool PermanentAddressesContainsPerson(int addressId, Person person)
          {
               return Context.Addresses
                          .Where(x => x.Id == addressId)
                          .Select(x => x.PersonPermanentAddresses)
                          .FirstOrDefault().Contains(person);
          }

          private bool CurrentAddresessesContainsPerson(int addressId, Person person)
          {
               return Context.Addresses
                          .Where(x => x.Id == addressId)
                          .Select(x => x.PersonCurrentAddresesses)
                          .FirstOrDefault().Contains(person);
          }

          private Person? GetPerson(int? personId)
          {
               Person? validPerson = Context.Persons
                    .Where(x => x.Id == personId).FirstOrDefault();

               return validPerson;
          }
     }
}
