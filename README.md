# a-ffxiv-hunt-tracker [WIP]

### A(n) FFXIV Hunt Map / Radar

A standalone program that displays the current map and shows the players position, spawn points, and nearby **A, B, S, SS** Ranks.  

Accurately depicts the location, direction, and movement of the player and any rendered mobs.   
All at a smooooooth 166.66 refresh rate.

# How do I use it?

[GFYCAT OF THIS PROGRAM IN ACTION, CLICK HD](https://gfycat.com/ifr/BigYoungIcefish)

When in a map with hunt mobs, the relevant map image will appear and a player icon with a detection circle will appear in relation to the players position.

This detection circle covers 2 in-game coordinates in either direction around the player, and through personal testing is quite accurate.

When an **A / B / S / SS** rank mob is found, a mob icon will be displayed corresponding to it's position.


![Image of S and B, showing Rank Priority](https://i.imgur.com/kIjmPkI.png)
The TOP text section depicts the current PRIORITY mob. **SS > S > A > B**. 

The toggleable panel **[Shortcut: TAB, or click on the arrow]** outlines all nearby hunt mobs' rank, name, position, and HP %.

Mouse over the top section to see the full name.
![Image of Mouse Over Text](https://i.imgur.com/FWkbjAK.png)




![IMAGE OF MOUSE OVER MOB](https://i.imgur.com/QeJ5Jdn.png)
Alternatively, you can mouse over a player or mob icon to show a tooltip providing the same information regarding that mob -- useful if you have the panel collapsed.


You can also mouse over anywhere on the map to get the equivalent in-game **(XX.X, YY.Y)** coordinates.


### Current (hardcoded) shortcuts:
- **CTRL + Q** - OPEN SETTINGS WINDOW
- **CTRL + A**  - TOGGLE ALWAYS ON TOP
- **CTRL + S**  - TOGGLE OPACITY
- **CTRL + F**  - TOGGLE SS MAP
- **TAB**       - TOGGLE SIDE PANEL
- Double click the top or ALT + F4 to exit
- Click and drag from anywhere to move the window around.


# How do I change things?  
```
├── Data
│   ├── ARR-A.json
|   ├── config.json
│   └── ...
├── Images
│   ├── Icons
|   |   ├── Mob Icons
|   |   └── Player Icon
│   └── Maps
|       ├── *.jpg
│       └── SS Maps  
```
Mob data can be changed in the jsons files in the **Data** folder using the structure provided (Do not delete these files).

Icons can be changed in the **Images/Icons** subfolders. Replace images using the same file name, **512x512  1:1** recommended.

Map images can be changed in the **Images/Maps** folder, if you want to compress the images to reduce filesize. **1:1** ratio needed.    
It is recommended that you do not change these. If you change the map images, positions might not be accurate.

Must follow the format _"{MapName}-data.jpg"_ with _ replacing spaces.  
These are the default file names from [Cable Monkey](http://cablemonkey.us/huntmap2/).  

The **Images/SS Maps** names are changed from the default file names to follow the format _"{MapName}\_SS-data.jpg"_

The current images and shortcuts are placeholders.

--
## Settings Window
![Settings Window Image](https://i.imgur.com/Rg38pbv.png)  

You can open the settings window with CTRL+Q,  

In this window you can set various values as described. Image / Windows sizes are pixels.  

Logging S Ranks saves to a text file on your desktop called "UFHT S Log.txt" in the format shown below,  
>Sphatika	 (18.3, 23.3)	 Local: 13/01/22 2:25:04 PM | UTC: 13/01/22 3:25:04 AM




## Config.json

Settings changed in the settings window are saved here.  
You have edit the json file manually, but be careful.   
If you break something you can delete this file and rerun the program. It will create a new default config on startup.  

You can manually customise the hotkeys using the enums in the following links:  
https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.key   
https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.modifierkeys


# TODO (no promises):
- Replace Endwalker Map images, once Cable Monkey releases new maps.
- FATE detection, at least for Minho, Senmurv, Orghana, Special Fates.
- ~~SS Maps showing location of minions - low prio~~
- ~~Settings Page~~
  - Customisable shortcuts
  - Customisable text font / colours / weight / etc
  - ~~Resize Window - maintaining aspect ratio~~
  - ~~Resizable Icons~~
  - ~~Customisable opacity~~
  - ~~Save and load settings~~
- 'Record' and save a list of mob info for 'hunt trains'
- TTS (suggest using triggernometry tbh)
- ACT plugin version
- ..tbd


### Credits and other stuff
This project uses [Sharlayan](https://github.com/FFXIVAPP/sharlayan) to read the game memory, and the map images were created by [Cable Monkey of Goblin](http://cablemonkey.us/huntmap2/).

All other imagery used (mob icons, backgrounds images, some maps) are copyright of Square Enix.

This is a personal project created to practice WPF, personal learning, and to create something useful to myself.
