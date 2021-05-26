using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;

using Paradise.Models;
using Paradise.Views;
using Paradise.ViewModels;
using System.Collections.ObjectModel;

namespace Paradise.Views
{ 
    public partial class ItemsPage : ContentPage
    {
        IAdapter adapter;
        IBluetoothLE bluetoothBLE;
        ObservableCollection<IDevice> list = new ObservableCollection<IDevice>();

        private IDevice device;
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            bluetoothBLE = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;

            list = new ObservableCollection<IDevice>();
            ItemsCollectionView.ItemsSource = list;

            BindingContext = viewModel = new ItemsViewModel();
        }



        private async void searchDevice(object sender, EventArgs e)
        {
            if (bluetoothBLE.State == BluetoothState.Off)
            {
                await DisplayAlert("Atenção", "Bluetooth desabilitado.", "OK");
            }
            else
            {
                list.Clear();

                adapter.ScanTimeout = 10000;
                adapter.ScanMode = ScanMode.Balanced;


                adapter.DeviceDiscovered += (obj, a) =>
                {
                    if (!list.Contains(a.Device))
                        list.Add(a.Device);
                };

                await adapter.StartScanningForDevicesAsync();
            }
        }


        private async void DevicesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            device = ItemsCollectionView.SelectedItem as IDevice;

            var result = await DisplayAlert("AVISO", "Deseja se conectar a esse dispositivo?", "Conectar", "Cancelar");

            if (!result)
                return;

            //Stop Scanner
            await adapter.StopScanningForDevicesAsync();

            try
            {
                await adapter.ConnectToDeviceAsync(device);

                await DisplayAlert("Conectado", "Status:" + device.State, "OK");

            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }

        }
        /// ////

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}