using System.Text;

namespace FemboyDev.TextGenBot.Common;

internal sealed class Lexer
{
	public List<List<string>> Lex(string text)
	{
		var lines = text.Split("\n");
		var outLexTable = new List<List<string>>();
		
		foreach (var line in lines)
		{
			var tokens = line.Split(" ").Where(e => e != string.Empty).ToList();
			
			outLexTable.Add(tokens);
		}

		return outLexTable;
	}
}