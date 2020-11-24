using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingUAJY.ViewModels
{
    public class Computer
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Processor { get; set; }
        public DateTimeOffset ProductionYear { get; set; }
        public int ScreenSize { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
    }
    public class Brand
    {
        public string IdBrand { get; set; }
        public string Title { get; set; }
    }
}