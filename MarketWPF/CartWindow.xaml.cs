using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MarketWPF
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public List<Product> CartProducts { get; set; }
        public event EventHandler<ProductEventArgs> ProductRemovedFromCart;

        public CartWindow()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            string cartData = File.ReadAllText("cart.json");
            CartProducts = JsonConvert.DeserializeObject<List<Product>>(cartData);
            CartListBox.ItemsSource = CartProducts;
        }

        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartListBox.SelectedItem is Product selectedProduct)
            {
                CartProducts.Remove(selectedProduct);
                Product productInStore = MainWindow.Products.FirstOrDefault(p => p.Name == selectedProduct.Name);
                if (productInStore != null)
                {
                    productInStore.Quantity++;
                }

                SaveCart();
                ProductRemovedFromCart?.Invoke(this, new ProductEventArgs { Product = selectedProduct });
            }
        }

        private void SaveCart()
        {
            string cartData = JsonConvert.SerializeObject(CartProducts, Formatting.Indented);
            File.WriteAllText("cart.json", cartData);
        }
    }
}

