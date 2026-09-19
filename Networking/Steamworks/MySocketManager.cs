using System;
using Steamworks;
using Steamworks.Data;

namespace CosmoDreadCoop.Networking.Steamworks;

internal sealed class MySocketManager : SocketManager
{
	public override void OnConnecting(Connection connection, ConnectionInfo info)
	{
		base.OnConnecting(connection, info);
	}

	public override void OnConnected(Connection connection, ConnectionInfo info)
	{
		base.OnConnected(connection, info);
	}

	public override void OnDisconnected(Connection connection, ConnectionInfo info)
	{
		base.OnDisconnected(connection, info);
	}

	public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
	{
		base.OnMessage(connection, identity, data, size, messageNum, recvTime, channel);
	}
}