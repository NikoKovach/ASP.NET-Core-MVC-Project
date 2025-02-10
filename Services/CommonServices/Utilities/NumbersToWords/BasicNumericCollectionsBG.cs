
namespace LegalFramework.Services.Utilities.NumbersToWords
{
	public class BasicNumericCollectionsBG : IBasicNumericCollectionsBG
	{
		public BasicNumericCollectionsBG()
		{
			this.ZeroToNineDic = this.SetZeroToNineDictionary();

			this.TenToNineteenDic = this.SetTenToNineteenDic();

			this.TensDic = this.SetTensDictionary();

			this.HundredsDic = this.SetHundredsDictionary();

			this.TripleDegrees = this.SetTripleDegreesDictionary();
		}

		public IDictionary<int, string> ZeroToNineDic { get; set; }

		public IDictionary<int, string> TenToNineteenDic { get; set; }

		public IDictionary<int, string> TensDic { get; set; }

		public IDictionary<int, string> HundredsDic { get; set; }

		public IDictionary<int, string> TripleDegrees { get; set; }

		//#############################################################

		private IDictionary<int, string>? SetZeroToNineDictionary()
		{
			return new Dictionary<int, string>
			{
				{0,NumberConstantsBG.Zero },
				{1,NumberConstantsBG.One },
				{2,NumberConstantsBG.Two },
				{3,NumberConstantsBG.Three },
				{4,NumberConstantsBG.Four },
				{5,NumberConstantsBG.Five },
				{6,NumberConstantsBG.Six },
				{7,NumberConstantsBG.Seven },
				{8,NumberConstantsBG.Eight },
				{9,NumberConstantsBG.Nine },
			};
		}

		private IDictionary<int, string>? SetTenToNineteenDic()
		{
			return new Dictionary<int, string>
			{
				{10,NumberConstantsBG.Ten },
				{11,NumberConstantsBG.Eleven },
				{12,NumberConstantsBG.Twelve },
				{13,NumberConstantsBG.Thirteen },
				{14,NumberConstantsBG.Fourteen},
				{15,NumberConstantsBG.Fifteen},
				{16,NumberConstantsBG.Sixteen},
				{17,NumberConstantsBG.Seventeen},
				{18,NumberConstantsBG.Eighteen},
				{19,NumberConstantsBG.Nineteen},
			};
		}

		private IDictionary<int, string>? SetTensDictionary()
		{
			return new Dictionary<int, string>
			{
				{20,NumberConstantsBG.Twenty},
				{30,NumberConstantsBG.Thirty },
				{40,NumberConstantsBG.Fourty },
				{50,NumberConstantsBG.Fifty },
				{60,NumberConstantsBG.Sixty},
				{70,NumberConstantsBG.Seventy},
				{80,NumberConstantsBG.Eighty},
				{90,NumberConstantsBG.Ninety},
			};
		}

		private IDictionary<int, string>? SetHundredsDictionary()
		{
			return new Dictionary<int, string>
			{
				{100,NumberConstantsBG.OneHundred},
				{200,NumberConstantsBG.TwoHundred},
				{300,NumberConstantsBG.ThreeHundred},
				{400,NumberConstantsBG.FourHundred},
				{500,NumberConstantsBG.FiveHundred},
				{600,NumberConstantsBG.SixHundred},
				{700,NumberConstantsBG.SevenHundred},
				{800,NumberConstantsBG.EightHundred},
				{900,NumberConstantsBG.NineHundred},
			};
		}

		private IDictionary<int, string>? SetTripleDegreesDictionary()
		{
			return new Dictionary<int, string>
			{
				{2,NumberConstantsBG.PostfixThousands},
				{3,NumberConstantsBG.PostfixMillion},
				{4,NumberConstantsBG.PostfixBillion},
			};

		}
	}
}
