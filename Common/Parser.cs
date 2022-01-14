namespace FemboyDev.TextGenBot.Common;

internal sealed class Parser
{
	public Parser(Lexer lexer, TextFormatter formatter)
	{
		_lexer = lexer;
		_formatter = formatter;
	}
	
	private readonly Lexer _lexer;
	private readonly TextFormatter _formatter;

	public List<List<string>> Parse(string text)
	{
		return _lexer.Lex(_formatter.Format(text));
	}
}