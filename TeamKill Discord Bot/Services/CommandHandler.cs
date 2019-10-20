using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace TeamKill_Discord_Bot.Services
{
    public class CommandHandler
    {
        // Setup fields to be set later in the constructor
        private readonly IConfiguration _config;
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services)
        {
            // Since we passed the services in, we can use GetRequiredService to pass them
            // into the fields set earlier
            _config = services.GetRequiredService<IConfiguration>();
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            // Take action when we execute a command
            _commands.CommandExecuted += CommandExecutedAsync;

            // Take action when we receive a message (so we can process it, and see if it's a valid command)
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            // Register modules that are public and inherit ModuleBase<T>
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        // This class is where the magic starts, and takes actions upon receiving messages
        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // Ensure we don't process system/ other bot messages
            if(!(rawMessage is SocketUserMessage message))
            {
                return;
            }

            if(message.Source != Discord.MessageSource.User)
            {
                return;
            }

            // Sets the argument position away from the prefix we set
            var argPos = 0;

            // Get prefix from the configuration file
            char prefix = Char.Parse(_config["Prefix"]);

            // Determine if the message has a valid prefix, and adjust argPos based on prefix
            if(!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);

            // Execute command if one is found that matches
            await _commands.ExecuteAsync(context, argPos, _services);
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // If a command isn't found, log that info to console and exit this method
            if(!command.IsSpecified)
            {
                System.Console.WriteLine($"Command failed to execute for [{context.User.Username}] <-> [{result.ErrorReason}]!");
                return;
            }

            // Log success to the console and exit this method
            if(result.IsSuccess)
            {
                System.Console.WriteLine($"Command [{command.Value.Name}] executed for -> [{context.User.Username}]");
                return;
            }

            // Failure scenario, let's let the user know
            await context.Channel.SendMessageAsync($"Sorry, {context.User.Username}... something went wrong -> [{result}]!");
        }
    }
}
