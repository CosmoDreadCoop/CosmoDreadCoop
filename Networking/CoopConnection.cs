using System;
using Steamworks;
using Steamworks.Data;

namespace CosmoDreadCoop.Networking;

public abstract class CoopConnection : IDisposable
{
	public abstract bool IsHost { get; }

	protected abstract bool Disconnect();

	public void Dispose()
	{
		CosmoLogger.LogInfo("Closing connection");
		Disconnect();
	}
}