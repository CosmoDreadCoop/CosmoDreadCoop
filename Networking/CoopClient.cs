using CosmoDreadCoop.Networking.Steamworks;
using Steamworks;

namespace CosmoDreadCoop.Networking;

internal sealed class CoopClient : CoopConnection
{
	public override bool IsHost => false;

	private MyConnectionManager connection;

	public CoopClient(SteamId address)
	{
		connection = SteamNetworkingSockets.ConnectRelay<MyConnectionManager>(address);
	}

	protected override bool Disconnect()
	{
		connection.Close();
		return true;
	}
}