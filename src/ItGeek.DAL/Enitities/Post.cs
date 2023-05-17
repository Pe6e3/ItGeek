using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class Post :BaseEntity
    {
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User? CreatedBy { get; set; }
        public User? EditedBy { get; set; }
        public bool isDeleted { get; set; } = false;

        public List<Author>? Authors { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
