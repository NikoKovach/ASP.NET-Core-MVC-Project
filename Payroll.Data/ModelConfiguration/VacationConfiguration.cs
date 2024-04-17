using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class VacationConfiguration : IEntityTypeConfiguration<Vacation>
     {
          public void Configure( EntityTypeBuilder<Vacation> builder )
          {
               builder.HasOne(v => v.TypeVacation)
                      .WithMany(tv => tv.Vacations)
                      .HasForeignKey(f => f.TypeVacationId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(v => v.Employee)
                      .WithMany(e => e.Vacations)
                      .HasForeignKey(f => f.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany(v => v.WorkingDaysByMonths)
                      .WithOne(w => w.Vacation)
                      .HasForeignKey(f => f.VacationId)
                      .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
