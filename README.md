# Alisa mods

Bug fixes and new features for the video game **Alisa** ([Steam](https://store.steampowered.com/app/1335530/Alisa/), [itch.io](https://caspercroes.itch.io/alisa), [GOG](https://www.gog.com/en/game/alisa)).

See [Issues](../../issues) for a detailed list of every changes.

## Warning
Compiled and tested only with the demo version 'cause my poor ass is too broke to buy games at the moment, but it _could_ work with the full version as well, or if you recompile the project with the *Assembly-CSharp.dll* file from the full version, it _should_ work as-is or with minimal changes.

## How to play
1. Download the [latest x86 version of BepInEx 5](https://github.com/BepInEx/BepInEx/releases) (e.g., [v5.4.23.2](https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x86_5.4.23.2.zip))
2. Extract its contents to your game root folder (e.g., `C:\Program Files (x86)\Steam\steamapps\common\Alisa Demo (Windows)\`)
3. Download the latest version of the mod from the [releases](https://github.com/TakoNekko/Alisa-Mods/releases) page
4. Extract its contents to your game root folder
5. If all done correctly, you should now have a `\Alisa Demo (Windows)\BepInEx\plugins\BepInEx5Plugins.Ash.Alisa.Closet\` subfolder
6. Start the game

## How to compile (for developers)
1. Copy all the files inside the game's `Alisa_Data/Managed` folder to the project's `lib` folder
2. Open the solution or project and let NuGet Package Manager restore the dependencies (or edit the project files and link directly to the files you copied into the `lib` folder)
3. Compile
