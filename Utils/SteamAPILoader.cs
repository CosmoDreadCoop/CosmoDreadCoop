using System;
using System.IO;
using System.Reflection;
using UnityEngine;

// I'm not gonna lie I shamelessly copied all of this from BoneLabs Fusion

namespace CosmoDreadCoop.Utils;

public static class SteamAPILoader
{
	public static bool Loaded => _loaded;
	
	private static bool _loaded = false;
	private static IntPtr _libraryPtr;

	public static void LoadSteamAPI()
    {
        if (Loaded) { return; }

        string apiPath = PersistentData.GetPath($"{Steamworks.Platform.LibraryName}.dll");
        ExtractAPI(apiPath, false);

        if (TryLoadAPI(apiPath, out var libraryPtr, out var errorCode))
        {
            OnLoadAPI(libraryPtr);
        }
        // 193 is a corrupted file
        else if (errorCode == 193)
        {
            CosmoLogger.LogError("SteamAPI was corrupted, attempting re-extraction...");

            ExtractAPI(apiPath, true);

            if (TryLoadAPI(apiPath, out libraryPtr, out _))
            {
                OnLoadAPI(libraryPtr);
            }
        }
    }

    public static void FreeSteamAPI()
    {
        // Don't unload it if it isn't loaded
        if (!Loaded)
            return;

        DllTools.FreeLibrary(_libraryPtr);

        _loaded = false;
    }

    private static void ExtractAPI(string path, bool overwrite = false)
    {
        if (!File.Exists(path) || overwrite)
        {
            File.WriteAllBytes(path, EmbeddedResource.LoadBytesFromAssembly(Assembly.GetExecutingAssembly(), ResourcePaths.SteamAPI));
        }
    }

    private static bool TryLoadAPI(string path, out IntPtr libraryPtr, out uint errorCode)
    {
        libraryPtr = DllTools.LoadLibrary(path);

        if (libraryPtr != IntPtr.Zero)
        {
			errorCode = 0;
            return true;
        }
        else
        {
            errorCode = DllTools.GetLastError();
            return false;
        }
    }

    private static void OnLoadAPI(IntPtr libraryPtr)
    {
        _libraryPtr = libraryPtr;
        _loaded = true;
		CosmoLogger.LogInfo("SteamAPI successfully loaded!");
    }
}