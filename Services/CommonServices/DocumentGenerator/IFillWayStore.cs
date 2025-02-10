namespace LegalFramework.Services.DocumentGenerator
{
	public interface IFillWayStore
	{
		IDictionary<string, IFill> FillWayStore { get; set; }
	}
}
