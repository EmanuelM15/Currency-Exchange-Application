using Currency_Exchange_Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fromCurrency = "EUR";
            string toCurrency = "USD";
            int amount = 1;

            string[] availableCurrency = CurrencyConverter.GetCurrencyTags();
            // Print current Currencies
            Console.WriteLine("Available Currencies");
            Console.WriteLine(string.Join(",", availableCurrency));
            Console.WriteLine("\n");

            //
            // Currency Conversions Inputs
            //

            Console.WriteLine("Insert Currency you want to convert FROM");
            fromCurrency = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Insert Currency you want to convert TO");
            toCurrency = Console.ReadLine();
            Console.WriteLine("\n");

            //
            // Calculate Currencies
            //

            
            float exchangeRate = CurrencyConverter.GetExchangeRate(fromCurrency, toCurrency, amount);
            Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + exchangeRate);

            Console.ReadLine();
        }
    }
}

