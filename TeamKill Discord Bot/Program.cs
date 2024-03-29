﻿using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using TeamKill_Discord_Bot.Services;

namespace TeamKill_Discord_Bot
{
    public class Program
    {
        // Setup the fields we'll assign later
        private readonly IConfiguration _config;
        private DiscordSocketClient _client;
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        
        public Program()
        {
            // Create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");

            // Build the configuration and assign to _config
            _config = _builder.Build();
        }

        public async Task MainAsync()
        {
            // Call ConfigureServices to create the ServiceCollection/Provider for passing around the services
            using(var services = ConfigureServices())
            {
                // Get the client and assign to client you get
                // the services via GetRequiredService<T>
                var client = services.GetRequiredService<DiscordSocketClient>();
                _client = client;

                // Setup logging and the ready event
                client.Log += LogAsync;
                client.Ready += ReadyAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                // This is where we get the Token from the configuration file, and start the bot
                await client.LoginAsync(TokenType.Bot, _config["Token"]);
                await client.StartAsync();

                // We get the CommandHandler class here and call the InitializeAsync method
                // to start things up for the CommandHandler service
                await services.GetRequiredService<CommandHandler>().InitializeAsync();

                await Task.Delay(-1);
            }
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
            return Task.CompletedTask;
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        // This method handles the ServiceCollection creation/configuration, and builds out the service provider we can call on later
        private ServiceProvider ConfigureServices()
        {
            // This returns a ServiceProvider that is used later to call for those services
            // we can add types we have access to here, hence adding the new using statement:
            // using TeamKill_Discord_Bot.Services;
            // The config we build is also added, which comes in handy for setting the command prefix!
            return new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }
    }
}
