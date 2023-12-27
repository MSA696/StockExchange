using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer1;

namespace BusinessLayer1
{

    public class TradingService
    {
        private readonly DataLayer _dataLayer; // Assuming DataLayer is a class handling data operations.
        private Dictionary<int, (decimal, decimal)> pairValueMap; // Store current pair values

        public TradingService()
        {
            _dataLayer = new DataLayer(); // Initialize your DataLayer class here or inject it through dependency injection.
            pairValueMap = new Dictionary<int, (decimal, decimal)>();
        }

        public async Task SimulateTradesAndUpdateValues()
        {
            // Populate pairValueMap with initial values from the database
            await InitializePairValueMap();

            Random random = new Random();

             foreach (var pairId in pairValueMap.Keys)
                {
                    var (minValue, maxValue) = pairValueMap[pairId];

                    // Simulate changes in trading pair values
                    decimal randomChange = Convert.ToDecimal(random.NextDouble()) * 0.2m - 0.1m; // Random value between -0.1 and 0.1
                    decimal newMinValue = minValue + randomChange;
                    decimal newMaxValue = maxValue + randomChange;

                    // Check if the values have changed
                    if (newMinValue != minValue || newMaxValue != maxValue)
                    {
                        // Values have changed, update database and update pairValueMap
                        await _dataLayer.UpdateCurrencyPairValues(pairId, newMinValue, newMaxValue);
                        pairValueMap[pairId] = (newMinValue, newMaxValue);
                        Console.WriteLine($"Values changed for PairId: {pairId}. New MinValue: {newMinValue}, New MaxValue: {newMaxValue}");
                    }
                }          
        }
        private async Task InitializePairValueMap()
        {
            var pairs = await _dataLayer.GetCurrencyPairs(); // Fetch initial pair values from the database

            pairValueMap.Clear(); // Clear existing values before adding new ones

            foreach (var pair in pairs)
            {
                pairValueMap[pair.PairId] = (pair.MinValue, pair.MaxValue); // Override existing or add new values
            }
        }
        /*public async Task<List<CurrencyPair>> GetTradingPairs()
        {
            var pairs = await _dataLayer.GetCurrencyPairs();
            return pairs;
        }*/
        public async Task<List<TradingPair>> GetTradingPairs()
        {
            // Call appropriate method in your DataLayer to fetch trading pairs
            var currencyPairs = await _dataLayer.GetCurrencyPairs();

            // Map DataLayer's CurrencyPair objects to BusinessLayer's TradingPair objects if needed
            var tradingPairs = currencyPairs.Select(pair => new TradingPair
            {
                PairId = pair.PairId,
                CurrencyId1 = pair.CurrencyId1,
                CurrencyId2 = pair.CurrencyId2,
                MinValue = pair.MinValue,
                MaxValue = pair.MaxValue
                // Map other properties as required
            }).ToList();

            return tradingPairs;
        }

        public class TradingPair
        {
            public int PairId { get; set; }
            public int CurrencyId1 { get; set; }
            public int CurrencyId2 { get; set; }
            public decimal MinValue { get; set; }
            public decimal MaxValue { get; set; }
            // Add other properties as needed
        }

    }

}
