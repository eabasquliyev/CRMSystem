﻿using System;

namespace CrmSystem.Domain.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        public Employee Owner { get; set; }
        public Contact Contact { get; set; }
        public DateTime ContractDate { get; set; }
        public SaleType SaleType { get; set; }
        public RecordDetail CreatedBy { get; set; }
        public RecordDetail ModifiedBy { get; set; }
    }

    public enum SaleType
    {
        Cash = 1,
        Credit
    }
}