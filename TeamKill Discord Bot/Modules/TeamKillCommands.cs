﻿using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamKill_Discord_Bot.Modules
{
    // For commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class TeamKillCommands : ModuleBase
    {
        [Command("tkadd")]
        public async Task AddTeamKillCommand([Remainder]string args = null)
        {
            // Initialize empty string builder for reply
            var sb = new StringBuilder();

            // Initialize embed builder to reply with an embed
            var embed = new EmbedBuilder();

            // Build out the reply
            sb.AppendLine($"{Context.User.Username},");
            sb.AppendLine();

            // Let's make sure the supplied arguments aren't null
            if(args == null)
            {
                // If no arguments were passed (args are null), reply with the below text
                sb.AppendLine("Desculpa, não percebi isso.");
            }
            else
            {
                // Split the received args into multiple args
                string[] splitArgs = args.Split(' ');

                // Check if we have 3 arguments
                if(splitArgs.Length == 3)
                {
                    // Check if the last argument is an integer and get it
                    int teamKillsNumber;
                    if(int.TryParse(splitArgs[2], out teamKillsNumber))
                    {
                        // If we have the arguments, let's reply!
                        sb.AppendLine($"Pediste para adicionar {teamKillsNumber} Team Kills de {splitArgs[0]} para {splitArgs[1]}");
                        sb.AppendLine("Mas ainda não é possivel :(");
                    }
                }
                else
                {
                    sb.AppendLine("Têm que ser passados 3 argumentos");
                }

            }

            // Now we can assign the description of the embed to the contents of the StringBuilder we created
            embed.Description = sb.ToString();

            // This will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
    }
}