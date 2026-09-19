using System;
using CosmoDreadCoop.Networking.Steamworks;
using Steamworks;

namespace CosmoDreadCoop.Networking;

internal sealed class CoopServer : CoopConnection
{
	public override bool IsHost => true;

	private MySocketManager server;

	public CoopServer()
	{
		CosmoLogger.LogInfo($"Opening new server...");
		server = SteamNetworkingSockets.CreateRelaySocket<MySocketManager>();
		CosmoLogger.LogInfo($"Opened connection with socket {server.Socket}");
	}

	protected override bool Disconnect()
	{
		foreach(var connection in server.Connected)
		{
			connection.Close(false, 1, "Server shutting down");
		}
		server.Close();
		return true;
	}
}