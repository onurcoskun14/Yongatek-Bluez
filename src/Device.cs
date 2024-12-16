using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class Device(BluezService service, ObjectPath path) : BluezObject(service, path)
{
    private const string Interface = BluezConstants.DeviceInterface;

    public Task DisconnectAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "Disconnect");
            return writer.CreateMessage();
        }
    }

    public Task ConnectAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "Connect");
            return writer.CreateMessage();
        }
    }

    public Task ConnectProfileAsync(string uUID)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "s",
                member: "ConnectProfile");
            writer.WriteString(uUID);
            return writer.CreateMessage();
        }
    }

    public Task DisconnectProfileAsync(string uUID)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "s",
                member: "DisconnectProfile");
            writer.WriteString(uUID);
            return writer.CreateMessage();
        }
    }

    public Task PairAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "Pair");
            return writer.CreateMessage();
        }
    }

    public Task CancelPairingAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "CancelPairing");
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

    public Task SetTrustedAsync(bool value)
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
            writer.WriteString("Trusted");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task SetBlockedAsync(bool value)
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
            writer.WriteString("Blocked");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task SetWakeAllowedAsync(bool value)
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
            writer.WriteString("WakeAllowed");
            writer.WriteSignature("b");
            writer.WriteBool(value);
            return writer.CreateMessage();
        }
    }

    public Task<string> GetAddressAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Address"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string> GetAddressTypeAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "AddressType"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string> GetNameAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Name"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<string> GetAliasAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Alias"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<uint> GetClassAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Class"), (m,s) => ReadMessage_v_u(m, (BluezObject)s!), this);

    public Task<ushort> GetAppearanceAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Appearance"), (m,s) => ReadMessage_v_q(m, (BluezObject)s!), this);

    public Task<string> GetIconAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Icon"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<bool> GetPairedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Paired"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<bool> GetBondedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Bonded"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<bool> GetTrustedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Trusted"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<bool> GetBlockedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Blocked"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<bool> GetLegacyPairingAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "LegacyPairing"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<short> GetRSSIAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "RSSI"), (m,s) => ReadMessage_v_n(m, (BluezObject)s!), this);

    public Task<bool> GetConnectedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Connected"), (m,s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<string[]> GetUUIDsAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "UUIDs"), (m,s) => ReadMessage_v_as(m, (BluezObject)s!), this);

    public Task<string> GetModaliasAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Modalias"), (m,s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<ObjectPath> GetAdapterAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Adapter"), (m,s) => ReadMessage_v_o(m, (BluezObject)s!), this);

    public Task<Dictionary<ushort, VariantValue>> GetManufacturerDataAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "ManufacturerData"), (m,s) => ReadMessage_v_aeqv(m, (BluezObject)s!), this);

    public Task<Dictionary<string, VariantValue>> GetServiceDataAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "ServiceData"), (m,s) => ReadMessage_v_aesv(m, (BluezObject)s!), this);

    public Task<short> GetTxPowerAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "TxPower"), (m, s) => ReadMessage_v_n(m, (BluezObject)s!), this);

    public Task<bool> GetServicesResolvedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "ServicesResolved"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<byte[]> GetAdvertisingFlagsAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "AdvertisingFlags"), (m, s) => ReadMessage_v_ay(m, (BluezObject)s!), this);

    public Task<Dictionary<byte, VariantValue>> GetAdvertisingDataAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "AdvertisingData"), (m, s) => ReadMessage_v_aeyv(m, (BluezObject)s!), this);

    public Task<bool> GetWakeAllowedAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "WakeAllowed"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<Dictionary<ObjectPath, Dictionary<string, VariantValue>>> GetSetsAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Sets"), (m, s) => ReadMessage_v_aeoaesv(m, (BluezObject)s!), this);

    public Task<DeviceProperties> GetPropertiesAsync()
    {
        return Connection.CallMethodAsync(CreateGetAllPropertiesMessage(Interface), (m, _) => ReadMessage(m), this);

        static DeviceProperties ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }

    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<DeviceProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(Interface, (m, _) => ReadMessage(m), handler, emitOnCapturedContext, flags);

        static PropertyChanges<DeviceProperties> ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            reader.ReadString();
            List<string> changed = [];
            return new PropertyChanges<DeviceProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Appearance": invalidated.Add("Appearance"); break;
                    case "Icon": invalidated.Add("Icon"); break;
                    case "Paired": invalidated.Add("Paired"); break;
                    case "Bonded": invalidated.Add("Bonded"); break;
                    case "Trusted": invalidated.Add("Trusted"); break;
                    case "Blocked": invalidated.Add("Blocked"); break;
                    case "LegacyPairing": invalidated.Add("LegacyPairing"); break;
                    case "RSSI": invalidated.Add("RSSI"); break;
                    case "Connected": invalidated.Add("Connected"); break;
                    case "UUIDs": invalidated.Add("UUIDs"); break;
                    case "Modalias": invalidated.Add("Modalias"); break;
                    case "Adapter": invalidated.Add("Adapter"); break;
                    case "ManufacturerData": invalidated.Add("ManufacturerData"); break;
                    case "ServiceData": invalidated.Add("ServiceData"); break;
                    case "TxPower": invalidated.Add("TxPower"); break;
                    case "ServicesResolved": invalidated.Add("ServicesResolved"); break;
                    case "AdvertisingFlags": invalidated.Add("AdvertisingFlags"); break;
                    case "AdvertisingData": invalidated.Add("AdvertisingData"); break;
                    case "WakeAllowed": invalidated.Add("WakeAllowed"); break;
                    case "Sets": invalidated.Add("Sets"); break;
                }
            }

            return invalidated?.ToArray() ?? [];
        }
    }

    private static DeviceProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new DeviceProperties();
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
                case "Appearance":
                    reader.ReadSignature("q"u8);
                    props.Appearance = reader.ReadUInt16();
                    changedList?.Add("Appearance");
                    break;
                case "Icon":
                    reader.ReadSignature("s"u8);
                    props.Icon = reader.ReadString();
                    changedList?.Add("Icon");
                    break;
                case "Paired":
                    reader.ReadSignature("b"u8);
                    props.Paired = reader.ReadBool();
                    changedList?.Add("Paired");
                    break;
                case "Bonded":
                    reader.ReadSignature("b"u8);
                    props.Bonded = reader.ReadBool();
                    changedList?.Add("Bonded");
                    break;
                case "Trusted":
                    reader.ReadSignature("b"u8);
                    props.Trusted = reader.ReadBool();
                    changedList?.Add("Trusted");
                    break;
                case "Blocked":
                    reader.ReadSignature("b"u8);
                    props.Blocked = reader.ReadBool();
                    changedList?.Add("Blocked");
                    break;
                case "LegacyPairing":
                    reader.ReadSignature("b"u8);
                    props.LegacyPairing = reader.ReadBool();
                    changedList?.Add("LegacyPairing");
                    break;
                case "RSSI":
                    reader.ReadSignature("n"u8);
                    props.RSSI = reader.ReadInt16();
                    changedList?.Add("RSSI");
                    break;
                case "Connected":
                    reader.ReadSignature("b"u8);
                    props.Connected = reader.ReadBool();
                    changedList?.Add("Connected");
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
                case "Adapter":
                    reader.ReadSignature("o"u8);
                    props.Adapter = reader.ReadObjectPath();
                    changedList?.Add("Adapter");
                    break;
                case "ManufacturerData":
                    reader.ReadSignature("a{qv}"u8);
                    props.ManufacturerData = ReadType_aeqv(ref reader);
                    changedList?.Add("ManufacturerData");
                    break;
                case "ServiceData":
                    reader.ReadSignature("a{sv}"u8);
                    props.ServiceData = reader.ReadDictionaryOfStringToVariantValue();
                    changedList?.Add("ServiceData");
                    break;
                case "TxPower":
                    reader.ReadSignature("n"u8);
                    props.TxPower = reader.ReadInt16();
                    changedList?.Add("TxPower");
                    break;
                case "ServicesResolved":
                    reader.ReadSignature("b"u8);
                    props.ServicesResolved = reader.ReadBool();
                    changedList?.Add("ServicesResolved");
                    break;
                case "AdvertisingFlags":
                    reader.ReadSignature("ay"u8);
                    props.AdvertisingFlags = reader.ReadArrayOfByte();
                    changedList?.Add("AdvertisingFlags");
                    break;
                case "AdvertisingData":
                    reader.ReadSignature("a{yv}"u8);
                    props.AdvertisingData = ReadType_aeyv(ref reader);
                    changedList?.Add("AdvertisingData");
                    break;
                case "WakeAllowed":
                    reader.ReadSignature("b"u8);
                    props.WakeAllowed = reader.ReadBool();
                    changedList?.Add("WakeAllowed");
                    break;
                case "Sets":
                    reader.ReadSignature("a{oa{sv}}"u8);
                    props.Sets = ReadType_aeoaesv(ref reader);
                    changedList?.Add("Sets");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }

        return props;
    }
}