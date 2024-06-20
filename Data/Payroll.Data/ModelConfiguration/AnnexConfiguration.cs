using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class AnnexConfiguration : IEntityTypeConfiguration<Annex>
     {
          public void Configure( EntityTypeBuilder<Annex> builder )
          {
               builder.HasOne(a => a.Department)
                      .WithMany(d => d.Annexes)
                      .HasForeignKey(f => f.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.ContractType)
                      .WithMany(a => a.Annexes)
                      .HasForeignKey(f => f.ContractTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
