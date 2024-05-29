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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            string productData = File.ReadAllText("products.json");
            Products = JsonConvert.DeserializeObject<List<Product>>(productData);
            ProductListBox.ItemsSource = Products;
        }

        private void ProductListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                ProductDetailsWindow detailsWindow = new ProductDetailsWindow(selectedProduct);
                detailsWindow.ProductAddedToCart += OnProductAddedToCart;
                detailsWindow.Show();
            }
        }

        private void OnProductAddedToCart(object sender, ProductEventArgs e)
        {
            SaveProducts();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow profileWindow = new UserProfileWindow();
            profileWindow.Show();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow();
            cartWindow.ProductRemovedFromCart += OnProductRemovedFromCart;
            cartWindow.Show();
        }

        private void OnProductRemovedFromCart(object sender, ProductEventArgs e)
        {
            SaveProducts();
            LoadProducts(); // Обновляем отображение продуктов
        }

        private void SaveProducts()
        {
            string productData = JsonConvert.SerializeObject(Products, Formatting.Indented);
            File.WriteAllText("products.json", productData);
        }
    }
}
