using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class ObjectManager(BluezService service, ObjectPath path) : BluezObject(service, path)
{
    private const string Interface = BluezConstants.ObjectManagerInterface;

    public Task<Dictionary<ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>>> GetManagedObjectsAsync()
    {
        return Connection.CallMethodAsync(CreateMessage(), (Message m, object? s) => ReadMessage_aeoaesaesv(m, (BluezObject)s!), this);
        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "GetManagedObjects");
            return writer.CreateMessage();
        }
    }
    public ValueTask<IDisposable> WatchInterfacesAddedAsync(Action<Exception?, (ObjectPath Object, Dictionary<string, Dictionary<string, VariantValue>> Interfaces)> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, Interface, Path, "InterfacesAdded", (Message m, object? s) => ReadMessage_oaesaesv(m, (BluezObject)s!), handler, emitOnCapturedContext, flags);
    public ValueTask<IDisposable> WatchInterfacesRemovedAsync(Action<Exception?, (ObjectPath Object, string[] Interfaces)> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
        => base.WatchSignalAsync(Service.Destination, Interface, Path, "InterfacesRemoved", (Message m, object? s) => ReadMessage_oas(m, (BluezObject)s!), handler, emitOnCapturedContext, flags);
}