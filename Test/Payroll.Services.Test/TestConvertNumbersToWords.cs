using LegalFramework.Services.NumbersToWords;

using NUnit.Framework;


namespace Payroll.Services.Test
{
    [TestFixture]
    internal class TestConvertNumbersToWords : ConvertingNumberToWords
    {
        [TestCaseSource( nameof( WholeNumbersList ) )]
        public void Method_CountTheNumberOfDigits_ShouldReturnIntegerPositiveNumber
                                                ( long number, int expectedResult )
        {
            int digitsCount = this.CountTheNumberOfDigits( number );

            Assert.That( digitsCount, Is.EqualTo( expectedResult ) );
        }

        [TestCaseSource( nameof( DecimalNumbersList ) )]
        public void Method_GetWholePart_ReturnWholePartFromNumber
                                                ( decimal number, long resultWholePart )
        {
            long wholePart = this.GetWholePart( number );

            Assert.That( wholePart, Is.EqualTo( resultWholePart ) );
        }

        [TestCaseSource( nameof( NumbersListReturnFractional ) )]
        public void Method_GetFractionalPart_ReturnFractionalParttoTheFourthDigit
                                                ( decimal number, decimal result )
        {
            decimal fractionalPart = this.GetFractionalPart( number );

            Assert.That( fractionalPart, Is.EqualTo( result ) );
        }

        [TestCaseSource( nameof( DegreeDicTestDictionaryCount ) )]
        public void Method_SetDegreeDictionary_ReturnDictionaryWithMaxCountThree
                                                ( int wholePart, int resultCount )
        {
            int count = this.SetDegreeDictionary( wholePart ).Count;

            Assert.That( count, Is.EqualTo( resultCount ) );
        }

        [TestCaseSource( nameof( TestParameters_SetDegreeDictionary ) )]
        public void Method_SetDegreeDictionary_ReturnDictionary
                                                ( int wholePart, Dictionary<int, int> resultDic )
        {
            Dictionary<int, int> dictionary = this.SetDegreeDictionary( wholePart );

            Assert.That( dictionary[1], Is.EqualTo( resultDic[1] ) );
        }

        [TestCaseSource( nameof( TestParameters_SetRemainingValueMethod ) )]
        public void Method_SetRemainingValue_ReturnsTheRemainderOfTheIntegerToConvertInWords
                                                ( long basis, int index, long resultValue )
        {
            long remainingValue = this.SetRemainingValue( basis, index );

            Assert.That( remainingValue, Is.EqualTo( resultValue ) );
        }

        [TestCaseSource( nameof( TestParameters_SetThreesomeMethod ) )]
        public void Method_SetThreesome_ReturnIntegerNumberLessThen1000
                                                ( long value, int index, int resultValue )
        {
            int threesome = this.SetThreesome( value, index );

            Assert.That( threesome, Is.EqualTo( resultValue ) );
        }

        [TestCaseSource( nameof( TestParameters_ParseWholePartRegularNumber ) )]
        public void Method_ParseWholePart_ConvertToRegularNumber
                                                ( long wholePart, string resultType, string result )
        {
            string numberInWords = this.ParseWholePart( wholePart, resultType );

            Assert.That( numberInWords, Is.EqualTo( result ) );
        }

        [TestCaseSource( nameof( TestParameters_ParseFractionalPartToRegularNumber ) )]
        public void Method_ParseFractionalPart_ConvertToRegularNumber
                                            ( decimal fractionalPart, string resultType, string result )
        {
            string fractionalPartInWords = this.ParseFractionalPart( fractionalPart, resultType );

            Assert.That( fractionalPartInWords, Is.EqualTo( result ) );
        }

        [TestCaseSource( nameof( TestParameters_ParseFractionalPartToCurrency ) )]
        public void Method_ParseFractionalPart_ConvertToCurrency
                                            ( decimal number, string resultType, string result )
        {
            string? fractionalPartInWords = this.WriteNumberInWords( number, resultType );

            Assert.That( this.FractionalPartStr, Is.EqualTo( result ) );
        }

        [TestCaseSource( nameof( TestParameters_WriteNumberInWords_ResultTypeNumber ) )]
        public void Method_WriteNumberInWords_ReturnRegularNumberInWords
                                                ( decimal number, string result )
        {
            string? numberInWords = this.WriteNumberInWords( number );

            Assert.That( numberInWords, Is.EqualTo( result ) );
        }

