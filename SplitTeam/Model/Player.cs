﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    [Table("Players")]
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
