using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.ModelDtos
{
    public class TeamPlayerDto
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public decimal Rank { get; set; }
    }
}
