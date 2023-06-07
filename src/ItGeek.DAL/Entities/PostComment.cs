using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Entities
{
    public class PostComment :BaseEntity
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
    }
}
