# Trade Goods Dump

A mod for Mount & Blade II: Bannerlord that creates a CSV file that contains all trade goods and animals in the game with their average price.

This project is just to help me learn modding for Bannerlord.  There is no polish on this project for general consumption.

If you do use this mod, I suggest running it only once and then quitting the game and disabling the mod.  Unless you add other mods that modify the trade goods and animals, the usefulness of this mod ends when it is run once and it has a performance cost.

The output of the mod can be found in the `bin\Win64_Shipping_Client` folder under your Bannerlord install.

## Development Setup
1. Deploy Script - There's a PowerShell script that can be used to easily copy the dll and SubModule.xml to the proper location for Bannerlord Modules.  This should be customized with paths that match your environment.
1. Update references - The C# project references the game's dlls.  You'll need to update the references to point to your game's dlls, usually found in `C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client`

## Developing Mods for Bannerlord
### Minimum Requirements
Create a class that extends `MBSubModuleBase` and override the event functions.  Create a SubModule.xml file and populate it similar to what you see in mine.

### SubModule events
There are a few key things to notice when developing SubModules for Bannerlord.  The SubModules hook into an event system.  The SubModule class has overrides for these events.  To respond to an event, override the desired event function.  Here's a breakdown of when SubModule events are fired.

        Application start:
            OnSubModuleLoad
            OnApplicationTick (continuously)
         
        Arriving at Main Menu (from app start or from ending a game): 
            OnBeforeInitialModuleScreenSetAsRoot
         
        Load Game:
            OnGameStart
            BeginGameStart
            OnGameLoaded - Unique to Loading a game.
            OnGameInitializationFinished - Game Data loaded now
            DoLoading
         
        Entering Scenic View (non-map):
            OnMissionBehaviourInitialize
         
        Exiting Game:
            OnGameEnd
            OnBeforeInitialModuleScreenSetAsRoot
         
        Starting Campaign Or Custom Battle:
            OnGameStart
            BeginGameStart
            OnCampaignStart
            OnGameInitializationFinished - Game Data loaded now
            DoLoading
            OnNewGameCreated - Unique to starting campaign
         
        Quitting Application:
            OnSubModuleUnloaded

### SubModule.xml
Also, the SubModule.xml has a strange behavior.  The version must start with a letter otherwise the game launcher will fail to load.

### Logging
Bannerlord has logging, but I haven't figured out how to enable it.  So I wrote a custom logger to help me determine when events happen.