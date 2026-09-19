using System;
using CosmoDreadCoop.Networking;
using Steamworks;
using Steamworks.Data;

namespace CosmoDreadCoop.Components;

public sealed class LobbyManager : Singleton<LobbyManager>
{
	public bool HasConnection => _connection is not null;

	private CoopConnection _connection;
	private Lobby _lobby;

	public static Action<Lobby> OnLobbyLeft;

	public async void CreateLobby()
	{
		if(HasConnection)
		{
			return;
		}
		
		_connection = new CoopServer();
		Lobby? lob = await SteamMatchmaking.CreateLobbyAsync(Plugin.MAX_PLAYERS);
		if(lob is null)
		{
			LeaveLobby(); // Shuts down the connection
		}

		_lobby = lob.Value;
		_lobby.SetGameServer(SteamClient.SteamId);
		_lobby.SetJoinable(true);
		_lobby.SetFriendsOnly();
	}

	public async void ConnectLobby(SteamId serverId)
	{
		if(HasConnection)
		{
			return;
		}
		
		_connection = new CoopClient(serverId);
		Lobby? lob = await SteamMatchmaking.JoinLobbyAsync(serverId);
		if(lob is null)
		{
			LeaveLobby(); // Shuts down the connection
		}
		_lobby = lob.Value;
	}

	public void LeaveLobby()
	{
		if(!HasConnection)
		{
			return;
		}
		OnLobbyLeft.Invoke(_lobby);
		_lobby.Leave();
		_connection.Dispose();
		_lobby = default;
		_connection = null;
	}
}