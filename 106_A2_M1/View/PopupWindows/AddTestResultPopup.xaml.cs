﻿using _106_A2_M1.Model;
using _106_A2_M1.ViewModel;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _106_A2_M1.View.PopupWindows
{
    /// <summary>
    /// Interaction logic for AddTestResultPopup.xaml
    /// </summary>
    public partial class AddTestResultPopup : UserControl
    {
        public AddTestResultPopup()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Reset values to default when the UserControl is loaded
            TypeComboBox.SelectedIndex = 0; // Set the default selection for the ComboBox
            //TestDatePicker.SelectedDate = DateTime.Now;
        }
    }
}
