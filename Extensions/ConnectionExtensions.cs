using Lotos.Abstractions.Database;

namespace FemboyDev.TextGenBot.Extensions;

internal static class ConnectionExtensions
{
	public static IBasket<T> GetBasketSync<T>(this IConnection connection) where T : IEntity<T>
	{
		return connection.GetBasket<T>().GetAwaiter().GetResult();
	}
}