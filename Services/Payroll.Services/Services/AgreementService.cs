using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models.EnumTables;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services
{
	public class AgreementService : IAgreementTypeService
	{
		private IRepository<ContractType> repository;
		private IMapEntity mapper;

		public AgreementService(IRepository<ContractType> repository, IMapEntity mapper)
		{
			this.repository = repository;

			this.mapper = mapper;
		}

		public IQueryable<AgreementTypeVM>? AllAgreements()
		{
			IQueryable<ContractType>? types = this.repository.AllAsNoTracking()
				   .Where(x => x.Type.Contains("contract"));

			IQueryable<AgreementTypeVM>? typesVMList = this.mapper
														   .ProjectTo<ContractType, AgreementTypeVM>(types);

			return typesVMList;
		}

		public async Task AddAsync(AgreementTypeVM agreementTypeVM)
		{
			if (string.IsNullOrEmpty(agreementTypeVM.Type))
				return;

			ContractType? agreementType = this.mapper.Map<AgreementTypeVM, ContractType>(agreementTypeVM);

			await this.repository.AddAsync(agreementType);

			await this.repository.SaveChangesAsync();
		}

		public async Task UpdateAsync(AgreementTypeVM agreementTypeVM)
		{
			if (agreementTypeVM.Id < 1)
				return;

			if (string.IsNullOrEmpty(agreementTypeVM.Type))
				return;

			ContractType? agreementType = this.mapper.Map<AgreementTypeVM, ContractType>(agreementTypeVM);

			this.repository.Update(agreementType);

			await this.repository.SaveChangesAsync();
		}

		public IQueryable<string?>? GetEntity(int? entityId)
		{
			IQueryable<string?>? agreementTypeName = this.AllAgreements()
														 .Where(x => x.Id == entityId)
														 .Select(x => x.Type);

			return agreementTypeName;
		}
	}
}
