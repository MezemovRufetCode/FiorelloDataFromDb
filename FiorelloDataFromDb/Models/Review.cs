using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ReviewContent { get; set; }
        public string  UserFullname { get; set; }
        public string Job { get; set; }
    }
}
