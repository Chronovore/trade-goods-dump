using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TradeGoodsDump.Helpers;
using TradeGoodsDump.Logging;

namespace TradeGoodsDump.IO
{
    internal static class CsvDumper
    {
        private const string ITEM_CATEGORIES_FILE_NAME = "ItemCategories.csv";
        private const string TRADE_GOODS_FILE_NAME = "TradeGoods.csv";
        
        internal static void DumpTradeGoods()
        {
            Logger.Info("Dumping Trade Goods");
            var tradeGoodsAndAnimals = new List<ItemObject>(ItemHelper.TradeGoods);
            tradeGoodsAndAnimals.AddRange(ItemHelper.Animals);
            var tradeGoodsTask = DumpToFileAsync(tradeGoodsAndAnimals, TRADE_GOODS_FILE_NAME, 
                new[] {"Name", "Value", "IsAnimal"}, 
                o => $"{o.Name}, {o.Value}, {o.IsAnimal}");
            
            var categoriesTask = DumpToFileAsync(ItemHelper.Categories, ITEM_CATEGORIES_FILE_NAME, 
                new[] {"Name", "AvgValue", "IsAnimal", "Demand", "LuxDemand"}, 
                o => $"{o.GetName()}, {o.AverageValue}, {o.IsAnimal}, {o.BaseDemand}, {o.LuxuryDemand}");
        }
        
        private static async Task DumpToFileAsync<T>(IEnumerable<T> data, string fileName, string[] headers, Func<T, string> rowBuilder)
        {
            Logger.Info($"Started dumping {fileName}");
            var dataArray = data as T[] ?? data.ToArray();
            if (!dataArray.Any())
            {
                return;
            }

            using (var file = File.CreateText(fileName))
            {
                await file.WriteLineAsync(string.Join(",", headers));
                foreach (var datum in dataArray)
                {
                    try
                    {
                        await file.WriteLineAsync(rowBuilder(datum));
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Error while dumping {fileName}: {e.Message}\n{e.StackTrace}");
                    }
                }
            }
            
            Logger.Info($"Finished dumping {fileName}");
        }
    }
}