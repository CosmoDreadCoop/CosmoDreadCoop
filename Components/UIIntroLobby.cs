using System.Collections.Generic;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoDreadCoop.Components;

public class UIIntroLobby : MonoBehaviour
{
	private TextMeshProUGUI titleText;

	private TextMeshProUGUI changeText;
	private Button changeButton;

	private TextMeshProUGUI playerList;

	private bool _inLobby;

	private void Awake()
	{
		BuildTitle();
		BuildChangeButton();
		BuildPlayerList();

		SteamMatchmaking.OnLobbyMemberJoined += OnMemberChanged;
		SteamMatchmaking.OnLobbyMemberLeave += OnMemberChanged;
		SteamMatchmaking.OnLobbyMemberDisconnected += OnMemberChanged;
		SteamMatchmaking.OnLobbyMemberKicked += OnMemberKicked;
		SteamMatchmaking.OnLobbyMemberBanned += OnMemberKicked;

		SteamMatchmaking.OnLobbyEntered += OnLobbyEntered;
		SteamMatchmaking.OnLobbyCreated += OnLobbyCreated;
		LobbyManager.OnLobbyLeft += OnLobbyLeft;
		
		ChangeInLobby(false);
	}

	private void OnDestroy()
	{
		SteamMatchmaking.OnLobbyMemberJoined -= OnMemberChanged;
		SteamMatchmaking.OnLobbyMemberLeave -= OnMemberChanged;
		SteamMatchmaking.OnLobbyMemberDisconnected -= OnMemberChanged;
		SteamMatchmaking.OnLobbyMemberKicked -= OnMemberKicked;
		SteamMatchmaking.OnLobbyMemberBanned -= OnMemberKicked;

		SteamMatchmaking.OnLobbyEntered -= OnLobbyEntered;
		SteamMatchmaking.OnLobbyCreated -= OnLobbyCreated;
		LobbyManager.OnLobbyLeft -= OnLobbyLeft;
	}

	void ChangeInLobby(bool newState)
	{
		_inLobby = newState;
		if(_inLobby)
		{
			titleText.text = "In Lobby";
			changeText.text = "Leave Lobby";
		}
		else
		{
			titleText.text = "No Lobby";
			changeText.text = "Create Lobby";
		}
	}

	public void UpdatePlayerList(IEnumerable<Friend> members)
	{
		playerList.text = members.Join(delimiter: "\n");
	}

	#region Event Handlers

	void OnMemberChanged(Lobby lobby, Friend member) => UpdatePlayerList(lobby.Members);
	void OnMemberKicked(Lobby lobby, Friend member, Friend kicker) => UpdatePlayerList(lobby.Members);
	void OnLobbyEntered(Lobby lobby) => UpdatePlayerList(lobby.Members);
	void OnLobbyLeft(Lobby lobby) => UpdatePlayerList([]);
	void OnLobbyCreated(Steamworks.Result result, Lobby lobby) => UpdatePlayerList(lobby.Members);

	void OnButtonClicked()
	{
		var lobbyManager = Singleton<LobbyManager>.Get();
		if(!_inLobby) // Creating lobby
		{
			lobbyManager.CreateLobby();
		}
		else // Leave lobby
		{
			lobbyManager.LeaveLobby();
		}
		ChangeInLobby(!_inLobby);
	}

	#endregion

	#region UI Components

	void BuildTitle()
	{
		GameObject titleObject = new("title");
		titleObject.transform.SetParent(transform);
		titleObject.transform.localPosition = Vector3.zero;
		titleObject.transform.localRotation = Quaternion.identity;
		titleObject.transform.localScale = Vector3.one;
		
		var textRect = titleObject.AddComponent<RectTransform>();
		textRect.offsetMin = new Vector2(-250, 200);
		textRect.offsetMax = new Vector2(250, 250);

		titleText = titleObject.AddComponent<TextMeshProUGUI>();
		titleText.alignment = TextAlignmentOptions.Center;
	}

	void BuildChangeButton()
	{
		GameObject buttonObject = new("change");
		buttonObject.transform.SetParent(transform);
		buttonObject.transform.localPosition = Vector3.zero;
		buttonObject.transform.localRotation = Quaternion.identity;
		buttonObject.transform.localScale = Vector3.one;

		var buttonRect = buttonObject.AddComponent<RectTransform>();
		buttonRect.offsetMin = new Vector2(-125, -225);
		buttonRect.offsetMax = new Vector2(125, -175);

		changeText = buttonObject.AddComponent<TextMeshProUGUI>();
		changeText.alignment = TextAlignmentOptions.Center;
		
		changeButton = buttonObject.AddComponent<Button>();
		var buttonColors = changeButton.colors;
		buttonColors.highlightedColor = UnityEngine.Color.green;
		changeButton.colors = buttonColors;
		changeButton.onClick.AddListener(OnButtonClicked);
	}

	void BuildPlayerList()
	{
		GameObject listParent = new("player_list");
		listParent.transform.SetParent(transform);
		listParent.transform.localPosition = Vector3.zero;
		listParent.transform.localRotation = Quaternion.identity;
		listParent.transform.localScale = Vector3.one;

		var textRect = listParent.AddComponent<RectTransform>();
		textRect.offsetMin = new Vector2(-250, -175);
		textRect.offsetMax = new Vector2(250, 200);

		playerList = listParent.AddComponent<TextMeshProUGUI>();
		playerList.alignment = TextAlignmentOptions.Center;
	}

	#endregion
}