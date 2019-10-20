using System;
using System.Collections.Generic;
using System.Text;

namespace TeamKill_Discord_Bot.TeamKillsData
{
    public class TeamKill
    {
        public string UserToTeamKill { get; set; }
        public string UserToGetTeamKilled { get; set; }
        public int TeamKills { get; set; }

        public TeamKill(string userToTeamKill, string userToGetTeamKilled, int teamKills)
        {
            UserToTeamKill = userToTeamKill;
            UserToGetTeamKilled = userToGetTeamKilled;
            TeamKills = teamKills;
        }
    }
}
