using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class GattService(BluezService service, ObjectPath path) : BluezObject(service, path)
{
    private const string Interface = BluezConstants.GattServiceInterface;

    public Task<ushort> GetHandleAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Handle"), (m,s) => ReadMessage_v_q(m, (BluezObject)s!), this);

    public Task<string> GetUUIDAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "UUID"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<ObjectPath> GetDeviceAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Device"), (m,s) => ReadMessage_v_o(m, (BluezObject)s!), this);

    public Task<bool> GetPrimaryAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Primary"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<ObjectPath[]> GetIncludesAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Includes"), (m,s) => ReadMessage_v_ao(m, (BluezObject)s!), this);

    public Task<GattServiceProperties> GetPropertiesAsync()
    {
        return Connection.CallMethodAsync(CreateGetAllPropertiesMessage(Interface), (m,_) => ReadMessage(m), this);

        static GattServiceProperties ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }

    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<GattServiceProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(Interface, (m,_) => ReadMessage(m), handler, emitOnCapturedContext, flags);

        static PropertyChanges<GattServiceProperties> ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            reader.ReadString();
            List<string> changed = [];
            return new PropertyChanges<GattServiceProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
        }

        static string[] ReadInvalidated(ref Reader reader)
        {
            List<string>? invalidated = null;
            var arrayEnd = reader.ReadArrayStart(DBusType.String);
            while (reader.HasNext(arrayEnd))
            {
                invalidated ??= [];
                var property = reader.ReadString();
                switch (property)
                {
                    case "Handle": invalidated.Add("Handle"); break;
                    case "UUID": invalidated.Add("UUID"); break;
                    case "Device": invalidated.Add("Device"); break;
                    case "Primary": invalidated.Add("Primary"); break;
                    case "Includes": invalidated.Add("Includes"); break;
                }
            }

            return invalidated?.ToArray() ?? [];
        }
    }

    private static GattServiceProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new GattServiceProperties();
        var arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Handle":
                    reader.ReadSignature("q"u8);
                    props.Handle = reader.ReadUInt16();
                    changedList?.Add("Handle");
                    break;
                case "UUID":
                    reader.ReadSignature("s"u8);
                    props.UUID = reader.ReadString();
                    changedList?.Add("UUID");
                    break;
                case "Device":
                    reader.ReadSignature("o"u8);
                    props.Device = reader.ReadObjectPath();
                    changedList?.Add("Device");
                    break;
                case "Primary":
                    reader.ReadSignature("b"u8);
                    props.Primary = reader.ReadBool();
                    changedList?.Add("Primary");
                    break;
                case "Includes":
                    reader.ReadSignature("ao"u8);
                    props.Includes = reader.ReadArrayOfObjectPath();
                    changedList?.Add("Includes");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }

        return props;
    }
}