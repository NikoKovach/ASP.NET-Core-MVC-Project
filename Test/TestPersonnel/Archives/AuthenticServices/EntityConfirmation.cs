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
     }
}
