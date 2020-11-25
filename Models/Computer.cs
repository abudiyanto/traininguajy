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
        public Processor Processor { get; set; }
        public DateTimeOffset ProductionYear { get; set; }
        public int ScreenSize { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
    }
    public class Transaction
    {
        [Key]
        public string IdTransaction { get; set; }
        public Computer Computer { get; set; }
        public DateTimeOffset Created { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public int TotalTransaction { get; set; }
        public string Notes { get; set; }
    }
    public enum TransactionType
    {
        Debet,
        Credit
    }
    public class Brand
    {
        [Key]
        public string IdBrand { get; set; }
        public string Title { get; set; }
    }
    public class Processor
    {
        [Key]
        public string IdProcessor { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Year { get; set; }
    }
}