# a-ffxiv-hunt-tracker [WIP] ![downloads count](https://img.shields.io/github/downloads/imaginary-png/a-ffxiv-hunt-tracker/total.svg)

### A(n) FFXIV Hunt Map / Radar  [(Now with ACT companion plugin)](https://github.com/imaginary-png/a-ffxiv-hunt-tracker.ACT)

A standalone program that displays the current map and shows the players position, spawn points, and nearby **A, B, S, SS** Ranks.  

Accurately depicts the location, direction, and movement of the player and any rendered mobs.   

# How do I use it?

[GFYCAT OF THIS PROGRAM IN ACTION, CLICK HD](https://gfycat.com/ifr/BigYoungIcefish)


https://user-images.githubusercontent.com/70348218/164956167-eee6cb1d-9227-47b3-9561-b24219b34ba2.mp4


When in a map with hunt mobs, the relevant map image will appear and a player icon with a detection circle will appear in relation to the players position.

This detection circle covers 2 in-game coordinates in either direction around the player, and through personal testing is quite accurate.

When an **A / B / S / SS** rank mob is found, a mob icon will be displayed corresponding to its position.


![Image of S and B, showing Rank Priority](https://i.imgur.com/kIjmPkI.png)
The TOP text section depicts the current PRIORITY mob. **SS > S > A > B**. 

The toggleable panel **[Shortcut: TAB, or click on the arrow]** outlines all nearby hunt mobs' rank, name, position, and HP %.

Mouse over the top section to see the full name.
![Image of Mouse Over Text](https://i.imgur.com/FWkbjAK.png)




![IMAGE OF MOUSE OVER MOB](https://i.imgur.com/QeJ5Jdn.png)
Alternatively, you can mouse over a player or mob icon to show a tooltip providing the same information regarding that mob -- useful if you have the panel collapsed.


You can also mouse over anywhere on the map to get the equivalent in-game **(XX.X, YY.Y)** coordinates.


### Default shortcuts:
- **CTRL + Q** - OPEN SETTINGS WINDOW
- **CTRL + W** - TOGGLE CLICK-THROUGH
- **CTRL + A**  - TOGGLE ALWAYS ON TOP (By default, clickthru is enabled when On Top is)
- **CTRL + S**  - TOGGLE OPACITY
- **CTRL + F**  - TOGGLE SS MAP
- **TAB**       - TOGGLE SIDE PANEL
- Double click the top or ALT + F4 to exit
- Click and drag from anywhere to move the window around. 

These can be changed from the settings window.


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
![Settings Window Image](https://i.imgur.com/1WRzQ6f.png)  

You can open the settings window with CTRL+Q,  

In this window you can set various values as described. Image / Windows sizes are pixels.  

Logging S Ranks saves to a text file on your desktop called "UFHT S Log.txt" in the format shown below,  
>Sphatika	 (18.3, 23.3)	 Local: 13/01/22 2:25:04 PM | UTC: 13/01/22 3:25:04 AM

You can toggle Text-to-Speech for individual callouts for separate Rank tiers. This uses your Windows default voice.
>S Ranks: {Name} in zone.  
>A and B: {Name} nearby.

The refresh rate is how often FFXIV's memory is scanned. This affects position and hp updates of the player and nearby mobs.  

Lower this to reduce resources used. Increase for a slightly smoother experience.

Disabling the Priority mob coordinates update may also slightly improve performance.  

![Hotkey Window](https://i.imgur.com/fKs6O1v.png)  

Hotkeys can be changed in the Hotkeys Window, a modifier + key is required.  
Hotkeys can be made Global hotkeys. This means that they will activate even when another program is in focus.  

Be careful, Global hotkeys will overwrite local hotkeys - such as CTRL+S to save in most programs, will not activate while UFHT is open if you use it as a Global.  

Some hotkeys can only be used as Global hotkeys, as they would otherwise be reserved elsewhere, such as ALT+Numpad used for unicode characters: ™♣☺ê©


## Config.json

Settings changed in the settings window are saved here.  
You can edit the json file manually, but be careful.   
If you break something you can delete this file and rerun the program. It will create a new default config on startup.  

You can manually customise the hotkeys using the enums in the following links:  
https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.key   
https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.modifierkeys


# TODO (no promises):
- Replace Endwalker Map images, once Cable Monkey releases new maps.
- FATE detection, at least for Minho, Senmurv, Orghana, Special Fates. - Low Prio
- ~~SS Maps showing location of minions - low prio~~
- ~~Settings Page~~
  - ~~Customisable Hotkeys~~
  - Customisable text font / colours / weight / etc - REALLY LOW PRIO
  - ~~Resize Window - maintaining aspect ratio~~
  - ~~Resizable Icons~~
  - ~~Customisable opacity~~
  - ~~Save and load settings~~
  - ~~Click Through~~
  - ~~Global Hotkeys~~
  - ~~Refresh Rate~~
- 'Record' and save a list of mob info for 'hunt trains' - Low Prio
- ~~TTS (suggest using triggernometry tbh)~~
- ACT plugin version
- ..tbd


### Credits and other stuff
This project uses [Sharlayan](https://github.com/FFXIVAPP/sharlayan) to read the game memory, and the map images were created by [Cable Monkey of Goblin](http://cablemonkey.us/huntmap2/).

All other imagery used (mob icons, backgrounds images, some maps) are copyright of Square Enix.

This is a personal project created to practice WPF, personal learning, and to create something useful to myself.
