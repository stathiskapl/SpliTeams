using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Model
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
