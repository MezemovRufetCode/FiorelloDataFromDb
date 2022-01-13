using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:200)]
        public string SlideIdForPlgn { get; set; }
        public string Image { get; set; }
        [StringLength(maximumLength:200)]
        public string Title { get; set; }
        [StringLength(maximumLength: 330)]
        public string Desc { get; set; }
        public string Signature { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public IFormFile SignatureFile { get; set; }
    }
}
