using NUnit.Framework;
using Payroll.Data;
using Payroll.Data.Common;

namespace Payroll.Services.Test
{
     [TestFixture]
     public class EntityConfirmationServiceTest
     {
          [SetUp]
          public void Setup()
          { 
               //Arrange
               //Act
               //Assert
          }
               

          [Test]
          public void ArgumentNullConfirmationMethodShouldThrowArgumentNullException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = null;

               Assert.Throws<ArgumentNullException>( () =>
				EntityConfirmation.ArgumentNullConfirmation<PayrollContext>
				(db,paramName, methodName,className) );
          }

          [Test]
          public void ArgumentNullConfirmationMethodDoesNotThrowException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = new PayrollContext();

               Assert.DoesNotThrow(() => EntityConfirmation.ArgumentNullConfirmation
									(db,paramName, methodName,className));
          }

          [Test]
          public void EntityNullConfirmationMethodThrowInvalidOperationException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = null;

               Assert.Throws<InvalidOperationException>
			(
				() => EntityConfirmation.EntityNullConfirmation<PayrollContext>(db,
				paramName, methodName,className)
			);
          }

          [Test]
          public void EntityNullConfirmationMethodNotThrowException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = new PayrollContext();

               Assert.DoesNotThrow(() => EntityConfirmation.EntityNullConfirmation
									(db,paramName, methodName,className)
							);
          }
     }
}
