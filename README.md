# CosmoDread Co-op
A mod that will eventually allow you to play CosmoDread with your friends.

# How the FUCK do I set this shit up
Don't. It's not done.

Otherwise, first you need to install [BepInEx 5.4.23.5](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.5) into your game, which will allow plugins to be loaded. <br>
Run the game once so that the necessary files and folders get generated.

Now you need to build the mod. Check `CosmoDreadCoop.csproj` and scroll down to the `ItemGroup` that specifies a bunch of included reference DLLs.<br>
Get those from your CosmoDread install (`CosmoDread_Data/Managed`) and put them in `lib`. This will let dotnet find the namespaces needed to compile the thing.

Once you've compiled the project, navigate to the `bin` folder and grab the `CosmoDreadCoop.dll` file that was generated, then place that into the `BepInEx/plugins` folder in your game. <br>
Done. The mod is now installed and you can play it.

# Licenses

Facepunch.Steamworks is under MIT.