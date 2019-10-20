using System;
using System.Collections.Generic;
using System.Text;

namespace TeamKill_Discord_Bot.TeamKillsData
{
    public interface ITeamKillsContainer
    {
        List<TeamKill> GetTeamKills();
        int GetTeamKillsWithUsers(ulong UserToTeamKill, ulong UserToGetTeamKilled);
        void SetTeamKills(TeamKill teamKill);
    }
}
