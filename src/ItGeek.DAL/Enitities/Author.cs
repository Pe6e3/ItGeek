﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class Author :BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string?  Slug { get; set; }
        public string? Regalia { get; set; }
        public string? Description { get; set; }
        public string? AuthorImage { get; set; }
        public string? Email { get; set; }

        public List<PostAuthor>? PostAuthors { get; set; }

    }
}
