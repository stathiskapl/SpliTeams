﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    [Table("TeamPlayers")]
    public class TeamPlayer : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
