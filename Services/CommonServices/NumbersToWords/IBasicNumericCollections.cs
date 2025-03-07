namespace LegalFramework.Services.NumbersToWords
{
    public interface IBasicNumericCollections
    {
        IDictionary<int, string> ZeroToNineDic { get; set; }

        IDictionary<int, string> TenToNineteenDic { get; set; }

        IDictionary<int, string> TensDic { get; set; }

        IDictionary<int, string> HundredsDic { get; set; }

        IDictionary<int, string> TripleDegrees { get; set; }

        public IDictionary<string, string> CoinSubunit { get; set; }
    }
}
