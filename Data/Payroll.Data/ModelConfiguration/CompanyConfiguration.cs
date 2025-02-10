using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.HasMany(c => c.Employees)
				   .WithOne(e => e.Company)
				   .HasForeignKey(f => f.CompanyId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(c => c.HeadquartersAddress)
							.WithMany(a => a.CompaniesHeadquarters)
							.HasForeignKey(f => f.HeadquartersAddressId)
							.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.ManagementAddress)
							.WithMany(a => a.ManagementAddresses)
							.HasForeignKey(f => f.ManagementAddressId)
							.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
