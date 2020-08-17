using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.ModelDtos
{
    public class MatchWithTeamAndPlayersDto
    {
        public int Id { get; set; }
        public Team TeamA { get; set; }
        public Team TeamB { get; set; }
        public int? ScoreTeamA { get; set; }
        public int? ScoreTeamB { get; set; }
        public List<TeamPlayer> TeamPlayerA { get; set; }
        public List<TeamPlayer> TeamPlayerB { get; set; }
        public string Description { get; set; }
    }
}
