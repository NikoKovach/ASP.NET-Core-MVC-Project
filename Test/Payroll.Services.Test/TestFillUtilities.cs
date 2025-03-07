using System.Text;

using LegalFramework.Services.Utilities;

using MigraDoc.DocumentObjectModel;

using NUnit.Framework;

namespace Payroll.Services.Test
{
    [TestFixture]
    public class TestFillUtilities
    {
        private Document? document;
        private string? testString;
        private Paragraph? paragraph;

        [SetUp]
        public void SetUp( )
        {
            this.document = new Document( );
            Section section = document.AddSection( );
            this.paragraph = section.AddParagraph( );

            this.testString = "TestValue";
        }

        [TearDown]
        public void TearDown( )
        {
            this.document = default;

            this.testString = default;

            this.paragraph = default;
        }

        [Test]
        public void Method_AddTextToStringBuilder_ShouldClearTheStringBuilder( )
        {
            Paragraph? paragraph = this.document.LastSection.LastParagraph;

            StringBuilder sb = new StringBuilder( this.testString );

            string text = sb.ToString( );

            FillUtilities.AddTextToStringBuilder( paragraph, sb );

            int sbLength = sb.Length;

            Assert.Multiple( ( ) =>
            {
                Assert.That( text, Is.EqualTo( "TestValue" ) );

                Assert.That( sbLength, Is.EqualTo( 0 ) );
            } );
        }

        [Test]
        public void Method_AddTextToStringBuilder_ApendToStringBuilder( )
        {
            Paragraph? paragraph = this.document.LastSection.LastParagraph;

            paragraph.AddText( this.testString );

            StringBuilder sb = new StringBuilder( );

            FillUtilities.AddTextToStringBuilder( paragraph, sb );

            Assert.That( sb.ToString( ), Is.EqualTo( "TestValue" ) );
        }

        [Test]
        public void Method_AddTextToStringBuilder_AcceptDocumentObjectAndApendToStringBuilder( )
        {
            this.document.LastSection.LastParagraph.AddText( this.testString );

            DocumentObject? paragraphElement = this.document.LastSection.LastParagraph.Elements.First;

            StringBuilder sb = new StringBuilder( );

            FillUtilities.AddTextToStringBuilder( paragraphElement, sb );

            Assert.That( sb.ToString( ), Is.EqualTo( this.testString ) );
        }

        [Test]
        public void Method_NewValue_AcceptValidStringAndReturnSameString( )
        {
            string result = FillUtilities.NewValue( this.testString );

            Assert.That( result, Is.EqualTo( this.testString ) );
        }

