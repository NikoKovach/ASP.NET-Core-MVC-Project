
namespace LegalFramework.Services.NumbersToWords
{
    public class BasicNumericCollectionsEng : IBasicNumericCollections
    {
        public BasicNumericCollectionsEng( )
        {
            this.ZeroToNineDic = SetZeroToNineDictionary( );

            this.TenToNineteenDic = SetTenToNineteenDic( );

            this.TensDic = SetTensDictionary( );

            this.TripleDegrees = SetTripleDegreesDictionary( );

            this.CoinSubunit = SetCoinSubunitDictionary( );
        }

        public IDictionary<int, string> ZeroToNineDic { get; set; }

        public IDictionary<int, string> TenToNineteenDic { get; set; }

        public IDictionary<int, string> TensDic { get; set; }

        public IDictionary<int, string> HundredsDic { get; set; }

        public IDictionary<int, string> TripleDegrees { get; set; }

        public IDictionary<string, string> CoinSubunit { get; set; }

        //#############################################################

        private IDictionary<int, string>? SetZeroToNineDictionary( )
        {
            return new Dictionary<int, string>
            {
                {0,NumberConstantsEng.Zero },
                {1,NumberConstantsEng.One },
                {2,NumberConstantsEng.Two },
                {3,NumberConstantsEng.Three },
                {4,NumberConstantsEng.Four },
                {5,NumberConstantsEng.Five },
                {6,NumberConstantsEng.Six },
                {7,NumberConstantsEng.Seven },
                {8,NumberConstantsEng.Eight },
                {9,NumberConstantsEng.Nine },
            };
        }

        private IDictionary<int, string>? SetTenToNineteenDic( )
        {
            return new Dictionary<int, string>
            {
                {10,NumberConstantsEng.Ten },
                {11,NumberConstantsEng.Eleven },
                {12,NumberConstantsEng.Twelve },
                {13,NumberConstantsEng.Thirteen },
                {14,NumberConstantsEng.Fourteen},
                {15,NumberConstantsEng.Fifteen},
                {16,NumberConstantsEng.Sixteen},
                {17,NumberConstantsEng.Seventeen},
                {18,NumberConstantsEng.Eighteen},
                {19,NumberConstantsEng.Nineteen},
            };
        }

        private IDictionary<int, string>? SetTensDictionary( )
        {
            return new Dictionary<int, string>
            {
                {20,NumberConstantsEng.Twenty},
                {30,NumberConstantsEng.Thirty },
                {40,NumberConstantsEng.Fourty },
                {50,NumberConstantsEng.Fifty },
                {60,NumberConstantsEng.Sixty},
                {70,NumberConstantsEng.Seventy},
                {80,NumberConstantsEng.Eighty},
                {90,NumberConstantsEng.Ninety},
            };
        }

        private IDictionary<int, string>? SetTripleDegreesDictionary( )
        {
            return new Dictionary<int, string>
            {
                {1,NumberConstantsEng.Hundreds},
                {2,NumberConstantsEng.Thousands},
                {3,NumberConstantsEng.Million},
                {4,NumberConstantsEng.Billion},
            };

        }

        private IDictionary<string, string>? SetCoinSubunitDictionary( )
        {
            return new Dictionary<string, string>
            {
                {"€","cent"},
                {"$","cent"},
                {"￥","subunit"},// ¥ - Japan
                {"CHF","cent"},// CHF - Swiss
                {"zł","gr" },// zł - Poland
                {"R$","centavo" },//R$ - Brazil
                {"units" , "subunit"}
            };
        }
    }
}
