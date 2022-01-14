using FemboyDev.TextGenBot.Common;
using FemboyDev.TextGenBot.Services;
using Lotos.DependencyInjection.Extensions;
using Lotos.Mongo.Database;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Adapters.Telegram;
using Replikit.Core.Modules;

namespace FemboyDev.TextGenBot;

internal class Module : ReplikitModule
{
	private const string EnvMongoConnectionStringKeyName = "MONGO_CONNECTION_STRING";
	private const string EnvMongoDatabaseKeyName = "MONGO_DATABASE_NAME";
	private const string EnvTelegramBotTokenKeyName = "TG_BOT_TOKEN";
	
	protected override void ConfigureAdapters(IAdapterLoaderOptions options)
	{
		options.AddTelegram(Environment.GetEnvironmentVariable(EnvTelegramBotTokenKeyName)!);
	}

	protected override void ConfigureServices(IServiceCollection services)
	{
		var driver = new MongoDriver(
			Environment.GetEnvironmentVariable(EnvMongoConnectionStringKeyName)!,
			Environment.GetEnvironmentVariable(EnvMongoDatabaseKeyName)!);
		
		services.AddLotos().SingleDatabase(driver);

		services.AddSingleton<Lexer>();
		services.AddSingleton<TextFormatter>();
		services.AddSingleton<Parser>();
		services.AddSingleton<LearnService>();
		services.AddSingleton<GenerateService>();
	}
}