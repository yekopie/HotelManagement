﻿using Entities.Concrete;

namespace WebApi.Dtos.HotelDtos
{
    public class UpdateHotelDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int StarRating { get; set; }
    }
}

