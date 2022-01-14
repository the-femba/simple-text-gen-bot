using FemboyDev.TextGenBot.Common;
using FemboyDev.TextGenBot.Entities;
using FemboyDev.TextGenBot.Services;
using Lotos.Abstractions.Database;
using MongoDB.Bson;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace FemboyDev.TextGenBot.Controllers;

internal sealed class MessagingController : Controller
{
	public MessagingController(Parser parser, LearnService learnService, GenerateService generateService)
	{
		_parser = parser;
		_learnService = learnService;
		_generateService = generateService;
	}
	
	private const string GenerateCommandName = "generate";
	private const string LearnCommandName = "learn";
	
	private readonly Parser _parser;
	
	private readonly LearnService _learnService;
	private readonly GenerateService _generateService;

	[Command(GenerateCommandName)]
	public async Task<OutMessage> GenerateCommand()
	{
		try
		{
			if (Message.Text is var text && text is null) return "Error";
			text = RemoveCommandPrefix(text, GenerateCommandName);

			var tokens = _parser.Parse(text);
		
			return await _generateService.Generate(tokens[0][0], strict: false);
		}
		catch (Exception)
		{
			return "Error";
		}
	}
	
	[Command(LearnCommandName)]
	public async Task<OutMessage> LearnCommand()
	{
		try
		{
			if (Message.Text is var text && text is null) return "Error";
			text = RemoveCommandPrefix(text, LearnCommandName);
		
			await _learnService.LearnText(_parser.Parse(text));
		
			return "Done";
		}
		catch (Exception)
		{
			return "Error";
		}
	}

	private string RemoveCommandPrefix(string text, string prefix)
	{
		return  text.Remove(0, prefix.Length + 1);
	}
}