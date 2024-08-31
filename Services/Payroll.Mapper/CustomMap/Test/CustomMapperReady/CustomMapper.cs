namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class CustomMapper : ICustomMapper
       {
              public TResult? Map<TSource, TResult>( Func<TSource, TResult?> expression, TSource source )
              {
                     TResult? result = expression( source );

                     return result;
              }
       }
}
