using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonverterValuta.Models
{

    public class ConvertedData
    {
        public decimal amount { get; set; }
        public string _base { get; set; }
        public string _convert { get; set; }
        public Dictionary<string, decimal> rates { get; set; }

    }

}
