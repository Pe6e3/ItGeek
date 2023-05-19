﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Entities
{
    [Keyless]
    public class PostCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Post? Post { get; set; }
        public Category? Category { get; set; }
    }
}
