using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class PersonConfiguration : IEntityTypeConfiguration<Person>
     {
          public void Configure( EntityTypeBuilder<Person> builder )
          {
               builder.HasOne(p => p.Gender)
                      .WithMany(g => g.Persons)
                      .HasForeignKey(f => f.GenderId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(p => p.PermanentAddress)
                      .WithMany(a => a.PersonPermanentAddresses)
                      .HasForeignKey(f => f.PermanentAddressId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(p => p.CurrentAddress)
                      .WithMany(a => a.PersonCurrentAddresesses)
                      .HasForeignKey(f => f.CurrentAddressId)
                      .OnDelete(DeleteBehavior.Restrict);
               //###############################################
               builder.HasOne(p => p.Employee)
                      .WithOne(e => e.Person)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( p => p.ContactInfoList )
                    .WithOne(c => c.Person)
                    .HasForeignKey(f => f.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( p => p.IdDocuments )
                    .WithOne(d => d.Person)
                    .HasForeignKey(f => f.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( p => p.Diplomas )
                    .WithOne(d => d.Person)
                    .HasForeignKey(f => f.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
