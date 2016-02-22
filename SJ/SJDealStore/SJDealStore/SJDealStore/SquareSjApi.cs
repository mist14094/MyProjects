using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RestSharp;

namespace SJDealStore
{
    public class SquareSjApi
    {
       DALayer daLayer = new DALayer();
        private const string Token = "HL_JtBus1af_BUvEugmY7Q"; 
       // private const string Token = "oI9GFjBvoOHhjBcetZQu-w";
        public List<Categories> GetAllCategories()
        {
            var client = new RestSharp.RestClient("https://connect.squareup.com/v1/me");
            var post = new RestRequest("/categories", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            post.AddHeader("Authorization", String.Format("Bearer {0}", Token));
            var response = client.Execute(post);
            Categories sCategories = new Categories();
            List<Categories> categories =
                new JavaScriptSerializer().Deserialize<List<Categories>>(response.Content);
            return categories;
        }
        public string CreateCategories(string CategoryName)
        {
            var client = new RestSharp.RestClient("https://connect.squareup.com/v1/me");
            var post = new RestRequest("/categories", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            post.AddHeader("Authorization", String.Format("Bearer {0}", Token));
            post.AddBody(new
            {
                name = CategoryName
            });
            var response = client.Execute(post);
            Categories sCatagories = new Categories();
            Categories catagories =
                new JavaScriptSerializer().Deserialize<Categories>(response.Content);
            return catagories.id;
        }
        public string GetCategoryId(string Name)
        {
            List<Categories> listCategory = GetAllCategories();
            var catag = listCategory.FirstOrDefault(categories => categories.name.ToUpper() == Name.ToUpper());
            if (catag == null)
            {
                return CreateCategories(Name);
            }
            else
            {
                return catag.id;
            }

        }
        public string InsertProduct(string categoryName, string skuNumber, string name, string description, string price, string qoh)
        {
            try
            {
                string CategoryId = GetCategoryId(categoryName);
                var client = new RestSharp.RestClient("https://connect.squareup.com/v1/me");
                var post = new RestRequest("/items", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                post.AddHeader("Authorization", String.Format("Bearer {0}", Token));
                post.AddBody(new
                {
                    name = name,
                    description = description,
                    category_id = CategoryId,
                    visibility = "PRIVATE",
                    variations = new object[] 
                {
                    new {
                            name = "Regular",
                            pricing_type = "FIXED_PRICING",
                            price_money = new 
                            {
                                currency_code = "USD",
                                amount = price
                            },
                            sku = skuNumber,
                            track_inventory = "true",
                        }
                    },
                });
                var response = client.Execute(post);
                // Categories sCategories = new Categories();
                Item item =
                    new JavaScriptSerializer().Deserialize<Item>(response.Content);
                if (item != null)
                {
                    var firstOrDefault = item.variations.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        string variationId = firstOrDefault.id;
                        AdjustInventory(variationId, qoh, "Stock Adjust");
                    }
                }
                // return categories;

                daLayer.SJDeals_SquareLogger(categoryName, skuNumber, name, description, price, qoh, item.id, response.Content);
                return item.id;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
           
        }
        public void AdjustInventory(string variationId, string quantityDelta,  string memo)
        {
            var client = new RestSharp.RestClient("https://connect.squareup.com/v1/me");
            var post = new RestRequest("/inventory/"+variationId, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            post.AddHeader("Authorization", String.Format("Bearer {0}", Token));
            post.AddBody(new
            {
                quantity_delta = quantityDelta,
                adjustment_type = "RECEIVE_STOCK",
                memo = memo
            });
            var response = client.Execute(post);
        }
        public List<Item> GetAllItems()
        {
            var client = new RestSharp.RestClient("https://connect.squareup.com/v1/me");
            var post = new RestRequest("/items", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            post.AddHeader("Authorization", String.Format("Bearer {0}", Token));
            var response = client.Execute(post);
          List<Item> Items =
               new JavaScriptSerializer().Deserialize<List<Item>>(response.Content);
          return Items;
        }
        public void ListInventory()
        {
            var client = new RestSharp.RestClient("https://connect.squareup.com/v1/me");
            var post = new RestRequest("/inventory", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            post.AddHeader("Authorization", String.Format("Bearer {0}", Token));
            var response = client.Execute(post);
        
        }
    }

    public class Categories
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class PriceMoney
    {
        public string currency_code { get; set; }
        public int amount { get; set; }
    }

    public class Variation
    {
        public string inventory_alert_type { get; set; }
        public bool track_inventory { get; set; }
        public string pricing_type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public PriceMoney price_money { get; set; }
        public string sku { get; set; }
        public int ordinal { get; set; }
        public string item_id { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Item
    {
        public IList<object> images { get; set; }
        public IList<object> fees { get; set; }
        public IList<object> modifier_lists { get; set; }
        public IList<Variation> variations { get; set; }
        public bool available_for_pickup { get; set; }
        public bool available_online { get; set; }
        public string visibility { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string category_id { get; set; }
        public Category category { get; set; }
        public string type { get; set; }
        public string variation_id { get; set; }
        public int quantity_on_hand { get; set; }
    }
}