using System.Text;

using LegalFramework.Services.Utilities;

using MigraDoc.DocumentObjectModel;

using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace LegalFramework.Services.DocumentGenerator.FillWayTemplates
{
	public class FillLaborContractBulPdf : IFill
	{
		private LaborAgreementVM agreement;
		private AddressEmpVM conclusionAddress;
		private CompanyVM company;
		private AddressEmpVM companyHeadquarters;
		private PersonInfoContractVM employee;

		private readonly StringBuilder sb;

		public FillLaborContractBulPdf()
		{
			this.sb = new StringBuilder();
		}

		public bool Fill(ITemplate? document, object[]? documentModels)
		{
			if (document == null) return false;

			if (documentModels == null || documentModels.Length < 1) return false;

			this.agreement = (LaborAgreementVM)documentModels[0];

			this.conclusionAddress = (AddressEmpVM)documentModels[1];

			this.company = (CompanyVM)documentModels[2];

			this.companyHeadquarters = (AddressEmpVM)documentModels[3];

			this.employee = (PersonInfoContractVM)documentModels[4];

			if (!FillMainSection(document)) return false;

			return true;
		}

		private bool FillMainSection(ITemplate document)
		{
			Dictionary<string, Paragraph> section = document.SectionsDic["laborMainSection"];

			try
			{
				this.FillContractNumberParagraph(section["contractNumber"]);

				this.FillRegistationPlaceParagraph(section["registationPlace"]);

				this.FillCompanyName(section["companyName"]);

				this.FillCompanyInfo(section["companyInformation"]);

				this.FillEmployeeName(section["employeeName"]);

				this.FillEmployeeInfo(section["employeeInformation"]);

				this.FillContractReason(section["contractReason"]);

				this.FillPartOnePointOne(section["contractOne-One"]);

				this.FillPartOnePointTwo(section["contractOne-Two"]);

				this.FillPartOneDepartment(section["contractOneDepartment"]);

				this.FillPartFourBasicLeave(section["basicLeave"]);

				this.FillPartFourAdditionalLeave(section["additionalLeave"]);

				this.FillPartFive(section["contractTerm"]);

				this.FillPartSix(section["noticePeriod"]);

				this.FillPartEight(section["admissionDate"]);

				this.FillPartNine(section["paraPartNine"]);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		//######################################################################

		private void FillContractNumberParagraph(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<ContractNumber>>",
							FillUtilities.NewValue(this.agreement.ContractNumber));

			this.sb.Replace("<<ContractDateYear>>",
							FillUtilities.NewValue(this.agreement.ContractDate, "yyyy"));

			FillUtilities.FillParagraph(paragraph, this.sb);

			paragraph.Format.Font.Bold = true;

			paragraph.Format.Font.Size = Unit.FromPoint(12);
		}

		private void FillRegistationPlaceParagraph(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<ContractDate>>",
							FillUtilities.NewValue(this.agreement.ContractDate, "dd.MM.yyyy"));

			this.sb.Replace("<<City>>", FillUtilities.NewValue(this.conclusionAddress.City));

			this.sb.Replace("<<Region>>", FillUtilities.NewValue(this.conclusionAddress.Region));

			this.sb.Replace("<<Municipality>>",
							FillUtilities.NewValue(this.conclusionAddress.Municipality));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillCompanyName(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<companyName>>", this.company.Name);

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillCompanyInfo(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph.Elements.First, this.sb);

			this.sb.Replace("<<country>>", FillUtilities.NewValue(this.companyHeadquarters.Country));

			this.sb.Replace("<<town>>", FillUtilities.NewValue(this.companyHeadquarters.City));

			this.sb.Replace("<<street>>", FillUtilities.NewValue(this.companyHeadquarters.Street));

			this.sb.Replace("<<number>>", BuildAddressNumberText());

			this.sb.Replace("<<uniqueIdentifier>>", this.company.UniqueIdentifier);

			this.sb.Replace("<<representedBy>>", this.company.RepresentedBy);

			this.sb.Replace("<<representativeIdNumber>>",
							 FillUtilities.NewValue(this.company.RepresentativeIdNumber));

			FillUtilities.FillParagraphElement(paragraph.Elements.First, this.sb);
		}

		private void FillEmployeeName(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<EmployeeFullName>>", FillUtilities.NewValue(this.employee.FullName));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillEmployeeInfo(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<EmpCivilNumber>>", FillUtilities.NewValue(this.employee.CivilNumber));

			this.sb.Replace("<<City>>", FillUtilities.NewValue(this.employee.PermanentAddress.City));

			this.sb.Replace("<<Street>>", FillUtilities.NewValue(this.employee.PermanentAddress.Street));

			this.sb.Replace("<<Number>>",
							FillUtilities.NewValue(this.employee.PermanentAddress.Number.ToString()));

			this.sb.Replace("<<Municipality>>",
							FillUtilities.NewValue(this.employee.PermanentAddress.Municipality));

			this.sb.Replace("<<CardNumber>>", FillUtilities.NewValue(this.employee.DocumentNumber));

			this.sb.Replace("<<DateOfIssue>>", FillUtilities.NewValue(this.employee.IdDocumentDateOfIssue, "dd.MM.yyyy"));

			this.sb.Replace("<<IssuingAuthority>>", FillUtilities.NewValue(this.employee.IssuingAuthority));

			this.sb.Replace("<<Education>>", FillUtilities.NewValue(this.employee.EducationName));

			this.sb.Replace("<<Speciality>>", FillUtilities.NewValue(this.employee.Speciality));

			this.sb.Replace("<<DiplomaNumber>>", FillUtilities.NewValue(this.employee.DiplomaRegNumber));

			this.sb.Replace("<<EducationalInstitution>>",
							FillUtilities.NewValue(this.employee.EducationalInstitution));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		//#################################################################

		private void FillContractReason(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<LaborCodeArticle>>", FillUtilities.NewValue(this.agreement.LaborCodeArticle));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillPartOnePointOne(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<JobTitle>>", FillUtilities.NewValue(this.agreement.JobTitle));

			this.sb.Replace("<<EAC>>", FillUtilities.NewValue(this.agreement.EAC));

			this.sb.Replace("<<NCOP>>", FillUtilities.NewValue(this.agreement.NCOP));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillPartOneDepartment(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<DepartmentName>>", FillUtilities.NewValue(this.agreement.DepartmentName));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillPartOnePointTwo(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<WorkTime>>", FillUtilities.NewValue(this.agreement.WorkTime));

			this.sb.Replace("<<WorkTimeInWords>>", FillUtilities.TheNumberInWords(this.agreement.WorkTime));

			this.sb.Replace("<<Salary>>", FillUtilities.NewValue(this.agreement.Salary));

			this.sb.Replace("<<SalaryInWords>>", FillUtilities.TheNumberInWords(this.agreement.Salary));

			this.sb.Replace("<<PercentSWE>>", FillUtilities.NewValue(this.agreement.PercentSWE));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillPartFourBasicLeave(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<PaidLeaveInDays>>", FillUtilities.NewValue(this.agreement.PaidLeaveInDays));

			FillUtilities.FillParagraph(paragraph, this.sb);
		}

		private void FillPartFourAdditionalLeave(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph, this.sb);

			this.sb.Replace("<<AdditionalPaidAnnualLeaveInDays>>",
							FillUtilities.NewValue(this.agreement.AdditionalPaidAnnualLeaveInDays));

			FillUtilities.FillParagraph(paragraph, this.sb);

		}

		private void FillPartFive(Paragraph paragraph)
		{
			FillUtilities.AddTextToStringBuilder(paragraph.Elements.LastObject, this.sb);

			this.sb.Replace("<<ProbationInMonths>>",
							FillUtilities.NewValue(this.agreement.ProbationInMonths));

			FillUtilities.FillParagraphElement(paragraph.Elements.LastObject, this.sb);
		}

		private void FillPartSix(Paragraph paragraph)
		{
			//FillUtilities.AddTextToStringBuilder(paragraph.Elements.LastObject, this.sb);

			//this.sb.Replace("<<NoticePeriodInDays>>",
			//				FillUtilities.NewValue(this.agreement.NoticePeriodInDays));

			//FillUtilities.FillParagraphElement(paragraph.Elements.LastObject, this.sb);

			//#####################################
			string newValue = FillUtilities.NewValue(this.agreement.NoticePeriodInDays);

			int elementIndex = paragraph.Elements.IndexOf(paragraph.Elements.LastObject);

			FillUtilities.SetBoldText(paragraph, elementIndex, "<<NoticePeriodInDays>>", newValue);
		}

		private void FillPartEight(Paragraph paragraph)
		{
			//FillUtilities.AddTextToStringBuilder(paragraph.Elements.LastObject, this.sb);

			//this.sb.Replace("<<StartingWorkDate>>",
			//				FillUtilities.NewValue(this.agreement.StartingWorkDate, "dd.MM.yyyy"));

			//FillUtilities.FillParagraphElement(paragraph.Elements.LastObject, this.sb);

			//###########################
			string newValue = FillUtilities.NewValue(this.agreement.StartingWorkDate, "dd.MM.yyyy");

			int elementIndex = paragraph.Elements.IndexOf(paragraph.Elements.LastObject);

			FillUtilities.SetBoldText(paragraph, elementIndex, "<<StartingWorkDate>>", newValue);

		}

		private void FillPartNine(Paragraph paragraph)
		{
			string newValue = FillUtilities.NewValue(this.agreement.ContractDate, "dd.MM.yyyy");

			int elementIndex = paragraph.Elements.IndexOf(paragraph.Elements.LastObject);

			FillUtilities.SetBoldText(paragraph, elementIndex, "<<ContractDate>>", newValue);

		}

		//##################################################################

		private string BuildAddressNumberText()
		{
			string addressNumber = FillUtilities.NewValue(this.companyHeadquarters.Number);

			if (addressNumber.Equals(FillUtilities.TextFromDots)) return addressNumber;

			if (this.companyHeadquarters.Entrance != null)
			{
				addressNumber = $"{addressNumber}, вх.{this.companyHeadquarters.Entrance}";
			}

			if (this.companyHeadquarters.Floor != null)
			{
				addressNumber = $"{addressNumber}, ет.{this.companyHeadquarters.Floor}";
			}

			if (this.companyHeadquarters.ApartmentNumber != null)
			{
				addressNumber = $"{addressNumber}, ап.{this.companyHeadquarters.ApartmentNumber}";
			}

			return addressNumber;
		}
	}
}
