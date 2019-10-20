using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeamKill_Discord_Bot.TeamKillsData;

namespace TeamKill_Discord_Bot.Modules
{
    // For commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class TeamKillCommands : ModuleBase
    {
        TeamKillsDataContainer teamKills = TeamKillsDataContainer.Instance;

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
            if (args == null)
            {
                // If no arguments were passed (args are null), reply with the below text
                sb.AppendLine("Desculpa, não percebi isso.");
            }
            else
            {
                // Split the received args into multiple args
                string[] splitArgs = args.Split(' ');

                // Check if we have 3 arguments
                if (splitArgs.Length == 3)
                {
                    ulong userToTeamKill, userToGetTeamKilled;
                    string userToTeamKillRegex, userToGetTeamKilledRegex;

                    // Get the users id's from the mentions
                    userToTeamKillRegex = Regex.Match(splitArgs[0], @"\d+").Value;
                    userToGetTeamKilledRegex = Regex.Match(splitArgs[1], @"\d+").Value;

                    // Check if both user id's were passed
                    if (userToTeamKillRegex != null && userToGetTeamKilledRegex != null)
                    {
                        // Parse the id's to ulongs
                        userToTeamKill = ulong.Parse(userToTeamKillRegex);
                        userToGetTeamKilled = ulong.Parse(userToGetTeamKilledRegex);

                        // Check if the last argument is an integer and get it
                        int teamKillsNumber;
                        if (int.TryParse(splitArgs[2], out teamKillsNumber))
                        {
                            // If we have the arguments, let's reply!
                            sb.AppendLine($"Pediste para adicionar {teamKillsNumber} Team Kills de <@{userToTeamKill}> para <@{userToGetTeamKilled}>");

                            // Check if the passed users exist in the server
                            var userToTeamKillExists = Context.Guild.GetUserAsync(userToTeamKill);
                            var userToGetTeamKilledExists = Context.Guild.GetUserAsync(userToGetTeamKilled);

                            await Task.WhenAll(userToTeamKillExists, userToGetTeamKilledExists);

                            if (userToTeamKillExists.Result != null && userToGetTeamKilledExists.Result != null)
                            {
                                int newTeamKillCounter, currentKillCounter = 0;

                                // Get the current team kill counter
                                try
                                {
                                    currentKillCounter = teamKills.GetTeamKillsWithUsers(userToTeamKill, userToGetTeamKilled);

                                }
                                catch (KeyNotFoundException ex)
                                {
                                    Console.WriteLine($"The record for {splitArgs[0]} <-> {splitArgs[1]} was not found!");
                                }
                                finally
                                {
                                    newTeamKillCounter = currentKillCounter + teamKillsNumber;

                                    teamKills.SetTeamKills(new TeamKill(userToTeamKill, userToGetTeamKilled, newTeamKillCounter));

                                    sb.AppendLine($"Agora têm {newTeamKillCounter} Team Kills restantes!");
                                }
                            }
                            else
                            {
                                sb.Append("O utilizador a matar ou o que irá matar não existem neste servidor!");
                            }
                        }
                        else
                        {
                            sb.AppendLine("O numero de Team Kills deve ser um numero inteiro!");
                        }
                    }
                    else
                    {
                        sb.AppendLine("O utilizador a ser morto ou o a matar devem ser uma menção!");
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

        [Command("tksub")]
        public async Task SubtractTeamKillCommand([Remainder]string args = null)
        {
            // Initialize empty string builder for reply
            var sb = new StringBuilder();

            // Initialize embed builder to reply with an embed
            var embed = new EmbedBuilder();

            // Build out the reply
            sb.AppendLine($"{Context.User.Username},");
            sb.AppendLine();

            // Let's make sure the supplied arguments aren't null
            if (args == null)
            {
                // If no arguments were passed (args are null), reply with the below text
                sb.AppendLine("Desculpa, não percebi isso.");
            }
            else
            {
                // Split the received args into multiple args
                string[] splitArgs = args.Split(' ');

                // Check if we have 3 arguments
                if (splitArgs.Length == 3)
                {
                    ulong userToTeamKill, userToGetTeamKilled;
                    string userToTeamKillRegex, userToGetTeamKilledRegex;

                    // Get the users id's from the mentions
                    userToTeamKillRegex = Regex.Match(splitArgs[0], @"\d+").Value;
                    userToGetTeamKilledRegex = Regex.Match(splitArgs[1], @"\d+").Value;

                    // Check if both user id's were passed
                    if (userToTeamKillRegex != null && userToGetTeamKilledRegex != null)
                    {
                        // Parse the id's to ulongs
                        userToTeamKill = ulong.Parse(userToTeamKillRegex);
                        userToGetTeamKilled = ulong.Parse(userToGetTeamKilledRegex);

                        // Check if the last argument is an integer and get it
                        int teamKillsNumber;
                        if (int.TryParse(splitArgs[2], out teamKillsNumber))
                        {
                            // If we have the arguments, let's reply!
                            sb.AppendLine($"Pediste para subtrair {teamKillsNumber} Team Kills de <@{userToTeamKill}> para <@{userToGetTeamKilled}>");

                            // Check if the passed users exist in the server
                            var userToTeamKillExists = Context.Guild.GetUserAsync(userToTeamKill);
                            var userToGetTeamKilledExists = Context.Guild.GetUserAsync(userToGetTeamKilled);

                            await Task.WhenAll(userToTeamKillExists, userToGetTeamKilledExists);

                            if (userToTeamKillExists.Result != null && userToGetTeamKilledExists.Result != null)
                            {
                                int newTeamKillCounter, currentKillCounter = 0;

                                // Get the current team kill counter
                                try
                                {
                                    currentKillCounter = teamKills.GetTeamKillsWithUsers(userToTeamKill, userToGetTeamKilled);

                                }
                                catch (KeyNotFoundException ex)
                                {
                                    Console.WriteLine($"The record for {splitArgs[0]} <-> {splitArgs[1]} was not found!");
                                }
                                finally
                                {
                                    newTeamKillCounter = currentKillCounter - teamKillsNumber;

                                    teamKills.SetTeamKills(new TeamKill(userToTeamKill, userToGetTeamKilled, newTeamKillCounter));

                                    // Check if there's any team kills left
                                    if (newTeamKillCounter <= 0)
                                    {
                                        sb.AppendLine("Não têm mais Team Kills a dar!");
                                    }
                                    else
                                    {
                                        sb.AppendLine($"Agora têm {newTeamKillCounter} Team Kills restantes!");
                                    }

                                }
                            }
                            else
                            {
                                sb.Append("O utilizador a matar ou o que irá matar não existem neste servidor!");
                            }
                        }
                        else
                        {
                            sb.AppendLine("O numero de Team Kills deve ser um numero inteiro!");
                        }
                    }
                    else
                    {
                        sb.AppendLine("O utilizador a ser morto ou o a matar devem ser uma menção!");
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

        [Command("tklist")]
        public async Task ShowAllTeamKillsCommand()
        {
            // Initialize empty string builder for reply
            var sb = new StringBuilder();

            // Initialize embed builder to reply with an embed
            var embed = new EmbedBuilder();

            // Build out the reply
            sb.AppendLine($"{Context.User.Username}, a seguir listam todas as Team Kills a serem cobradas:");
            sb.AppendLine();

            var allTeamKills = teamKills.GetTeamKills();

            // Check if there's any team kills
            if(allTeamKills.Count > 0)
            {
                foreach(TeamKill teamKill in allTeamKills)
                {
                    sb.AppendLine($"[<@{teamKill.UserToTeamKill}>] -> [<@{teamKill.UserToGetTeamKilled}>] : {teamKill.TeamKills} Team Kill(s)");
                }
            }
            else
            {
                sb.AppendLine("Não existem Team Kills :(");
            }

            // Now we can assign the description of the embed to the contents of the StringBuilder we created
            embed.Description = sb.ToString();

            // This will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
    }
}
