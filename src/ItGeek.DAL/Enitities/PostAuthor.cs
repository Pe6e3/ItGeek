﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class PostAuthor
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public Post? Post { get; set; } 
        public Author? Author { get; set; }
    }
}
