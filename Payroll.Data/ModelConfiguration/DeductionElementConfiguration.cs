using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
    public class DeductionElementConfiguration : IEntityTypeConfiguration<DeductionElement>
    {
        public void Configure(EntityTypeBuilder<DeductionElement> builder)
        {
            builder.HasOne(d => d.Employee)
                   .WithMany(e => e.DeductionElements)
                   .HasForeignKey(f => f.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.DeductionType)
                   .WithOne(dt => dt.DeductionElement)
                   .OnDelete(DeleteBehavior.Restrict);
        }

     }
}
