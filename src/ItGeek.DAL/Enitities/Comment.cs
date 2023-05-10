﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Enitities
{
    public class Comment :BaseEntity
    {
        public string? Name { get; set; } //Иия комментатора
        public string? Email{ get; set; }
        public string? Url{ get; set; }
        public string? Message{ get; set; }
        public DateTime CreatedAt { get; set; }

        public List<PostComment>? PostComments { get; set; }

    }
}
