using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll.Models;


namespace Payroll.Data.ModelConfiguration
{
     public class TemporaryDisabilityConfiguration : IEntityTypeConfiguration<TemporaryDisability>
     {
          public void Configure( EntityTypeBuilder<TemporaryDisability> builder )
          {
               builder.HasOne(t => t.TypeSickSheet)
                      .WithMany(ts => ts.TemporaryDisabilities)
                      .HasForeignKey(f => f.TypeSickSheetId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(t => t.Gender)
                      .WithMany(g => g.TemporaryDisabilities)
                      .HasForeignKey(f => f.GenderId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(t => t.Employee)
                      .WithMany(e => e.TemporaryDisabilities)
                      .HasForeignKey(f => f.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( t => t.ModeOfTreatments )
                      .WithOne(m => m.TemporaryDisability)
                      .HasForeignKey(f => f.TemporaryDisabilityId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany(t => t.WorkingDaysByMonths)
                      .WithOne(w => w.TemporaryDisability)
                      .HasForeignKey(f => f.TemporaryDisabilityId)
                      .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