        [TestCaseSource( nameof( TestParameters_WriteNumberInWords_ResultTypeCurrency ) )]
        public void Method_WriteNumberInWords_ReturnCurrency
                                                ( decimal number, string cultureName, string result )
        {
            string? numberInWords = this.WriteNumberInWords( number, SupportConstants.Currency, cultureName );

            Assert.That( numberInWords, Is.EqualTo( result ) );
        }

        //##########################################################

        public static object[] WholeNumbersList =
        {
            new object[] { 1, 1,},
            new object[] { 12, 2,},
            new object[] { 100, 3,},
            new object[] { 1000, 4,},
            new object[] { 11_000, 5,},
            new object[] { 100_000, 6,},
            new object[] { 1_000_000, 7,},
            new object[] { 12_000_000, 8,},
            new object[] { 100_000_000, 9,},
            new object[] { 1_000_000_000, 10,},
            new object[] { 11_000_000_000, 11,},
            new object[] { 111_000_000_000, 12,},
            new object[] { 1_111_000_000_000, 12,}
        };

        public static object[] DecimalNumbersList =
        {
            new object[] { 1.0m, 1,},
            new object[] { 9.9m, 9,},
            new object[] { 11m, 11,},
            new object[] { 12.01m, 12,},
            new object[] { 199.333m, 199,},
            new object[] { 1002.0001m, 1002,},
            new object[] { 1_000_011.0001245m, 1_000_011, },
            new object[] { 12_001_000.1100458m, 12_001_000, },
            new object[] { 101_102_005.00012358m, 101_102_005, },
            new object[] { 1_999_999_999m, 1_999_999_999, },
            new object[] { 11_000_000_000.99m, 11_000_000_000, },
            new object[] { 110_000_000_000.99m, 110_000_000_000, },
        };

        public static object[] NumbersListReturnFractional =
        {
            new object[] { 1.1m, 0.1m,},
            new object[] { 1.01m, 0.01m,},
            new object[] { 1.001m, 0.001m,},
            new object[] { 1.0001m, 0.0001m,},
            new object[] { 1.10m, 0.1m,},
            new object[] { 1.100m, 0.1m,},
            new object[] { 1.1000m, 0.1m,},
            new object[] { 1.101m, 0.101m,},
            new object[] { 1.1001m, 0.1001m,},
            new object[] { 1.0010m, 0.001m,},
            new object[] { 1.099878855275m, 0.0999m,},
            new object[] { 1.00045556554m, 0.0005m,},
            new object[] { 1.099978855275m, 0.1m,},
        };

        public static object[] DegreeDicTestDictionaryCount =
        {
            new object[] {1,1},
            new object[] {10,2},
            new object[] {100,3},
            new object[] {9,1},
            new object[] {99,2},
            new object[] {999,3},
        };

        public static object[] TestParameters_SetDegreeDictionary =
        {
            new object[] {
                            1,new Dictionary<int, int>( )
                              {
                                  {1,1 }
                              }
                         },
            new object[] {10,new Dictionary<int, int>( )
                             {
                                 {1,0 },
                                 {10,10 },
                             }
                         },
            new object[] {100,new Dictionary<int, int>( )
                              {
                                  {1,0 },
                                  {10,0 },
                                  {100,1 },
                              }
                         },
            new object[] {9,new Dictionary<int, int>( )
                              {
                                  {1,9 }
                              }
                         },
            new object[] {99,new Dictionary<int, int>( )
                             {
                                 {1,9 },
                                 {10,9 },
                             }
                         },
            new object[] {999,new Dictionary<int, int>( )
                              {
                                  {1,9 },
                                  {10,9 },
                                  {100,9 },
                              }
                         },
        };

        public static object[] TestParameters_SetRemainingValueMethod =
        {
            new object[] {1,1,1},
            new object[] {10,1,10},
            new object[] {100,1,100},
            new object[] {1003,2,1003},
            new object[] {1003,1,3},
            new object[] {10003,2,10003},
            new object[] {10003,1,3},
            new object[] {10203,1,203},

