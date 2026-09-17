using System.IO;
using System.Reflection;
using System.Resources;

namespace CosmoDreadCoop.Utils;

public static class EmbeddedResource
{
    public static byte[] LoadBytesFromAssembly(Assembly assembly, string name)
    {
		using Stream str = assembly.GetManifestResourceStream(name) ?? throw new MissingManifestResourceException($"Could not find entry {name} in manifest");
		using MemoryStream memoryStream = new();

        str.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }

    public static Assembly LoadAssemblyFromAssembly(Assembly assembly, string name)
    {
        var rawAssembly = LoadBytesFromAssembly(assembly, name);

        if (rawAssembly == null)
        {
            return null;
        }

        return Assembly.Load(rawAssembly);
    }
}