using System.IO;
using UnityEngine;

namespace CosmoDreadCoop.Utils;

public static class PersistentData
{
	public static string PersistentPath { get; private set; }

	private static bool _initialized = false;

	public static void Initialize()
	{
		if (_initialized)
			return;

		string appData = Application.persistentDataPath;
		PersistentPath = appData + ResourcePaths.AppDataFolder;

		ValidateDirectory(PersistentPath);

		_initialized = true;
	}

	public static void ValidateDirectory(string path)
	{
		if (!Directory.Exists(path))
			Directory.CreateDirectory(path);
	}

	public static string GetPath(string appended)
	{
		if (!_initialized)
		{
			Initialize();
		}

		return PersistentPath + appended;
	}
}