using NUnit.Framework;
using Payroll.Data;
using Payroll.Services.Services.CompanyServices;
using static Payroll.Services.AuthenticServices.EntityConfirmation;
using static Payroll.Services.Utilities.Messages.ExceptionMessages;

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

               Assert.Throws<ArgumentNullException>( () => ArgumentNullConfirmation<PayrollContext>(db,paramName, methodName,className) );
          }

          [Test]
          public void ArgumentNullConfirmationMethodDoesNotThrowException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = new PayrollContext();

               Assert.DoesNotThrow(() => ArgumentNullConfirmation<PayrollContext>(db,paramName, methodName,className));
          }

          [Test]
          public void EntityNullConfirmationMethodThrowInvalidOperationException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = null;

               Assert.Throws<InvalidOperationException>(() => EntityNullConfirmation<PayrollContext>(db,paramName, methodName,className));
          }

          [Test]
          public void EntityNullConfirmationMethodNotThrowException()
          {
               string paramName = "name";
               string? methodName = "Name of Method";
               string? className = " Name of Class";
               PayrollContext db = new PayrollContext();

               Assert.DoesNotThrow(() => EntityNullConfirmation<PayrollContext>(db,paramName, methodName,className));
          }
     }
}
