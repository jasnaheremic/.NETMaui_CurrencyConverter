using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KonverterValuta.Models;
using KonverterValuta.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonverterValuta.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly ICurrencyService _currencyService;

        [ObservableProperty]
        private List<string> _currenciesFull;

        [ObservableProperty]
        private List<string> _currenciesAbbreviation;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private decimal _convertedAmount;

        [ObservableProperty]
        private string _selectedCurrency;

        [ObservableProperty]
        private string _selectedConvert;


        public MainPageViewModel(ICurrencyService currencyService)
        {
            _currencyService = currencyService;

            GetCurrencyDataAsync();

        }

        private async void GetCurrencyDataAsync()
        {
            try
            {

                Dictionary<string, string> currenciesData = await _currencyService.getCurrency();
                CurrenciesFull = new List<string>(currenciesData?.Values);
                CurrenciesAbbreviation = new List<string>(currenciesData?.Keys);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Message: {ex.Message}");
            }
        }

        [RelayCommand]
        async Task CheckInternet()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                ConvertCurrencyDataAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Provjerite konekciju", $"Current status: {accessType}", "Ok");
            }
        }


        private async void ConvertCurrencyDataAsync()
        {
            string currency1A = string.Empty;
            string currency2A = string.Empty;

            try
            {

                if(string.IsNullOrEmpty(SelectedCurrency) || string.IsNullOrEmpty(SelectedConvert))
                {
                    await Shell.Current.DisplayAlert("Error!", "Please select both currencies", "OK");
                    return;
                }

                for (int i = 0; i <= CurrenciesFull.Count(); i++) {

                    if (CurrenciesFull[i] == SelectedCurrency)
                    {
                        currency1A = CurrenciesAbbreviation[i];
                        break;
                    }

                }

                for (int i = 0; i <= CurrenciesFull.Count(); i++)
                {

                    if (CurrenciesFull[i] == SelectedConvert)
                    {
                        currency2A = CurrenciesAbbreviation[i];
                        break;
                    }

                }

                decimal currentAmount = Amount;

                ConvertedData conversionResult = await _currencyService.convertCurrency(currentAmount, currency1A, currency2A);
            

                if (conversionResult != null && conversionResult.rates != null && conversionResult.rates != null)
                {
                    ConvertedAmount = conversionResult.rates.Values.FirstOrDefault();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Unable to convert currency", "Ok");
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Message: {ex.Message}");
            }

        }


       
    }
}
