# a-ffxiv-hunt-tracker

### A(n) FFXIV Hunt Map / Radar

A standalone program that displays the current map and shows the players position, spawn points, and nearby **A, B, S, SS** Ranks.  

Accurately depicts the location, direction, and movement of the player and any rendered mobs.   
All at a smooooooth 166.66 refresh rate.

# How do I use it?

![GFYCAT OF THIS PROGRAM IN ACTION](https://gfycat.com/bigyoungicefish)

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
- **CTRL + A**  - ALWAYS ON TOP
- **CTRL + S**  - 70% OPACITY
- **TAB**       - TOGGLE SIDE PANEL
- Double click the top or ALT + F4 to exit
- Click and drag from anywhere to move the window around.


The default Images and Icons can be replaced in the Images folder, using the same naming scheme with the new image.

The current images and shortcuts are placeholders.


### TODO (no promises):
- Replace Endwalker Map images, once Cable Monkey releases new maps.
- FATE detection, at least for Minho, Senmurv, Orghana, Special Fates.
- Customisable shortcuts
- Customisable text font / colours / weight / etc
- Resize Window - maintaining aspect ration
- Customisable opacity 
- Save and load settings
- TTS (suggest using triggernometry tbh)
- ACT plugin


### Credits and other stuff
This project uses [Sharlayan](https://github.com/FFXIVAPP/sharlayan) to read the game memory, and the map images were created by [Cable Monkey of Goblin](http://cablemonkey.us/huntmap2/).

All other imagery used (mob icons, backgrounds images, some maps) are copyright of Square Enix.

This is a personal project created to practice WPF and to create something useful to myself.
