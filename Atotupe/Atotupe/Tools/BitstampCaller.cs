using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Atotupe.Tools
{
    public class BitstampCaller
    {
        const string Url = "https://www.bitstamp.net/api/v2/ticker/";

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task GetAllUsd()
        {
            HttpClient client = await GetClient();

            string btc = await client.GetStringAsync(Url + "btcusd");
            string bch = await client.GetStringAsync(Url + "bchusd");
            string eth = await client.GetStringAsync(Url + "ethusd");
            string ltc = await client.GetStringAsync(Url + "ltcusd");
            string xrp = await client.GetStringAsync(Url + "xrpusd");

            var btcMoney = JsonConvert.DeserializeObject<BitstampCurrency>(btc);
            var bchMoney = JsonConvert.DeserializeObject<BitstampCurrency>(bch);
            var ethMoney = JsonConvert.DeserializeObject<BitstampCurrency>(eth);
            var ltcMoney = JsonConvert.DeserializeObject<BitstampCurrency>(ltc);
            var xrpMoney = JsonConvert.DeserializeObject<BitstampCurrency>(xrp);

            App.CurrencyPriceUpdated?.Invoke(this, new CurrencyPriceUpdatedArgs
            {
                CurrencyCode = "BTC",
                NewPrice = btcMoney.last
            });

            App.CurrencyPriceUpdated?.Invoke(this, new CurrencyPriceUpdatedArgs
            {
                CurrencyCode = "BCH",
                NewPrice = bchMoney.last
            });

            App.CurrencyPriceUpdated?.Invoke(this, new CurrencyPriceUpdatedArgs
            {
                CurrencyCode = "ETH",
                NewPrice = ethMoney.last
            });

            App.CurrencyPriceUpdated?.Invoke(this, new CurrencyPriceUpdatedArgs
            {
                CurrencyCode = "LTC",
                NewPrice = ltcMoney.last
            });

            App.CurrencyPriceUpdated?.Invoke(this, new CurrencyPriceUpdatedArgs
            {
                CurrencyCode = "XRP",
                NewPrice = xrpMoney.last
            });

            App.UsdBtc = btcMoney.last;
            App.UsdBch = bchMoney.last;
            App.UsdEth = ethMoney.last;
            App.UsdLtc = ltcMoney.last;
            App.UsdXrp = xrpMoney.last;
        }
    }

    public class BitstampCurrency
    {
        /*
         * last	Last BTC price.
           high	Last 24 hours price high.
           low	Last 24 hours price low.
           vwap	Last 24 hours volume weighted average price.
           volume	Last 24 hours volume.
           bid	Highest buy order.
           ask	Lowest sell order.
           timestamp	Unix timestamp date and time.
           open	First price of the day.
           Bitstamp only return string.
         */

        public double last { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double vwap { get; set; }
        public double volume { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
        public double timestamp { get; set; }
        public double open { get; set; }
    }
}
