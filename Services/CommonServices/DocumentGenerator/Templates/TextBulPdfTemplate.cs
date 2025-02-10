namespace LegalFramework.Services.DocumentGenerator.Templates
{
	public class TextBulPdfTemplate
	{
		public const string ContractText = "ТРУДОВ ДОГОВОР";

		public const string ContractNumber = "№ <<ContractNumber>> / <<ContractDateYear>> г.";

		public const string DocumentDateText = "Днес, <<ContractDate>> г. в гр.<<City>>, "
			+ "област <<Region>>, община <<Municipality>> между:";

		//#################################################################################

		public const string CompanyName = "<<companyName>>";

		public const string NoteCompanyText = "/ наименование на предприятието, "
		+ "учреждението или организацията /";

		public const string Headquarters = "със седалище и адрес на управление държава <<country>>, "
			+ "гр.<<town>>, ул.<<street>> № <<number>>, ";

		public const string BulstatManagerStr = "ЕИК по Булстат <<uniqueIdentifier>>, "
			+ "представлявано от <<representedBy>> - РЪКОВОДИТЕЛ с ЕГН <<representativeIdNumber>> "
			+ "от една страна, наричан/а/ за краткост в договора ";

		public const string ManagerStrPartOne = "РАБОТОДАТЕЛ";

		public const string ManagerStrPartTwo = " и ";
		//##################################################################################

		public const string EmpFullName = "<<EmployeeFullName>>";

		public const string NoteEmpNameText = "/ трите имена по документ за самоличност /";

		public const string NoteEmployee = "от друга страна, наричан/а/ ";

		public const string NoteEmployeePartTwo = "РАБОТНИК /СЛУЖИТЕЛ/";

		public const string NoteEmployeePartThree = " ,";

		public const string EmpCivilNumber = "с ЕГН: <<EmpCivilNumber>> ,";

		public const string EmpPermanentAddress = " постоянен адрес: гр.<<City>>, "
		+ "ул. „<<Street>>” № <<Number>>, община: <<Municipality>>, ";

		public const string EmpIdDocument = "лична карта №: <<CardNumber>>, "
		+ "издадена на <<DateOfIssue>> от <<IssuingAuthority>>, ";

		public const string EmpEducation = "oбразование: <<Education>>, специалност: <<Speciality>>, ";

		public const string EmpDiploma = "диплома №: <<DiplomaNumber>>, "
		+ "издадена от <<EducationalInstitution>>, ";

		public const string EmpWorkExperience = "с трудов стаж: <<years>> г., <<mounths>> м., "
		+ "<<days>> д., в т.ч. по специалността: <<years>> г., <<mounths>> м., <<days>> д.";


		public const string ContractReason = "    на основание: <<LaborCodeArticle>>, "
		+ "се сключи настоящият трудов договор:";

		//####################################################################################

		public const string PartOne = "I.\tРаботодателят възлага, а Работникът /Служителят/ приема:";

		public const string PartOnePointOne = "1. Да изпълнява в <<???>>, длъжността \" <<JobTitle>> \", НКИД: <<EAC>>, НКПД: <<NCOP>> ,";

		public const string PartOnePointDepartment = "в отдел (място на работа) \" <<DepartmentName>> \" със задължения, утвърдени от Работодателя в длъжностна характеристика, представляващa неразделна част към настоящия трудов договор;";

		public const string PartOnePointTwo = "2. При условията на <<WorkTime>> / <<WorkTimeInWords>> / часа работно време с основно месечно трудово възнаграждение в размер на <<Salary>> / <<SalaryInWords>> / с периодичност на изплащане << ??? >> и с допълнително възнаграждение за придобит трудов стаж и професионален опит <<PercentSWE>> % за всяка година трудов стаж при настоящия работодател / за всяка година трудов стаж на същата, сходна или със същия характер работа, длъжност или професия.";

		//#############################################################################

		public const string PartTwo = "II.\tРаботодателят е длъжен:";

		public const string PartTwoPointOne = "1. Да заплаща трудовото възнаграждение на Работника /Служителя/  ежемесечно;";

		public const string PartTwoPointTwo = "2. Да осигурява здравно и социално Работника /Служителя/ за всички осигурителни рискове съгласно условия и по ред, установени в КСО и ЗЗО;";

		public const string PartTwoPointThree = "3. Да запознае Работника /Служителя/ с установената технология на работа във фирмата;";

		public const string PartTwoPointFour = "4. Да създаде безопасни условия за работа на Работника /Служителя/.";

		//#############################################################################
		public const string PartThree = "III.\tРаботникът /Служителят/ е длъжен:";

		public const string PartThreePointOne = "1. Да изпълнява трудовите си задължения в съответствие с установената трудова и технологична дисциплина;";

		public const string PartThreePointTwo = "2. Да изпълнява стриктно всички задължения, които произтичат от длъжностната му характеристика;";

		public const string PartThreePointThree = "3. Да опазва поверените му технически средства;";

		public const string PartThreePointFour = "4. Да пази доброто име на фирмата, както и да не разпространява информация, считана от Работодателя за поверителна.";

		//################################################################################

		public const string PartFour = "IV.\tРАБОТНИКЪТ/ СЛУЖИТЕЛЯТ има право на следните отпуски:";

		public const string PartFourPartOne = "1. Основен/удължен платен годишен отпуск по "
		+ "чл.155 КТ за срок от <<PaidLeaveInDays>> работни дни.";

		public const string PartFourPartTwo = "2. Допълнителен платен годишен отпуск по "
		+ "чл.156 КТ за срок от <<AdditionalPaidAnnualLeaveInDays>> работни дни.";

		public const string PartFive = "V.\tСрокът на настоящия трудов договор е <<ProbationInMonths>> месеца.";

		public const string PartSix = "VI.\tСрокът на предизвестие за прекратяване на "
		+ "трудовия договор е <<NoticePeriodInDays>> дни.";

		public const string PartSeven = "VII.\tЗа неуредени в настоящия трудов договор условия "
		+ "се прилагат разпоредбите на Кодекса на труда ";

		public const string PartSevenPointOne = "и нормативните актове на българското законодателство.";

		public const string PartEight = "VIII.\tРаботникът /Служителят/  се задължава да постъпи "
		+ "на работа на <<StartingWorkDate>> г.";

		public const string AfterEight = "\tНастоящият договор се състави в два еднообразни екземпляра, "
		+ "по един за всяка от страните.";

		//###############################################################################

		public const string PartNine = "IX.\tДнес, <<ContractDate>> г. получих:";

		public const string PartNinePointOne = "1. Оригинал от настоящия трудов договор, "
		+ "подписан от двете страни;\r\n2. Копие на уведомление по чл.62, ал.3 от КТ, "
		+ "заверено от ТД на НАП;\r\n3. Длъжностна характеристика.";

		//################################################################################

		public const string Signature = "/ подпис /";

		public const string SignatureAndSeal = "/ подпис и печат /";

		public const string SignatureDotes = " ...........................";

		public const string EmpSignature = "РАБОТНИК/СЛУЖИТЕЛ/:";

		public const string ManagerSignature = "РАБОТОДАТЕЛ:";

		public const string HRDepartment = "Отдел “Човешки ресурси”:";

		public const string RealDateOfAdmission = "Работникът /Служителят/ постъпи на работа на <<.....>> г.";
	}
}

/*

 */