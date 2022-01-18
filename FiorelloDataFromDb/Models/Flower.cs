using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Models
{
    public class Flower
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Description { get; set; }
        public int SKUCode { get; set; }
        [StringLength(maximumLength:30)]
        public string Dimension { get; set; }
        public bool InStock { get; set; }
        public int? CompaignId { get; set; }
        public Compaign Compaign { get; set; }
        public List<FlowerImage> FlowerImages { get; set; }
        public List<FlowerCategory> FlowerCategories { get; set; }
        public List<FlowerTag> FlowerTags { get; set; }
        [NotMapped]
        public List<int> CateogryIds { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public List<int> ImageIds { get; set; }
    }
}
