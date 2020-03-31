using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TradeGoodsDump.Helpers;
using TradeGoodsDump.IO;
using TradeGoodsDump.Logging;

namespace TradeGoodsDump
{
    public class TradeGoodsDumpSubModule : MBSubModuleBase
    {
        /*
         * Flow
         * Application start:
         *     OnSubModuleLoad
         *     OnApplicationTick (continuously)
         *
         * Arriving at Main Menu (from start or from ending a game): 
         *     OnBeforeInitialModuleScreenSetAsRoot
         *
         * Load Game:
         *     OnGameStart
         *     BeginGameStart
         *     OnGameLoaded - Unique to Loading a game.
         *     OnGameInitializationFinished
         *     DoLoading
         *
         * Entering Scenic View (non-map):
         *     OnMissionBehaviourInitialize
         *
         * Exiting Game:
         *     OnGameEnd
         *     OnBeforeInitialModuleScreenSetAsRoot
         *
         * Starting Campaign Or Custom Battle:
         *     OnGameStart
         *     BeginGameStart
         *     OnCampaignStart
         *     OnGameInitializationFinished
         *     DoLoading
         *     OnNewGameCreated - Unique to starting campaign
         *
         * Quitting Application:
         *     OnSubModuleUnloaded
         */
        
        
        #region Initialization
        
        /// <summary>
        /// Called First, before the main menu.
        /// </summary>
        protected override void OnSubModuleLoad()
        {
            // ReSharper disable once ObjectCreationAsStatement - Creates a static instance.
            new Logger("logs", "TradeGoodsDump.log");
            Logger.Info("OnSubModuleLoad");
            base.OnSubModuleLoad();
        }
        
        private bool _hasTicked;
        /// <summary>
        /// Called every APPLICATION tick, not just in game.
        /// </summary>
        /// <param name="dt"></param>
        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
            
            if (_hasTicked)
            {
                return;
            }
            
            Logger.Info("OnApplicationTick");
            _hasTicked = true;
        }
        
        /// <summary>
        /// Called before the main menu is displayed.
        /// Also called before the Campaign starts?
        /// </summary>
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            Logger.Info("OnBeforeInitialModuleScreenSetAsRoot");
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }

        #endregion
        
        /// <summary>
        /// Called when the game type is started, before any data loaded.
        /// Sometimes the game's object manager hasn't been loaded.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameStarterObject"></param>
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            Logger.Info("OnGameStart");
            base.OnGameStart(game, gameStarterObject);
        }
        
        /// <summary>
        /// Called after OnGameStart.
        /// Trade goods partially available, no item categories.
        /// Sometimes the game's object manager hasn't been loaded.
        /// </summary>
        /// <param name="game"></param>
        public override void BeginGameStart(Game game)
        {
            Logger.Info("BeginGameStart");
            base.BeginGameStart(game);
        }
        
        /// <summary>
        /// Called after BeginGameStart when creating a new campaign OR Custom Battle.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="starterObject"></param>
        public override void OnCampaignStart(Game game, object starterObject)
        {
            Logger.Info("OnCampaignStart");
            base.OnCampaignStart(game, starterObject);
            ItemHelper.TestItemAvailability();
        }
        
        /// <summary>
        /// Called after BeginGameStart.  Item availability unknown.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="initializerObject"></param>
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            Logger.Info("OnGameLoaded");
            base.OnGameLoaded(game, initializerObject);
        }
        
        /// <summary>
        /// Called after OnGameLoaded.  All categories and trade goods initialized.
        /// Common to all game startup types.
        /// </summary>
        /// <param name="game"></param>
        public override void OnGameInitializationFinished(Game game)
        {
            Logger.Info("OnGameInitializationFinished");
            base.OnGameInitializationFinished(game);
            ItemHelper.TestItemAvailability();
            
            CsvDumper.DumpTradeGoods();
        }
        
        /// <summary>
        /// Called after OnGameInitializationFinished.  All item categories and trade goods loaded.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public override bool DoLoading(Game game)
        {
            Logger.Info("DoLoading");
            return base.DoLoading(game);
        }
        
        /// <summary>
        /// Called after DoLoading.  Only for brand new games.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="initializerObject"></param>
        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            Logger.Info("OnNewGameCreated");
            base.OnNewGameCreated(game, initializerObject);
        }
        
        /// <summary>
        /// When a battle or quest conversation scene starts.
        /// </summary>
        /// <param name="mission"></param>
        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            Logger.Info("OnMissionBehaviourInitialize");
            base.OnMissionBehaviourInitialize(mission);
        }

        /// <summary>
        /// When returning to the main menu.  Game exists, but game data is unloaded.
        /// </summary>
        /// <param name="game"></param>
        public override void OnGameEnd(Game game)
        {
            Logger.Info("OnGameEnd");
            base.OnGameEnd(game);
        }
        
        public override void OnMultiplayerGameStart(Game game, object starterObject)
        {
            Logger.Info("OnMultiplayerGameStart");
            base.OnMultiplayerGameStart(game, starterObject);
            ItemHelper.TestItemAvailability();
        }
        
        /// <summary>
        /// When Application closes.
        /// </summary>
        protected override void OnSubModuleUnloaded()
        {
            Logger.Info("OnSubModuleUnloaded");
            Logger.Close();
            base.OnSubModuleUnloaded();
        }
    }
}