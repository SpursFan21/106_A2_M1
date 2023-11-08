﻿using _106_A2_M1.Services;
using _106_A2_M1.View;
using _106_A2_M1.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _106_A2_M1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainViewModel mainViewModel = ViewModelLocator.Instance.MainViewModel;
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();

        }

        LoginViewModel loginViewModel = new LoginViewModel();
        LoginView loginView = new LoginView();
    }

}
