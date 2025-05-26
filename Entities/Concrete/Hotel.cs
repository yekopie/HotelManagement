﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int StarRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }

}
