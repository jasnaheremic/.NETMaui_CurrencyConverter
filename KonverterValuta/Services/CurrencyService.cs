using KonverterValuta.Common;
using KonverterValuta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KonverterValuta.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _client;
        private const string Url = Constants.ApiUrl;
        

        public CurrencyService()
        {
            _client = new HttpClient();
        }

        public async Task<ConvertedData> convertCurrency(decimal amount, string Base, string convert)
        {
            var url = $"https://api.frankfurter.app/latest?amount={amount}&from={Base}&to={convert}";

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var convertedData = await JsonSerializer.DeserializeAsync<ConvertedData>(responseStream);

            return convertedData;

        }

        public async Task<Dictionary<string, string>> getCurrency()
        {
            try {

                string json = await _client.GetStringAsync(Url);
                Dictionary<string, string> response = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                return response ?? new Dictionary<string, string>();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Message: {ex.Message}");
                return new Dictionary<string, string>();
            }
            

        }

    
    }
}
