using Payroll.Data;
using static Payroll.Services.Utilities.Messages.ExceptionMessages;

namespace Payroll.Services.AuthenticServices
{
     public static class EntityConfirmation 
     {
          public static void ArgumentNullConfirmation<T>( T obj,string? paramName,string? methodName, string? className )
          {
               if ( obj == null )
               {
                    throw new ArgumentNullException(string.Format(NullModelViewExceptionString, paramName, methodName, className));
               }
          }

          public static void EntityNullConfirmation<T>( T obj,string? objName,string? methodName, string? className )
          {
               if (obj == null)
               {
                    throw new InvalidOperationException(string.Format(NullEntityExceptionString, objName, methodName, className));
               }
          }

          public static string GetClassName( object obj)
          {
               return obj.GetType().Name;
          }

          public static string? GetClassFullName( object obj)
          {
               return obj.GetType().FullName;
          }

          //public  static void EntityIdIsActual<TEntity>( PayrollContext db, int id )
          //{
          //     throw new NotImplementedException();
          //     //TODO :: Как с рефлекшън да бъде имплементиран класа!!!
          //     //TODO :: Как с рефлекшън да бъде имплементиран класа EntityConfirmation!!!
          //    //с цел да се покаже,че съществува или не запис в таблица с това 'Id' -> 
          //}
     }
}
