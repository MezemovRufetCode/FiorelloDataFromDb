using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Models
{
    public class Compaign
    {
        public int Id { get; set; }
        public int DiscountPercent { get; set; }
        public List<Flower> Flowers { get; set; }
    }
}
