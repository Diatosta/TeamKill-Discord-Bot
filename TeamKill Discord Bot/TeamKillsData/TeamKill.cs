using System;
using System.Collections.Generic;
using System.Text;

namespace TeamKill_Discord_Bot.TeamKillsData
{
    public class TeamKill
    {
        public ulong UserToTeamKill { get; set; }
        public ulong UserToGetTeamKilled { get; set; }
        public int TeamKills { get; set; }

        public TeamKill(ulong userToTeamKill, ulong userToGetTeamKilled, int teamKills)
        {
            UserToTeamKill = userToTeamKill;
            UserToGetTeamKilled = userToGetTeamKilled;
            TeamKills = teamKills;
        }
    }
}
