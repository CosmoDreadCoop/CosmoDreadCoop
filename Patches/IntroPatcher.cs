using CosmoDreadCoop.Components;
using HarmonyLib;
using UnityEngine;
using VRInteraction.UI;

namespace CosmoDreadCoop.Patches;

public static class IntroPatcher
{
	private static bool _patched = false;
	public static void Patch()
	{
		if(_patched) { return; }

		_patched = true;
		Harmony.CreateAndPatchAll(typeof(IntroPatcher));
	}

	[HarmonyPatch(typeof(Intro_GameManager), nameof(Intro_GameManager.InitScene))]
	[HarmonyPostfix]
	static void InitLobbyUI()
	{
		GameObject consoleObject = GameObject.Find("info_console");
		GameObject consoleCanvas = consoleObject.transform.Find("info_ui").gameObject;
		GameObject consoleUi = consoleCanvas.transform.Find("ui").gameObject;
		foreach(Transform child in consoleUi.transform)
		{
			child.gameObject.SetActive(false);
		}

		consoleCanvas.AddComponent<VRRaycaster>();
		consoleCanvas.AddComponent<VRCanvasInteractable>();

		GameObject lobbyElement = new("lobby_ui");
		lobbyElement.transform.SetParent(consoleUi.transform);
		lobbyElement.transform.localPosition = Vector3.zero;
		lobbyElement.transform.localRotation = Quaternion.identity;
		lobbyElement.transform.localScale = Vector3.one;
		
		var lobbyComponent = lobbyElement.AddComponent<UIIntroLobby>();
	}
}