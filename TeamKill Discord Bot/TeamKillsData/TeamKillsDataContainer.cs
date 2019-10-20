using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TeamKill_Discord_Bot.TeamKillsData
{
    public class TeamKillsDataContainer : ITeamKillsContainer
    {
        private List<TeamKill> _teamKills;

        private TeamKillsDataContainer()
        {
            Console.WriteLine("Initializing singleton object");

            // Read the Team Kills file and deserialize the values
            _teamKills = JsonConvert.DeserializeObject<List<TeamKill>>(File.ReadAllText(@"TeamKills.json"));

            // Check if the Json is null
            if(_teamKills == null)
            {
                // Create an empty team kills dictionary
                _teamKills = new List<TeamKill>();
            }
        }

        public int GetTeamKills()
        {
            return _teamKills.Count;
        }

        public int GetTeamKillsWithUsers(ulong UserToTeamKill, ulong UserToGetTeamKilled)
        {
            // Get the team kill in the list that matches if it exists
            var tk = _teamKills.FirstOrDefault(x => x.UserToTeamKill == UserToTeamKill && x.UserToGetTeamKilled == UserToGetTeamKilled);

            // Check if any team kill was found
            if (tk != null)
            {
                return tk.TeamKills;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void SetTeamKills(TeamKill teamKill)
        {
            // Get the team kill in the list that matches if it exists
            var tk = _teamKills.FirstOrDefault(x => x.UserToTeamKill == teamKill.UserToTeamKill && x.UserToGetTeamKilled == teamKill.UserToGetTeamKilled);

            // Check if any team kill was found
            if(tk != null)
            {
                // Check if the team kills value is 0 or below
                if (teamKill.TeamKills <= 0)
                {
                    // Delete the record from the list
                    _teamKills.Remove(tk);
                }
                else
                {
                    // Update the value
                    tk.TeamKills = teamKill.TeamKills;
                }
            }
            else
            {
                // Check if the team kills value is above 0
                if (teamKill.TeamKills > 0)
                {
                    // Add to the team kills list
                    _teamKills.Add(teamKill);
                }
            }

            // Write to file
            using (StreamWriter file = File.CreateText(@"TeamKills.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _teamKills);
            }
        }

        private static Lazy<TeamKillsDataContainer> instance = new Lazy<TeamKillsDataContainer>(() => new TeamKillsDataContainer());

        public static TeamKillsDataContainer Instance => instance.Value;
    }
}
