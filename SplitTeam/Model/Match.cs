using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    [Table("Matches")]
    public class Match : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("TeamAId")]
        public virtual Team TeamA { get; set; }
        [ForeignKey("TeamBId")]
        public virtual Team TeamB { get; set; }
        public int? ScoreTeamA { get; set; }
        public int? ScoreTeamB { get; set; }
        public string Description { get; set; }
    }
}
