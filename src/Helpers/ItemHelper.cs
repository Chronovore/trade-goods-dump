using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using Logger = TradeGoodsDump.Logging.Logger;

namespace TradeGoodsDump.Helpers
{
    /// <summary>
    /// Simplifies item testing.  Pure Behavior, no state.  
    /// </summary>
    internal static class ItemHelper
    {
        internal static void TestItemAvailability()
        {
            if (!HasGame) return;
            
            try
            {

                Logger.Info($"\tItem Categories: {CategoryCount}");
                if (HasObjectManager)
                {
                    Logger.Info($"\tTrade Goods: {TradeGoodsCount}");
                }
                if (HasObjectManager)
                {
                    Logger.Info($"\tAnimals: {AnimalsCount}");
                }
            }
            catch (Exception e)
            {
                Logger.Info($"\tException during Item Test:  {e.Message}");
            }
        }

        internal static MBReadOnlyList<ItemCategory> Categories => HasGame ? ItemCategory.All : null;
        internal static IEnumerable<ItemObject> Animals => HasGame ? ItemObject.All.Where(x => x.IsAnimal) : null;
        internal static IEnumerable<ItemObject> TradeGoods => HasGame ? ItemObject.AllTradeGoods : null;

        private static int AnimalsCount => Animals?.Count() ?? 0;
        private static int CategoryCount => Categories?.Count ?? 0;
        private static bool HasGame => Game.Current != null;
        private static bool HasObjectManager => Game.Current?.ObjectManager != null;
        private static int TradeGoodsCount => TradeGoods?.Count() ?? 0;
    }
}