            new object[] {999000,2,999000},
            new object[] {999000,1,0},
            new object[] {1_001_222,3,1001222},
            new object[] {1_001_222,2,1222},
            new object[] {1_001_222, 1,222},
            new object[] {10_003_999,3,10003999},
            new object[] {10_003_999,2,3999},
            new object[] {10_203_999,1,999},
        };

        public static object[] TestParameters_SetThreesomeMethod =
        {
            new object[] {999_000_001,3,999},
            new object[] {1,2,0},
            new object[] {1,1,1},
            new object[] {1_001_222,3,1},
            new object[] {1_222,2,1},
            new object[] {222,1,222},
            new object[] {10_003_010,3,10},
            new object[] {3_010,2,3},
            new object[] {010,1,10},

            new object[] {21_555_001_000,4,21},
            new object[] {555_001_000,3,555},
            new object[] {001_000,2,1},
            new object[] {0,1,0},
			//new object[] {1003,1,3},
			//new object[] {10003,2,10003},
			//new object[] {10003,1,3},
			//new object[] {10203,1,203},
		};

        public static object[] TestParameters_ParseWholePartRegularNumber =
        {
            new object[] {1,"number",NumberConstantsEng.One},
            new object[] {9,"number",NumberConstantsEng.Nine},
            new object[] {10,"number",NumberConstantsEng.Ten},
            new object[] {11,"number",NumberConstantsEng.Eleven},
            new object[] {19,"number",NumberConstantsEng.Nineteen},
            new object[] {20,"number",NumberConstantsEng.Twenty},
            new object[] {90,"number",NumberConstantsEng.Ninety},
            new object[] {41,"number",NumberConstantsEng.Fourty + "-" + NumberConstantsEng.One},
            new object[] {99,"number",NumberConstantsEng.Ninety + "-" + NumberConstantsEng.Nine},
            new object[] {100,"number",NumberConstantsEng.One + " " + NumberConstantsEng.Hundreds},
            new object[] {1000,"number",NumberConstantsEng.One + " " + NumberConstantsEng.Thousands},
            new object[] {10000,"number",NumberConstantsEng.Ten + " " + NumberConstantsEng.Thousands},
            new object[] {10_001,"number","ten thousand one"},
            new object[] {100_000,"number","one hundred " + NumberConstantsEng.Thousands},
            new object[] {100_001,"number", "one hundred thousand " + NumberConstantsEng.One},
            new object[] {100_010,"number", "one hundred thousand " + NumberConstantsEng.Ten},
            new object[] {100_100,"number", "one hundred thousand one hundred"},
            new object[] {110_000,"number","one hundred ten " + NumberConstantsEng.Thousands},
            new object[] {111_000,"number","one hundred eleven " + NumberConstantsEng.Thousands},
            new object[] {111_001,"number", "one hundred eleven thousand one"},
            new object[] {111_111,"number", "one hundred eleven thousand one hundred eleven"},
            new object[] {111_199,"number", "one hundred eleven thousand one hundred ninety-nine"},
            new object[] {1_000_000,"number",NumberConstantsEng.One + " " + NumberConstantsEng.Million},
            new object[] {100_000_000,"number", "one hundred " + NumberConstantsEng.Million},
            new object[] {1_000_000_000,"number",NumberConstantsEng.One + " " + NumberConstantsEng.Billion},
            new object[] { 1_000_000_001, "number","one billion one"},
            new object[] { 1_100_000_000, "number","one billion one hundred million"},

            new object[] { 1_100_100_000, "number","one billion one hundred million one hundred thousand"},
            new object[] { 1_100_001_000, "number","one billion one hundred million one thousand"},
            new object[] { 55_444_333_111, "number","fifty-five billion four hundred fourty-four million "
                                            + "three hundred thirty-three thousand one hundred eleven"},
        };

        public static object[] TestParameters_ParseFractionalPartToRegularNumber =
        {
            new object[] {0.1m,"number","one"},
            new object[] {0.01m,"number","nought one"},
            new object[] {0.001m,"number","nought nought one"},
            new object[] {0.0001m,"number","nought nought nought one"},
            new object[] {0.1000m,"number","one"},
            new object[] {0.0100m,"number","nought one"},
            new object[] {0.0010m,"number","nought nought one"},
            new object[] {0.12m,"number","one two"},
            new object[] {0.19m,"number","one nine"},
            new object[] {0.22m,"number","two two"},
            new object[] {0.101m,"number","one nought one"},

