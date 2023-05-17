using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class Tag : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Slug { get; set; }
        public string? TagImage { get; set; }

        public List<Post>? Posts { get; } = new();


    }
}
