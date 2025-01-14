﻿using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private User _currentUser;

        public UserProfileWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            UsernameTextBlock.Text = $"Username: {_currentUser.Username}";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}

