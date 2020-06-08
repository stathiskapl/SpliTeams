using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    [Table("PlayerRanks")]
    public class PlayerRank : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int Rank { get; set; }
    }
}
