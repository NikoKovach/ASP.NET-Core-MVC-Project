
namespace Payroll.Data.Common
{
     public static class EntityConfirmation 
     {
          public static void ArgumentNullConfirmation<T>( T obj,string? paramName,string? methodName, string? className )
          {
               if ( obj == null )
               {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.NullModelViewExceptionString, paramName, methodName, className));
               }
          }

          public static void EntityNullConfirmation<T>( T obj,string? objName,string? methodName, string? className )
          {
               if (obj == null)
               {
                    throw new InvalidOperationException(string.Format(ExceptionMessages.NullEntityExceptionString, objName, methodName, className));
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
     }
}
