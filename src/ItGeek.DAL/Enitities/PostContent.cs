using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class PostContent 
    {
        public int PostId { get; set; }
        public Post? Post { get; set; }
        public string? Title { get; set; }
        public string? PostBody { get; set; }
        public string? PostImage { get; set; }
        public int CommentsNum { get; set; }
        public bool CommentsClosed { get; set; }
    }
}
