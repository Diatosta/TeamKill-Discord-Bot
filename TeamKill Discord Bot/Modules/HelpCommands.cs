using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamKill_Discord_Bot.Modules
{
    public class HelpCommands : ModuleBase
    {
        [Command("help")]
        public async Task ListAllCommands()
        {
            // Initialize empty string builder for reply
            var sb = new StringBuilder();

            // Initialize embed builder to reply with an embed
            var embed = new EmbedBuilder();

            sb.AppendLine("Abaixo seguem todos os comandos disponiveis:");
            sb.AppendLine();
            sb.AppendLine("Adicionar Team Kills - ;tkadd [Utilizador a matar] [Utilizador a ser morto] [Numero de Team Kills]");
            sb.AppendLine("Subtrair Team Kills - ;tksub [Utilizador a matar] [Utilizador a ser morto] [Numero de Team Kills]");
            sb.AppendLine("Listar Team Kills - ;tklist");
            sb.AppendLine("Este comando - ;help");

            // Now we can assign the description of the embed to the contents of the StringBuilder we created
            embed.Description = sb.ToString();

            // This will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
    }
}
