using FemboyDev.TextGenBot.Entities;
using FemboyDev.TextGenBot.Extensions;
using Lotos.Abstractions.Database;

namespace FemboyDev.TextGenBot.Services;

internal sealed class GenerateService
{
	public GenerateService(IConnection connection)
	{
		_wordsBasket = connection.GetBasketSync<WordEntity>();
		_random = new Random();
	}

	private readonly Random _random;
	private readonly IBasket<WordEntity> _wordsBasket;

	public async Task<string> Generate(string word, int count = 10, bool strict = true)
	{
		var wordEntity = await _wordsBasket.PickOrKeepByValue(word);

		var words =  strict 
			? await GenerateWordsLineWithStrict(wordEntity, count) 
			: await GenerateWordsLine(wordEntity, count);

		return string.Join(" ", words.Select(e => e.Value));
	}
	
	public async Task<List<WordEntity>> GenerateWordsLineWithStrict(WordEntity seedWord, int count)
	{
		var wordsList = new List<WordEntity>();

		for (var i = 0; i < count; i++)
		{
			var words = (await GenerateWordsLine(seedWord, count))
				.Take(count - i).ToList();
			
			i += words.Count;
			wordsList.AddRange(words);
		}

		return wordsList;
	}

	public async Task<List<WordEntity>> GenerateWordsLine(WordEntity seedWord, int maxCount)
	{
		var wordsList = new List<WordEntity> { seedWord };
		var lastWord = seedWord;
		
		for (var i = 0; i < maxCount; i++)
		{
			if (lastWord.Dependencies.Count == 0) break;

			var randIndex = _random.Next(lastWord.Dependencies.Count - 1);
			var randId = lastWord.Dependencies[randIndex];

			lastWord = (await _wordsBasket.Pick(randId))!;
			wordsList.Add(lastWord);
		}

		return wordsList;
	}
}