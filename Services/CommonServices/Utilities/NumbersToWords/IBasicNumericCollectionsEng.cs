namespace LegalFramework.Services.Utilities.NumbersToWords
{
	public interface IBasicNumericCollectionsEng
	{
		IDictionary<int, string> ZeroToNineDic { get; set; }

		IDictionary<int, string> TenToNineteenDic { get; set; }

		IDictionary<int, string> TensDic { get; set; }

		//IDictionary<int, string> HundredsDic { get; set; }

		IDictionary<int, string> TripleDegrees { get; set; }

	}
}