        [Test]
        public void Method_NewValue_AcceptNullOrEmptyStringAndReturnDotsString( )
        {
            string? nullString = default;

            string emptyString = string.Empty;

            string dotsResultNullString = FillUtilities.NewValue( nullString );

            string dotsResultEmptyString = FillUtilities.NewValue( emptyString );

            Assert.Multiple( ( ) =>
            {
                Assert.That( dotsResultNullString, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultEmptyString, Is.EqualTo( FillUtilities.TextFromDots ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptIntValueGreaterThanOneAndReturnSameValueAsString( )
        {
            int intValue = 123456;
            int intOne = 1;

            string result = FillUtilities.NewValue( intValue );

            string resultIntOne = FillUtilities.NewValue( intOne );

            Assert.Multiple( ( ) =>
            {
                Assert.That( result, Is.EqualTo( intValue.ToString( ) ) );

                Assert.That( resultIntOne, Is.EqualTo( intOne.ToString( ) ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptNullOrIntValueLessThanOneAndReturnDotsString( )
        {
            int? nullIntValue = null;

            int zeroValue = 0;

            int negativeIntValue = -1;

            string dotsResultNullInt = FillUtilities.NewValue( nullIntValue );

            string dotsResultZeroInt = FillUtilities.NewValue( zeroValue );

            string dotsResultNegativeInt = FillUtilities.NewValue( negativeIntValue );

            Assert.Multiple( ( ) =>
            {
                Assert.That( dotsResultNullInt, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultZeroInt, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultNegativeInt, Is.EqualTo( FillUtilities.TextFromDots ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptPositiveDecimalValueAndReturnSameValueAsString( )
        {
            decimal valueLessThanOne = 0.4567m;

            decimal valueGreaterThanOne = 1.4567m;

            string resultLessThanOne = FillUtilities.NewValue( valueLessThanOne );

            string resultGreaterThanOne = FillUtilities.NewValue( valueGreaterThanOne );

            Assert.Multiple( ( ) =>
            {
                Assert.That( resultLessThanOne, Is.EqualTo( valueLessThanOne.ToString( ) ) );

                Assert.That( resultGreaterThanOne, Is.EqualTo( valueGreaterThanOne.ToString( ) ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptNullOrDecimalValueLessThanZeroAndReturnDotsString( )
        {
            decimal? decimalNullValue = null;

            decimal zeroValue = 0.0000000009m;

            decimal negativeDecimalValue = -0.00000456m;

            string dotsResultNullDecimal = FillUtilities.NewValue( decimalNullValue );

            string dotsResultZeroDecimal = FillUtilities.NewValue( zeroValue );

            string dotsResultNegativeDecimal = FillUtilities.NewValue( negativeDecimalValue );

            Assert.Multiple( ( ) =>
            {
                Assert.That( dotsResultNullDecimal, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultZeroDecimal, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultNegativeDecimal, Is.EqualTo( FillUtilities.TextFromDots ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptPositiveDoubleValueAndReturnSameValueAsString( )
        {
            double valueLessThanOne = 0.000000001;

            double valueGreaterThanOne = 1.4567;

            string resultLessThanOne = FillUtilities.NewValue( valueLessThanOne );

            string resultGreaterThanOne = FillUtilities.NewValue( valueGreaterThanOne );

            Assert.Multiple( ( ) =>
            {
                Assert.That( resultLessThanOne, Is.EqualTo( valueLessThanOne.ToString( ) ) );

                Assert.That( resultGreaterThanOne, Is.EqualTo( valueGreaterThanOne.ToString( ) ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptNullOrDoubleValueLessThanZeroAndReturnDotsString( )
        {
            double? doubleNullValue = null;

            double zeroDoubleValue = 0.0000000009;

            double negativeDoubleValue = -0.00000456;

            string dotsResultNullDouble = FillUtilities.NewValue( doubleNullValue );

            string dotsResultZeroDouble = FillUtilities.NewValue( zeroDoubleValue );

            string dotsResultNegativeDouble = FillUtilities.NewValue( negativeDoubleValue );

            Assert.Multiple( ( ) =>
            {
                Assert.That( dotsResultNullDouble, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultZeroDouble, Is.EqualTo( FillUtilities.TextFromDots ) );

                Assert.That( dotsResultNegativeDouble, Is.EqualTo( FillUtilities.TextFromDots ) );
            } );
        }

        [Test]
        public void Method_NewValue_AcceptNullableDateAndReturnDotsString( )
        {
            DateTime? nullableDate = null;

            string dateString = FillUtilities.NewValue( nullableDate, "dd.MM.yyyy" );

            Assert.That( dateString, Is.EqualTo( FillUtilities.TextFromDots ) );
        }

        [TestCaseSource( nameof( TestParameters_NewValueMethod_DateTimeValue ) )]
        public void Method_NewValue_AcceptValidDateAndReturnDateAsString( DateTime date,
                                                                          string dateFormat,
                                                                          string cultureName,
                                                                          string resultString )
        {
            string dateString = FillUtilities.NewValue( date, dateFormat, cultureName );

            Assert.That( dateString, Is.EqualTo( resultString ) );
        }
        [Test]
        public void Method_FillParagraph_ShouldChangeTheValueOfTheFirstElementOfTheParagraph( )
        {
            StringBuilder sb = new StringBuilder( this.testString );

            this.paragraph.AddText( "First Element" );

            this.paragraph.AddText( "Second Element" );

            FillUtilities.FillParagraph( this.paragraph, sb );

            string? valueOfTheFirstElement = this.paragraph.Elements.First
                                                        .GetValue( "Content" )
                                                        .ToString( );

            Assert.That( valueOfTheFirstElement, Is.EqualTo( this.testString ) );
        }

        [Test]
        public void Method_FillParagraph_ShouldNotChangeTheValueOfTheElementsOtherThanTheFirstOne( )
        {
            StringBuilder sb = new StringBuilder( this.testString );

            this.paragraph.AddText( "First Element" );

            this.paragraph.AddText( "Second Element" );

            FillUtilities.FillParagraph( this.paragraph, sb );

            string? valueOfTheSecondElement = this.paragraph.Elements.LastObject
                                                                     .GetValue( "Content" )
                                                                     .ToString( );

            Assert.That( valueOfTheSecondElement, Is.EqualTo( "Second Element" ) );

            Assert.That( valueOfTheSecondElement, Is.Not.EqualTo( this.testString ) );
        }

        [Test]
        public void Method_FillParagraph_ThrowNullReferenceExceptionWhenParagraphIsEmpty( )
        {
            Assert.Throws<NullReferenceException>( TestBodyForFillParagraphMethod );
        }

        //Arrange - Act - Assert
        //############################################################################################

        private static readonly object[] TestParameters_NewValueMethod_DateTimeValue =
        {
            new object[] {new DateTime(2025,03,01),"dd.MM.yyyy","bg-BG","01.03.2025"},
            new object[] {new DateTime(1999,03,01),"dd.MM.yyyy", "bg-BG", "01.03.1999" },
            new object[] {new DateTime(1900,03,01),"dd/MM/yyyy","es-ES","01/03/1900"},
        };

        private void TestBodyForFillParagraphMethod( )
        {
            StringBuilder sb = new StringBuilder( this.testString );

            FillUtilities.FillParagraph( this.paragraph, sb );
        }
    }
}

/*
 
//this.paragraph.AddText( "First Element" );

            //this.paragraph.AddText( "Second Element" );

            //FillUtilities.FillParagraph( this.paragraph, sb );

            //string? valueOfTheSecondElement = this.paragraph.Elements.LastObject
            //                                                         .GetValue( "Content" )
            //                                                         .ToString( );
            //Assert.That( valueOfTheSecondElement, Is.Not.EqualTo( this.testString ) );

            //Assert.That( FillUtilities.FillParagraph, Throws.TypeOf<NullReferenceException>( )
            //                                                .Arguments.);
            //Assert.Throws<NullReferenceException>( FillUtilities.FillParagraph );

 System.ArgumentException
  HResult=0x80070057
  Message=The actual value must be a parameterless delegate but was Action`2. (Parameter 'actual')
  Source=nunit.framework
  StackTrace:
   at NUnit.Framework.Guard.<ArgumentValid>g__ThrowArgumentException|3_0(String message, String paramName)
   at NUnit.Framework.Guard.ArgumentValid(Boolean condition, String message, String paramName)
   at NUnit.Framework.Internal.ExceptionHelper.RecordException(Delegate parameterlessDelegate, String parameterName)
   at NUnit.Framework.Constraints.ThrowsConstraint.ApplyTo[TActual](TActual actual)
   at NUnit.Framework.Assert.That[TActual](TActual actual, IResolveConstraint expression, NUnitString message, String actualExpression, String constraintExpression)
   at Payroll.Services.Test.TestFillUtilities.Method_FillParagraph_ThrowNullReferenceExceptionWhenParagraphIsEmpty() in D:\SoftwareCourses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Test\Payroll.Services.Test\TestFillUtilities.cs:line 318
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
 */

