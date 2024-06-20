using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class SalaryStatementConfiguration : IEntityTypeConfiguration<MonthlySalaryStatement>
     {
          public void Configure( EntityTypeBuilder<MonthlySalaryStatement> builder )
          {
               builder.HasOne(s => s.Employee)
                      .WithMany(e => e.MonthlySalaryStatements)
                      .HasForeignKey(f => f.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(s => s.PaymentType)
                      .WithMany(p => p.MonthlySalaryStatements)
                      .HasForeignKey(f => f.PaymentTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( s => s.RecapPartStatements )
                      .WithOne(r => r.MonthlySalaryStatement)
                      .HasForeignKey(f => f.SalaryStatementId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( s => s.IncomePartStatements)
                      .WithOne(i => i.MonthlySalaryStatement)
                      .HasForeignKey(f => f.SalaryStatementId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( s => s.DeductionPartStatements)
                      .WithOne(d => d.MonthlySalaryStatement)
                      .HasForeignKey(f => f.SalaryStatementId)
                      .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
