using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer1
{
    public class DataLayer
    {
        private readonly TradingContext _context;

        public DataLayer()
        {
            _context = new TradingContext();
            ClearTables();
            SeedData();
        }

        public async Task<List<CurrencyPair>> GetCurrencyPairs()
        {
            return await _context.CurrencyPairs.ToListAsync();
        }

        public async Task UpdateCurrencyPairValues(int pairId, decimal newMinValue, decimal newMaxValue)
        {
            var pair = await _context.CurrencyPairs.FindAsync(pairId);
            if (pair != null)
            {
                pair.MinValue = newMinValue;
                pair.MaxValue = newMaxValue;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle not found scenario
            }
        }
        public async Task<List<CurrencyPair>> LoadCurrencyPairsWithMinMaxValues()
        {
            return await _context.CurrencyPairs
                .Select(pair => new CurrencyPair
                {
                    PairId = pair.PairId,
                    CurrencyId1 = pair.CurrencyId1,
                    CurrencyId2 = pair.CurrencyId2,
                    MinValue = pair.MinValue,
                    MaxValue = pair.MaxValue
                })
                .ToListAsync();
        }

        public async Task SaveCurrencyPairMinMaxValues(int pairId, decimal minVal, decimal maxVal)
        {
            var pair = await _context.CurrencyPairs.FindAsync(pairId);
            if (pair != null)
            {
                pair.MinValue = minVal;
                pair.MaxValue = maxVal;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle not found scenario
            }
        }
        public static void SeedData()
        {
            using (var context = new TradingContext())
            {
                // Inserting currencies
                var currencies = new List<Currency>
            {
                new Currency { Country = "Israel", CurrencyName = "Shekel", CurrencyAbbreviation = "ILS" },
                new Currency { Country = "Eurozone", CurrencyName = "Euro", CurrencyAbbreviation = "EUR" },
                new Currency { Country = "United States", CurrencyName = "Dollar", CurrencyAbbreviation = "USD" },
                new Currency { Country = "United Kingdom", CurrencyName = "Pound", CurrencyAbbreviation = "GPB" },
                // Add other currencies
            };

                context.Currencies.AddRange(currencies);
                context.SaveChanges();

                // Inserting currency pairs
                var currencyPairs = new List<CurrencyPair>
            {
                new CurrencyPair { CurrencyId1 = 1, CurrencyId2 = 2, MinValue = 3.5m, MaxValue = 4.5m },
                new CurrencyPair { CurrencyId1 = 2, CurrencyId2 = 3, MinValue = 0.8m, MaxValue = 1.2m },
                new CurrencyPair { CurrencyId1 = 3, CurrencyId2 = 4, MinValue = 0.60m, MaxValue = 0.90m },
                // Add other currency pairs with their min/max values
            };

                context.CurrencyPairs.AddRange(currencyPairs);
                context.SaveChanges();
            }
        }
        public static void ClearTables()
        {
            using (var context = new TradingContext())
            {
                context.Currencies.RemoveRange(context.Currencies);
                context.CurrencyPairs.RemoveRange(context.CurrencyPairs);

                context.SaveChanges();
            }
        }
    }

}
