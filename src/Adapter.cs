using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class Adapter : BluezObject
{
    private const string Interface = BluezConstants.AdapterInterface;
    public event EventHandler<Device>? DeviceAdded;
    private IDisposable? _propertyWatcher;

    private Adapter(BluezService service, ObjectPath path, ObjectManager objectManager) : base(service, path)
    {
    }

    public static async Task<Adapter> CreateAsync(BluezService service, ObjectPath path, ObjectManager objectManager)
    {
        var adapter = new Adapter(service, path, objectManager);
        adapter._propertyWatcher = await objectManager.WatchInterfacesAddedAsync(adapter.OnPropertyChanges);

        return adapter;
    }

    private void OnPropertyChanges(Exception? ex, (ObjectPath objectPath, Dictionary<string, Dictionary<string, VariantValue>> interfaces) args)
    {
        if (ex != null)
        {
            throw ex;
        }

        if(BluezManager.IsMatch(BluezConstants.DeviceInterface, args.objectPath, args.interfaces.Keys, Path))
            DeviceAdded?.Invoke(this, new Device(Service, args.objectPath));           
    }
     
    public Task StartDiscoveryAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "StartDiscovery");
            return writer.CreateMessage();
        }
    }

    public Task SetDiscoveryFilterAsync(Dictionary<string, VariantValue> properties)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "a{sv}",
                member: "SetDiscoveryFilter");
            writer.WriteDictionary(properties);
            return writer.CreateMessage();
        }
    }

    public Task StopDiscoveryAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "StopDiscovery");
            return writer.CreateMessage();
        }
    }

    public Task RemoveDeviceAsync(ObjectPath device)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "o",
                member: "RemoveDevice");
            writer.WriteObjectPath(device);
            return writer.CreateMessage();
        }
    }

    public Task<string[]> GetDiscoveryFiltersAsync()
    {
        return Connection.CallMethodAsync(CreateMessage(), (m, s) => ReadMessage_as(m, (BluezObject)s!), this);

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "GetDiscoveryFilters");
            return writer.CreateMessage();
        }
    }

    public Task SetAliasAsync(string value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("Alias");
            writer.WriteSignature("s");
            writer.WriteString(value);
            return writer.CreateMessage();
        }
    }

    public Task SetConnectableAsync(bool value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("Connectable");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task SetPoweredAsync(bool value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("Powered");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task SetDiscoverableAsync(bool value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("Discoverable");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task SetDiscoverableTimeoutAsync(uint value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("DiscoverableTimeout");
            writer.WriteSignature("u");
            writer.WriteUInt32(value);
            return writer.CreateMessage();
        }
    }

    public Task SetPairableAsync(bool value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("Pairable");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task SetPairableTimeoutAsync(uint value)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: "org.freedesktop.DBus.Properties",
                signature: "ssv",
                member: "Set");
            writer.WriteString(Interface);
            writer.WriteString("PairableTimeout");
            writer.WriteSignature("u");
            writer.WriteUInt32(value);
            return writer.CreateMessage();
        }
    }

    public Task<string> GetAddressAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Address"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string> GetAddressTypeAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "AddressType"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string> GetNameAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Name"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string> GetAliasAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Alias"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<uint> GetClassAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Class"), (m, s) => ReadMessage_v_u(m, (BluezObject)s!), this);

    public Task<bool> GetConnectableAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Connectable"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<bool> GetPoweredAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Powered"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<string> GetPowerStateAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "PowerState"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<bool> GetDiscoverableAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Discoverable"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<uint> GetDiscoverableTimeoutAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "DiscoverableTimeout"), (m, s) => ReadMessage_v_u(m, (BluezObject)s!), this);

    public Task<bool> GetPairableAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Pairable"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<uint> GetPairableTimeoutAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "PairableTimeout"), (m, s) => ReadMessage_v_u(m, (BluezObject)s!), this);

    public Task<bool> GetDiscoveringAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Discovering"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<string[]> GetUUIDsAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "UUIDs"), (m, s) => ReadMessage_v_as(m, (BluezObject)s!), this);

    public Task<string> GetModaliasAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Modalias"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string[]> GetRolesAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Roles"), (m, s) => ReadMessage_v_as(m, (BluezObject)s!), this);

    public Task<string[]> GetExperimentalFeaturesAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "ExperimentalFeatures"), (m, s) => ReadMessage_v_as(m, (BluezObject)s!), this);

    public Task<ushort> GetManufacturerAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Manufacturer"), (m, s) => ReadMessage_v_q(m, (BluezObject)s!), this);

    public Task<byte> GetVersionAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Version"), (m, s) => ReadMessage_v_y(m, (BluezObject)s!), this);

    public Task<AdapterProperties> GetPropertiesAsync()
    {
        return Connection.CallMethodAsync(CreateGetAllPropertiesMessage(Interface), (m, _) => ReadMessage(m), this);

        static AdapterProperties ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }

    private ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<AdapterProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(Interface, (m, _) => ReadMessage(m), handler, emitOnCapturedContext, flags);

        static PropertyChanges<AdapterProperties> ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            reader.ReadString();
            List<string> changed = [];
            return new PropertyChanges<AdapterProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
        }

        static string[] ReadInvalidated(ref Reader reader)
        {
            List<string>? invalidated = null;
            var arrayEnd = reader.ReadArrayStart(DBusType.String);
            while (reader.HasNext(arrayEnd))
            {
                invalidated ??= new();
                var property = reader.ReadString();
                switch (property)
                {
                    case "Address": invalidated.Add("Address"); break;
                    case "AddressType": invalidated.Add("AddressType"); break;
                    case "Name": invalidated.Add("Name"); break;
                    case "Alias": invalidated.Add("Alias"); break;
                    case "Class": invalidated.Add("Class"); break;
                    case "Connectable": invalidated.Add("Connectable"); break;
                    case "Powered": invalidated.Add("Powered"); break;
                    case "PowerState": invalidated.Add("PowerState"); break;
                    case "Discoverable": invalidated.Add("Discoverable"); break;
                    case "DiscoverableTimeout": invalidated.Add("DiscoverableTimeout"); break;
                    case "Pairable": invalidated.Add("Pairable"); break;
                    case "PairableTimeout": invalidated.Add("PairableTimeout"); break;
                    case "Discovering": invalidated.Add("Discovering"); break;
                    case "UUIDs": invalidated.Add("UUIDs"); break;
                    case "Modalias": invalidated.Add("Modalias"); break;
                    case "Roles": invalidated.Add("Roles"); break;
                    case "ExperimentalFeatures": invalidated.Add("ExperimentalFeatures"); break;
                    case "Manufacturer": invalidated.Add("Manufacturer"); break;
                    case "Version": invalidated.Add("Version"); break;
                }
            }

            return invalidated?.ToArray() ?? [];
        }
    }

    private static AdapterProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new AdapterProperties();
        var arrayEnd = reader.ReadArrayStart(DBusType.Struct);
        while (reader.HasNext(arrayEnd))
        {
            var property = reader.ReadString();
            switch (property)
            {
                case "Address":
                    reader.ReadSignature("s"u8);
                    props.Address = reader.ReadString();
                    changedList?.Add("Address");
                    break;
                case "AddressType":
                    reader.ReadSignature("s"u8);
                    props.AddressType = reader.ReadString();
                    changedList?.Add("AddressType");
                    break;
                case "Name":
                    reader.ReadSignature("s"u8);
                    props.Name = reader.ReadString();
                    changedList?.Add("Name");
                    break;
                case "Alias":
                    reader.ReadSignature("s"u8);
                    props.Alias = reader.ReadString();
                    changedList?.Add("Alias");
                    break;
                case "Class":
                    reader.ReadSignature("u"u8);
                    props.Class = reader.ReadUInt32();
                    changedList?.Add("Class");
                    break;
                case "Connectable":
                    reader.ReadSignature("b"u8);
                    props.Connectable = reader.ReadBool();
                    changedList?.Add("Connectable");
                    break;
                case "Powered":
                    reader.ReadSignature("b"u8);
                    props.Powered = reader.ReadBool();
                    changedList?.Add("Powered");
                    break;
                case "PowerState":
                    reader.ReadSignature("s"u8);
                    props.PowerState = reader.ReadString();
                    changedList?.Add("PowerState");
                    break;
                case "Discoverable":
                    reader.ReadSignature("b"u8);
                    props.Discoverable = reader.ReadBool();
                    changedList?.Add("Discoverable");
                    break;
                case "DiscoverableTimeout":
                    reader.ReadSignature("u"u8);
                    props.DiscoverableTimeout = reader.ReadUInt32();
                    changedList?.Add("DiscoverableTimeout");
                    break;
                case "Pairable":
                    reader.ReadSignature("b"u8);
                    props.Pairable = reader.ReadBool();
                    changedList?.Add("Pairable");
                    break;
                case "PairableTimeout":
                    reader.ReadSignature("u"u8);
                    props.PairableTimeout = reader.ReadUInt32();
                    changedList?.Add("PairableTimeout");
                    break;
                case "Discovering":
                    reader.ReadSignature("b"u8);
                    props.Discovering = reader.ReadBool();
                    changedList?.Add("Discovering");
                    break;
                case "UUIDs":
                    reader.ReadSignature("as"u8);
                    props.UUIDs = reader.ReadArrayOfString();
                    changedList?.Add("UUIDs");
                    break;
                case "Modalias":
                    reader.ReadSignature("s"u8);
                    props.Modalias = reader.ReadString();
                    changedList?.Add("Modalias");
                    break;
                case "Roles":
                    reader.ReadSignature("as"u8);
                    props.Roles = reader.ReadArrayOfString();
                    changedList?.Add("Roles");
                    break;
                case "ExperimentalFeatures":
                    reader.ReadSignature("as"u8);
                    props.ExperimentalFeatures = reader.ReadArrayOfString();
                    changedList?.Add("ExperimentalFeatures");
                    break;
                case "Manufacturer":
                    reader.ReadSignature("q"u8);
                    props.Manufacturer = reader.ReadUInt16();
                    changedList?.Add("Manufacturer");
                    break;
                case "Version":
                    reader.ReadSignature("y"u8);
                    props.Version = reader.ReadByte();
                    changedList?.Add("Version");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }

        return props;
    }
}