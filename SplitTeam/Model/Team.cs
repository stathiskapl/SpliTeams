﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    [Table("Teams")]
    public class Team : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
