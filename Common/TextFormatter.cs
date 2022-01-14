using System.Text;
using System.Text.RegularExpressions;

namespace FemboyDev.TextGenBot.Common;

internal sealed class TextFormatter
{
	public string Format(string text)
	{
		return new Regex(@"[^а-яё \n]").Replace(text.ToLower(), "");
	}
}