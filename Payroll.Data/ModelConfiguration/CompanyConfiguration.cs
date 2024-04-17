using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class CompanyConfiguration : IEntityTypeConfiguration<Company>
     {
          public void Configure( EntityTypeBuilder<Company> builder )
          {
               builder.HasMany(c => c.Employees)
                      .WithOne(e => e.Company)
                      .HasForeignKey(f => f.CompanyId)
                      .OnDelete(DeleteBehavior.SetNull);
          }
     }
}
