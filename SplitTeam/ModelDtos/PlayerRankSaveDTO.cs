using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    public class PlayerRankUpdateDTO
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int PlayerId { get; set; }
        public int Rank { get; set; }
    }
}
