using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace TeamKill_Discord_Bot
{
    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            var token = "NjM1MTY0NTY3MTIyNjA4MTUw.XatQGg.2q6BhvccGN29NzwWI8l-3j-cUUA";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
