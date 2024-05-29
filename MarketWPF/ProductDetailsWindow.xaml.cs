using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        public Product Product { get; set; }

        public event EventHandler<ProductEventArgs> ProductAddedToCart;

        public ProductDetailsWindow(Product product)
        {
            InitializeComponent();
            Product = product;
            ProductName.Text = Product.Name;
            ProductQuantity.Text = $"Quantity: {Product.Quantity}";
            ProductImage.Source = new BitmapImage(new Uri(Product.ImagePath, UriKind.RelativeOrAbsolute));
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Product.Quantity > 0)
            {
                Product.Quantity--;
                ProductQuantity.Text = $"Quantity: {Product.Quantity}";
                ProductAddedToCart?.Invoke(this, new ProductEventArgs { Product = Product });
            }
            else
            {
                MessageBox.Show("Product is out of stock!");
            }
        }
    }

    public class ProductEventArgs : EventArgs
    {
        public Product Product { get; set; }
       
    }
}

