using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:200)]
        public string Logo { get; set; }
        [Required]
        public string SearchIcon { get; set; }
        [Required]
        public string BasketIcon { get; set; }
        [StringLength(maximumLength:150)]
        public string InstagramUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 150)]
        [Required]
        public string Parallax { get; set; }
        [StringLength(maximumLength: 150)]
        [Required]
        public string ParallaxTitle { get; set; }

    }
}
