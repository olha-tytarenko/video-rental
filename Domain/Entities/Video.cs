using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Video
    {
        public int video_id { get; set; }
        public string video_name { get; set; }
        public int video_yaer { get; set; }
        public string video_description { get; set; }
        public decimal video_price_per_day { get; set; }
        public int video_quantity { get; set; }
        public string video_cover_url { get; set; }
        public string video_country { get; set; }
    }
}
