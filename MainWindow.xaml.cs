﻿using FluentNewsApp_Jasmeet.ViewModels;
using System.Windows;

namespace FluentNewsApp_Jasmeet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}