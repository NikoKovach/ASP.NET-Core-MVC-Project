namespace Payroll.ViewModels.CustomValidation
{
       [AttributeUsage( AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false )]
       public class OrderAttribute : Attribute
       {
              public OrderAttribute( int index )
              {
                     this.Index = index;
              }

              public int Index { get; set; }

              //public int GetOrderValue() => this.Index;
       }
}
