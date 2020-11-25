using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingUAJY.Models;

namespace TrainingUAJY.ViewModels
{
    public class Transaction
    {
        public string IdTransaction { get; set; }
        public string Computer { get; set; }
        public DateTimeOffset Created { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public int TotalTransaction { get; set; }
        public string Notes { get; set; }
    }
}