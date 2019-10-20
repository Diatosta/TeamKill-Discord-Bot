using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamKill_Discord_Bot.Modules
{
    // For commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class ExamplesCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            // Initialize empty string builder for reply
            var sb = new StringBuilder();

            // Get user info from the Context
            var user = Context.User;

            // Build out the reply
            sb.AppendLine($"You are -> [{user.Username}]");
            sb.AppendLine("I must now say, World!");

            // Send simple string reply
            await ReplyAsync(sb.ToString());
        }
    }
}
