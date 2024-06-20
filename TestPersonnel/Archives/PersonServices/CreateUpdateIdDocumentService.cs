using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services.ServiceContracts;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
     public class CreateUpdateIdDocumentService : CreateUpdateEntityService<IdDocumentDto, IdDocument>, ICreateUpdateEntity<IdDocumentDto, IdDocument>
     {
          public CreateUpdateIdDocumentService(PayrollContext payrollContext, IMapper autoMapper) : base(payrollContext, autoMapper)
          {
          }

          public override IdDocument CreateObject(IdDocumentDto documentDto)
          {
               DocumentType? docType = SetDocumentType(documentDto.DocumentName, documentDto.DocumentTypeId);

               IdDocument document = Mapper.Map<IdDocument>(documentDto);

               document.DocumentType = docType;
               document.DocumentTypeId = docType.Id;

               return document;
          }

          public override bool UpdateRecord(IdDocument document)
          {
               try
               {
                    var docEntity = Context.Entry(document);
                    var docTypeEntity = Context.Entry(document.DocumentType);

                    if (docEntity.State == EntityState.Detached)
                    {
                         DbSet<IdDocument> docDbSet = Context.Set<IdDocument>();
                         docDbSet.Attach(document);
                    }

                    if (docTypeEntity.State == EntityState.Detached)
                    {
                         DbSet<DocumentType> docTypeDbSet = Context.Set<DocumentType>();
                         docTypeDbSet.Attach(document.DocumentType);
                    }

                    docEntity.State = EntityState.Modified;
                    docTypeEntity.State = EntityState.Modified;

                    Context.SaveChanges();

                    return true;
               }
               catch (Exception)
               {
                    return false;
               }
          }

          private DocumentType? SetDocumentType(string? documentName, int? documentTypeId)
          {
               if (documentName == null)
               {
                    return null;
               }

               DocumentType? docType = Context.DocumentTypes
                                       .Where(x => x.Id == documentTypeId)
                                       .SingleOrDefault();

               if (docType == null)
               {
                    DocumentType newEduType = new DocumentType { DocumentName = documentName };

                    return newEduType;
               }

               docType.DocumentName = documentName;

               return docType;
          }
     }
}
