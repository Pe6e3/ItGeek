using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class Post :BaseEntity
    {
        public int Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User? CreatedBy { get; set; }
        public User? EditedBy { get; set; }

        public List<PostAuthor>? PostAuthors { get; set; }
        public List<PostCategory>? PostCategories { get; set; }
        public List<PostTag>? PostTags { get; set; }
        public List<PostComment>? PostComments { get; set; }


    }
}
