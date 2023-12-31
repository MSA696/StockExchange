﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using BusinessLayer1;

namespace StockExchange1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BusinessLayer1.TradingService _tradingService;

        public MainWindow()
        {
            InitializeComponent();
            _tradingService = new BusinessLayer1.TradingService();
            LoadTradingPairData();
        }

        private async void LoadTradingPairData()
        {
            while (true)
            {
                // Fetch trading pair data from Business Layer
                List<BusinessLayer1.TradingService.TradingPair> tradingPairs = await _tradingService.GetTradingPairs();

                // Bind the data to the DataGrid
                dataGridTradingPairs.ItemsSource = tradingPairs;
                await Task.Delay(2000); // Simulate changes every 2 seconds
                await _tradingService.SimulateTradesAndUpdateValues();
            }
        }
    }
}