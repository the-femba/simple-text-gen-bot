using dotenv.net;
using FemboyDev.TextGenBot;
using Replikit.Core.Hosting;

DotEnv.Load();
ReplikitHost.RunModule<Module>();
