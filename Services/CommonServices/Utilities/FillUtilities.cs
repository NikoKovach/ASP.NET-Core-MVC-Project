using System.Text;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Visitors;

namespace LegalFramework.Services.Utilities
{
	public class FillUtilities
	{
		private readonly static Dictionary<int, string> intNumbersInWords = SetDictionary();


		public const string TextFromDots = "............";

		public static void AddTextToStringBuilder(Paragraph paragraph, StringBuilder sb)
		{
			sb.Clear();

			sb.Append(paragraph.GetText());
		}

		public static void AddTextToStringBuilder(DocumentObject paragraphElement, StringBuilder sb)
		{
			sb.Clear();

			string content = GetElementContent(paragraphElement);

			sb.Append(content);
		}

		public static string NewValue(string? value)
		{
			return !string.IsNullOrWhiteSpace(value) ? value : TextFromDots;
		}

		public static string NewValue(DateTime? value, string dateFormat)
		{
			string dateResult = string.Empty;

			if (value == null)
			{
				dateResult = TextFromDots;

				return dateResult;
			}

			dateResult = value.Value.Date.ToString(dateFormat);

			return dateResult;
		}

		public static string NewValue(int? value)
		{
			if (value == null || value < 1)
				return TextFromDots;

			return value.ToString();
		}

		public static string NewValue(decimal? value)
		{
			if (value == null || value < 0.000000001m)
				return TextFromDots;

			return value.ToString();
		}

		public static string? NewValue(double? value)
		{
			if (value == null || value < 0.000000001)
				return TextFromDots;

			return value.ToString();
		}

		public static void FillParagraph(Paragraph paragraph, StringBuilder sb)
		{
			DocumentObject? firstEl = paragraph.Elements.First;

			firstEl.SetValue("Content", sb.ToString());
		}

		public static void FillParagraphElement(DocumentObject paragraphElement, StringBuilder sb)
		{
			paragraphElement.SetValue("Content", sb.ToString());
		}

		public static string GetElementContent(DocumentObject paragraphElement)
		{
			string? text = paragraphElement
						   .GetValue("Content")
						   .ToString();

			return text;
		}

		internal static string TheNumberInWords(int? number)
		{
			int intNumber = Convert.ToInt32(number);

			if (!intNumbersInWords.ContainsKey(intNumber))
				return TextFromDots;

			return intNumbersInWords[intNumber];
		}

		internal static string TheNumberInWords(decimal? number)
		{
			string numberInWords = "четири хиляди";

			if (number == null || number < 1)
				return TextFromDots;

			//TODO : дробно число(число с плаваща запетая) с думи

			return numberInWords;
		}

		internal static void SetBoldText(Paragraph paragraph, int index, string oldValue, string newValue)
		{
			var element = paragraph.Elements[index];
			string? elementText = element.GetValue("Content").ToString();

			int oldValueLenght = oldValue.Length;
			int oldValueIndex = elementText.IndexOf(oldValue);

			string baseTextOne = elementText.Substring(0, oldValueIndex);
			string baseTextTwo = elementText.Substring(oldValueIndex + oldValueLenght);

			paragraph.Elements[index].SetNull();

			paragraph.AddText(baseTextOne);
			paragraph.AddFormattedText(newValue, TextFormat.Bold);
			paragraph.AddText(baseTextTwo);
		}

		//###################################################################################

		private static Dictionary<int, string> SetDictionary()
		{
			Dictionary<int, string> numbersDic = new Dictionary<int, string>
			{
				{0,"нула"},
				{1,"един"},
				{2,"два"},
				{3,"три"},
				{4,"четири"},
				{5,"пет"},
				{6,"шест"},
				{7,"седем"},
				{8,"осем"},
				{9,"девет"},
				{10,"десет"},
				{11,"единадесет"},
				{12,"дванадесет"},
				{13,"тринадесет"},
				{14,"четиринадесет"},
				{15,"петнадесет"},
				{16,"шестнадесет"},
				{17,"седемнадесет"},
			};

			return numbersDic;
		}

	}
}

//public static string NewValue(int? value)
//{
//	string addressNumber = string.Empty;

//	if (value == null || value < 1)
//	{
//		addressNumber = TextFromDots;

//		return addressNumber;
//	}

//	return value.ToString();
//}