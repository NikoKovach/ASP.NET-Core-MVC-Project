using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class DiplomaConfiguration : IEntityTypeConfiguration<Diploma>
     {
          public void Configure( EntityTypeBuilder<Diploma> builder )
          {
               builder.HasOne(d => d.EducationType)
                    .WithMany(e => e.Diplomas)
                    .HasForeignKey(f => f.EducationId)
                    .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
