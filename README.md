# UnityModLoader
It's a, in theory, universal mod loader for unity based games.

# How to install 
There is a few ways to install the loader. 

### Manual (mostly for linux user)

##### You need (for Linux): 
-> Wine (latest stable release available))
downloadable here https://www.winehq.org/

##### You need (once wine is installed or you use Windows)
-> DnSPY 
downloadable here : https://github.com/dnSpy/dnSpy/releases

-> Knowledge about the game you want to mod

##### Now :
-> Move UnityModLoader to the root folder of the game (Where there is the executable)

-> Open Dnspy (through Wine if you are on Linux)

-> Find a script that is loaded before most of the game (You'll need some knowledge of C# and said game to achieve it, but look for a MainMenu or something of that order)

-> Right Click on the script and look for the "Edit C#" option

-> Search for the Start() function, add it if there is none

-> press ctrl + o and add the UnityModLoader.dll 

-> at the beginning of the script, add `using UnityModLoader;`

-> in the Start Function, add `Loader.Loader.Hook();` 

-> press Compile. If there is errors linked to the game you want to mod, take the time you need to correct them. I'm assuming that you can do c# and read compilation error.

-> press ctrl + alt + s, then ok

-> install your mods in {Game}_Data\StreamingAssets\Mods, or launch the game without mods so it can create the folder.

##### coming soon :
-> Auto Injector to remove the manual injection (for Windows and Linux)

-> Library to ease the creation of mods on Unity based game

-> Mod template (done)