            new object[] {0.110m,"number","one one"},
            new object[] {0.199m,"number","one nine nine"},
            new object[] {0.1001m,"number","one nought nought one"},
            new object[] {0.1010m,"number","one nought one"},
            new object[] {0.1101m,"number","one one nought one"},
            new object[] {0.9999m,"number","nine nine nine nine"},
            new object[] {0.0999m,"number","nought nine nine nine"},
        };

        public static object[] TestParameters_ParseFractionalPartToCurrency =
        {
            new object[] {1.1m,"currency","10"},
            new object[] {1.01m, "currency", "01"},
            new object[] {1.001m, "currency", "00"},
            new object[] {1.0001m, "currency", "00"},
            new object[] {1.1000m,"currency", "10"},
            new object[] {1.0100m,"currency", "01"},
            new object[] {1.0010m, "currency", "00"},
            new object[] {1.12m, "currency", "12"},
            new object[] {1.19m, "currency", "19"},
            new object[] {1.22m, "currency", "22"},
            new object[] {1.101m, "currency", "10"},
            new object[] {1.110m,"currency","11"},
            new object[] {1.199m, "currency", "20"},
            new object[] {1.1001m,"currency","10"},
            new object[] {1.1010m,"currency","10"},
            new object[] {1.1101m,"currency","11"},
            new object[] {1.999555555m,"currency","00"},
            new object[] {1.0999m, "currency", "10"},
        };

        public static object[] TestParameters_WriteNumberInWords_ResultTypeNumber =
        {
            new object[] {0.1m,"point one"},
            new object[] {1.1m,"one point one"},
            new object[] {9.01m,"nine point nought one"},
            new object[] {10.001m,"ten point nought nought one"},
            new object[] {11.0001m,"eleven point nought nought nought one"},
            new object[] {19.1000m,"nineteen point one"},
            new object[] {20.0100m,"twenty point nought one"},
            new object[] {90.0010m,"ninety point nought nought one"},
            new object[] {99.12m,"ninety-nine point one two"},
            new object[] {100.19m,"one hundred point one nine"},
            new object[] {101.22m,"one hundred one point two two"},
            new object[] {112.101m,"one hundred twelve point one nought one"},

            new object[] {999.110m,"nine hundred ninety-nine point one one"},
            new object[] {1000.199m,"one thousand point one nine nine"},
            new object[] {10_000.1001m,"ten thousand point one nought nought one"},
            new object[] {10_001.1010m,"ten thousand one point one nought one"},
            new object[] {19_100.1101m,"nineteen thousand one hundred point one one nought one"},

            new object[] {101_001.9999555554444m,"one hundred one thousand two"},
            new object[] {9_888_100.09994444m,"nine million eight hundred eighty-eight thousand one hundred "
                                              + "point nought nine nine nine"},
        };

        public static object[] TestParameters_WriteNumberInWords_ResultTypeCurrency =
        {
            new object[] {1.89999999999m,  "fr-FR", "one,90 €"},
            new object[] {9.99999555555m,  "fr-FR", "ten,00 €"},
            new object[] {10m, "fr-FR", "ten,00 €"},
            new object[] {11.010008888m, "fr-FR", "eleven,01 €" },
            new object[] {19.109999999m, "fr-FR", "nineteen,11 €" },
            new object[] {20.99999998888m, "fr-FR", "twenty-one,00 €"},
            new object[] {99.999999994447m, "fr-FR", "one hundred,00 €"},
            new object[] {41.009999999m, "fr-FR", "fourty-one,01 €"},
            new object[] {999.999888888m,"fr-FR", "one thousand,00 €"},
            new object[] {9999.9995555555m,"fr-FR", "ten thousand,00 €"},

            new object[] {100_100.09678m,"fr-FR", "one hundred thousand one hundred,10 €"},
            new object[] {110_000.9997777m,"fr-FR", "one hundred ten thousand one,00 €"},
            new object[] {111_000.774444m,"fr-FR", "one hundred eleven thousand,77 €"},
        };
    }
}
