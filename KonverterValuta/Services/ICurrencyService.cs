using KonverterValuta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonverterValuta.Services
{
    public interface ICurrencyService
    {
        Task<Dictionary<string, string>> getCurrency();
        Task<ConvertedData> convertCurrency(decimal amount, string Base , string convert);
    }
}
