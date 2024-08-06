

using System.Net;
using System.Reflection;
using System.Transactions;

 public class LocalCurrency 
    {
        private static readonly LocalCurrency _instance = new LocalCurrency();
        public readonly  Dictionary<string,float[]> _currencies;
        public LocalCurrency()
        {
            _currencies = new Dictionary<string, float[]>();
        }

        public static LocalCurrency Instance
        {
            get 
            { 
                    return _instance;
            }
        }

        public bool AddCurrencyValues (string codeCountry, float[] values)
        {
            float []? currentValues;
            
            if(!_currencies.TryGetValue(codeCountry, out  currentValues))
            {
                _currencies.Add(codeCountry, values);
                return true;
            }
            else
            {
                return false;
            }

        }

        public float []? GetCurrency(string countryCode)
        {
            float []? values;
            if(_currencies.TryGetValue(countryCode, out values))
            {
                return values;
            }

            return null;
        }


    }
