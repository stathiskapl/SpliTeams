﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    public class PlayerRankSaveDTO
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int SkillId { get; set; }
        public int UserId { get; set; }
        public int Rank { get; set; }
    }
}
