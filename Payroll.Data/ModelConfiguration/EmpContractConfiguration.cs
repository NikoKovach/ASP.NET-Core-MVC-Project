using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class EmpContractConfiguration : IEntityTypeConfiguration<EmploymentContract>
     {
          public void Configure( EntityTypeBuilder<EmploymentContract> builder )
          {
               builder.HasOne(c => c.ContractType)
                      .WithMany(ct => ct.EmploymentContracts)
                      .HasForeignKey(f => f.ContractTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(c => c.LaborCodeArticle)
                      .WithMany(l => l.Contracts)
                      .HasForeignKey(f => f.LaborCodeArticleId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(c => c.Department)
                      .WithMany(l => l.EmploymentContracts)
                      .HasForeignKey(f => f.DeparmentId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne( c => c.Employee )
                      .WithOne(e => e.EmploymentContract)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(c => c.PlaceOfRegistration)
                      .WithMany(p => p.EmploymentContracts)
                      .HasForeignKey(f => f.PlaceId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasOne(c => c.WorkPlace)
                      .WithMany(p => p.WorkPlaceEmploymentContracts)
                      .HasForeignKey(f => f.WorkPlaceId)
                      .OnDelete(DeleteBehavior.Restrict);

               builder.HasMany( ec => ec.SupplementaryAgreements )
                      .WithOne(sa => sa.EmpContract)
                      .HasForeignKey(f => f.EmpContractId)
                      .OnDelete(DeleteBehavior.Restrict);
          }
     }
}
