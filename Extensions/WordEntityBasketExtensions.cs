using FemboyDev.TextGenBot.Entities;
using Lotos.Abstractions.Database;

namespace FemboyDev.TextGenBot.Extensions;

internal static class WordEntityBasketExtensions
{
	public static async Task<WordEntity> PickOrKeepByValue(this IBasket<WordEntity> basket, string token)
	{
		var entity = await basket.Pick(e => e.Value == token)
		             ?? await basket.Keep(new (token));

		return entity;
	}
}