using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public class Transaction
    {
        
        public Transaction(long transactionId, string transactionType, string storeCode, string itemDescription, string category, string department, string supplier, string supplierCode, decimal cost, decimal price, decimal quantity, decimal modifiers, decimal subtotal, decimal tax, decimal discount, decimal total, string cashier, DateTime time, string register)
        {
            TransactionId = transactionId;
            TransactionType = transactionType;
            StoreCode = storeCode;
            ItemDescription = itemDescription;
            Category = category;
            Department = department;
            Supplier = supplier;
            SupplierCode = supplierCode;
            Cost = cost;
            Price = price;
            Quantity = quantity;
            Modifiers = modifiers;
            Subtotal = subtotal;
            Tax = tax;
            Discount = discount;
            Total = total;
            Cashier = cashier;
            Time = time;
            Register = register;
        }

        public Transaction()
        {
        }

        public long TransactionId { get; set; }
        public string TransactionType { get;set; }
        public string StoreCode { get;set; }
        public string ItemDescription { get;set; }
        public string Category { get;set; }
        public string Department { get;set; }
        public string Supplier { get;set; }
        public string SupplierCode { get;set; }
        public decimal Cost { get;set; }
        public decimal Price { get;set; }
        public decimal Quantity { get;set; }
        public decimal Modifiers { get;set; }
        public decimal Subtotal { get;set; }
        public decimal Tax { get;set; }
        public decimal Discount { get;set; }
        public decimal Total { get;set; }
        public string Cashier { get;set; }
        public DateTime Time { get;set; }
        public string Register { get;set; }
       
    }
}
