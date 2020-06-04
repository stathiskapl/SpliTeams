using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    public class PlayerRankCreateDTO
    {
        public int SkillId { get; set; }
        public int PlayerId { get; set; }
        public int Rank { get; set; }
    }
}
