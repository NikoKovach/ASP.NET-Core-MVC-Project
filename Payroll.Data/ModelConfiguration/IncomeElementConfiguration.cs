using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
    public class IncomeElementConfiguration : IEntityTypeConfiguration<IncomeElement>
    {
        public void Configure(EntityTypeBuilder<IncomeElement> builder)
        {
               builder.HasOne( i => i.Employee )
                      .WithMany( e => e.IncomeElements )
                      .HasForeignKey( f => f.EmployeeId )
                      .OnDelete( DeleteBehavior.Restrict );

               builder.HasOne( i => i.IncomeType )
                      .WithOne( it => it.IncomeElement)
                      .OnDelete( DeleteBehavior.Restrict );
          }
    }
}
