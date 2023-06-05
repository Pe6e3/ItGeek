using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Entities
{
    public class User :BaseEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
