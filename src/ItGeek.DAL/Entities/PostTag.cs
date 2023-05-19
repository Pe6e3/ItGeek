﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGeek.DAL.Entities
{
    [Keyless]
    public class PostTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Post? Post { get; set; }
        public Tag? Tag { get; set; }
    }
}
