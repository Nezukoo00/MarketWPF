using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarketWPF
{
    public class Cart
    {
        private static Cart _instance;

        public static Cart Instance => _instance ?? (_instance = new Cart());

        public List<Product> Products { get; private set; } = new List<Product>();

        private const string CartFilePath = "cart.json";

        private Cart()
        {
            LoadCart();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            SaveCart();
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
            SaveCart();
        }

        public void LoadCart()
        {
            if (File.Exists(CartFilePath))
            {
                var json = File.ReadAllText(CartFilePath);
                Products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
        }

        public void SaveCart()
        {
            var json = JsonSerializer.Serialize(Products);
            File.WriteAllText(CartFilePath, json);
        }
    }
}
