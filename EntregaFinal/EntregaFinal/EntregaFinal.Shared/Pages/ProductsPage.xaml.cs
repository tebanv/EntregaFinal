﻿using EntregaFinal.Components;
using EntregaFinal.Helpers;
using EntregaFinal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EntregaFinal.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            this.InitializeComponent();
        }
        public ObservableCollection<Product> Products { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            Loader loader = new Loader("Por favor espere...");
            loader.Show();
            Response response = await ApiService.GetListAsync<Product>("products");
            loader.Close();

            if (response != null && !response.IsSuccess)
            {
                MessageDialog dialog = new MessageDialog(response.Message, "Error");
                await dialog.ShowAsync();
                return;
            }

            List<Product> products = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(products);
            RefreshList();

        }
        private void RefreshList()
        {
            ProductsListView.ItemsSource = null;
            ProductsListView.Items.Clear();
            ProductsListView.ItemsSource = Products;
        }
    }
}
