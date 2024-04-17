using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Models;

namespace Payroll.Data.ModelConfiguration
{
     public class IdDocumentConfiguration : IEntityTypeConfiguration<IdDocument>
     {
          public void Configure( EntityTypeBuilder<IdDocument> builder )
          {
               builder.HasOne(d => d.DocumentType)
                      .WithMany(dt => dt.IdDocuments)
                      .HasForeignKey(f => f.DocumentTypeId)
                      .OnDelete(DeleteBehavior.SetNull);
          }
     }
}
