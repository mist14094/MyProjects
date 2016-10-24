using System;
using System.Collections.Generic;
using System.Data;
using System.Security.AccessControl;
using SjFactoryDataAccess;

namespace SJFactoryBusiness
{
    public class Product
    {
        public int Sno { get; set; }
        public string Upc { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Cost { get; set; }
        private DataAccess Access = new DataAccess();
        public List<Product> GetProductsList()
        {
            List<Product> products = new List<Product>();
            foreach (DataRow product in Access.GetProducts().Rows)
            {
                Product newproduct = new Product();
                try
                {
                    newproduct.Sno = int.Parse(product["Sno"].ToString());
                    newproduct.Upc = product["UPC"].ToString();
                    newproduct.StockCode = product["StockCode"].ToString();
                    newproduct.Description = product["Description"].ToString();
                    newproduct.IsActive = bool.Parse(product["IsActive"].ToString());
                    newproduct.CreatedDate = DateTime.Parse(product["CreatedDate"].ToString());
                    newproduct.Cost = float.Parse(product["Cost"].ToString()==""? product["Cost"].ToString() :"0");
                }
                catch (Exception ex)
                {}
                products.Add(newproduct);
            }
            return products;
        }
    }
}