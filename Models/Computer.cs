using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingUAJY.Models
{
    public class Computer
    {
        [Key]
        public string SKU { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Brand Brand { get; set; }
    }
    public class Brand
    {
        [Key]
        public string IdBrand { get; set; }
        public string Title { get; set; }
    }
}