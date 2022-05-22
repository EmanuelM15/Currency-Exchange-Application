using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Currency_Exchange_Application
{
    class CurrencyConverter
    {
        public static string[] GetCurrencyTags()
        {

            // Currencies
            return new string[] {"eur", "usd", "jpy", "bgn", "czk", "dkk", "gbp", "huf", "ltl", "lvl"
            , "pln", "ron", "sek", "chf", "nok", "hrk", "rub", "try", "aud", "brl", "cad", "cny", "hkd", "idr", "ils"
            , "inr", "krw", "mxn", "myr", "nzd", "php", "sgd", "zar"};
        }

       
        public static float GetCurrencyRateInEuro(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("currency cannot be empty!");
            if (currency.ToLower() == "eur")
                throw new ArgumentException("Cannot get exchange rate for the eur to eur");

            try
            {
               
                string rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.ToLower() + ".html");

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(rssUrl);

                // XmlNamespaceManager for handling XML namespaces.
                System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
                nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

                // Get list of daily currency exchange rate 
                System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr);

                
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    // Create a CultureInfo, this is because EU and USA use different sepperators in float (, or .)
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";
                    try
                    {
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any,
                            ci);

                        return exchangeRate;
                    }
                    catch { }
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }
        public static float GetExchangeRate(string from, string to, float amount = 1)
        {
           
            if (from == null || to == null)
                return 0;

            
            if (from.ToLower() == "eur" && to.ToLower() == "eur")
                return amount;

            try
            {
                
                float toRate = GetCurrencyRateInEuro(to);
                float fromRate = GetCurrencyRateInEuro(from);

              
                if (from.ToLower() == "eur")
                {
                    return (amount * toRate);
                }
                else if (to.ToLower() == "eur")
                {
                    return (amount / fromRate);
                }
                else
                {
                   
                    return (amount * toRate) / fromRate;
                }
            }
            catch { return 0; }
        }
    }
}
