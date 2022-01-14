using FemboyDev.TextGenBot.Entities;
using FemboyDev.TextGenBot.Extensions;
using Lotos.Abstractions.Database;

namespace FemboyDev.TextGenBot.Services;

internal sealed class LearnService
{
	public LearnService(IConnection connection)
	{
		_wordsBasket = connection.GetBasketSync<WordEntity>();
	}
	
	private readonly IBasket<WordEntity> _wordsBasket;
	
	public async Task LearnText(List<List<string>> tokens)
	{
		foreach (var lineTokens in tokens) await LearnLine(lineTokens);
	}
	
	public async Task LearnLine(List<string> tokens)
	{
		for (var i = 0; i < tokens.Count; i++)
		{
			var token = tokens[i];
			var nextToken = tokens.Count - 1 != i ? tokens[i + 1] : null;

			var tokenEntity = await _wordsBasket.PickOrKeepByValue(token);

			if (nextToken is null) return;

			var nextTokenEntity = await _wordsBasket.PickOrKeepByValue(nextToken);

			if (tokenEntity.Dependencies.Contains(nextTokenEntity.Id)) continue;
			
			tokenEntity.Dependencies.Add(nextTokenEntity.Id);
			await tokenEntity.Sync();
		}
	}
}