using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public string PictureURL { get; set; }
        public string Country { get; set; }

    }
}