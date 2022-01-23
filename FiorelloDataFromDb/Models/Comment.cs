using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:600)]
        public string Text { get; set; }
        public bool IsAccess { get; set; }
        public DateTime WriteTime { get; set; }
        [Required]
        public int FlowerId { get; set; }
        public Flower Flower { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
