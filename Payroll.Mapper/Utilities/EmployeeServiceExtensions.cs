using Payroll.Data;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Mapper.Utilities
{
     public static class EmployeeServiceExtensions
     {
          //public static GetEmployeeDto? GetJobTitle(this GetEmployeeDto e, PayrollContext db, int empId)
          //{
          //     if (AnnexesCount(db, empId) > 0)
          //     {
          //          e.JobTitle = GetAnnexJobTitle(db, empId);
          //          return e;
          //     }

          //     e.JobTitle = db.EmploymentContracts
          //                    .Where(x => x.EmployeeId == empId && x.IsActive == true)
          //                    .Select(x => x.JobTitle)
          //                    .FirstOrDefault();

          //     return e;
          //}

          //public static GetEmployeeDto? GetDocumentName(this GetEmployeeDto e, PayrollContext db, int empId)
          //{

          //     string? documentName = db.IdDocuments
          //         .Where(x => x.EmployeeId == empId && x.IsValid == true)
          //         .Select(x => x.DocumentType.DocumentName)
          //         .FirstOrDefault();

          //     e.DocumentName = documentName;

          //     return e;
          //}

          //public static GetEmployeeDto? GetDocumentNumber(this GetEmployeeDto e, PayrollContext db, int empId)
          //{

          //     string? documentNumber = db.IdDocuments
          //          .Where(x => x.Person.EmployeeId == empId && x.IsValid == true)
          //          .Select(x => x.DocumentNumber)
          //          .FirstOrDefault();

          //     e.DocumentNumber = documentNumber;

          //     return e;
          //}

          //public static GetEmployeeDto? GetDepartmentName(this GetEmployeeDto e, PayrollContext db, int empId)
          //{
          //     if (AnnexesCount(db, empId) > 0)
          //     {
          //          e.DepartmentName = GetAnnexDepartmentName(db, empId);

          //          return e;
          //     }

          //     e.DepartmentName = db.EmploymentContracts
          //          .Where(x => x.EmployeeId == empId && x.IsActive == true)
          //          .Select(x => x.Department.Name)
          //          .FirstOrDefault();

          //     return e;
          //}

 //*************************************************************************
          private static int AnnexesCount(PayrollContext db, int empId)
          {
               int annexesCount = db.Annexes
                    .Where(x => x.EmpContract.EmployeeId == empId &&
                     x.EmpContract.IsActive == true)
                    .Count();

               return annexesCount;
          }

          private static string GetAnnexJobTitle(PayrollContext db, int empId)
          {
               string? jobTitle = db.Annexes
                    .Where(x => x.EmpContract.EmployeeId == empId && x.EmpContract.IsActive == true)
                    .OrderBy(x => x.DateOfAgreementOrChange)
                    .Select(x => x.JobTitle)
                    .ToList().FirstOrDefault();

               return jobTitle;
          }

          private static string? GetAnnexDepartmentName(PayrollContext db, int empId)
          {
               string? departmentName = db.Annexes
                    .Where(x => x.EmpContract.EmployeeId == empId &&
                     x.EmpContract.IsActive == true)
                    .OrderBy(x => x.DateOfAgreementOrChange)
                    .Select(x => x.Department.Name)
                    .FirstOrDefault();

               return departmentName;
          }
     }
}
