using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class GattCharacteristic : BluezObject
{
    public event EventHandler<byte[]>? ValueChanged;
    private const string Interface = BluezConstants.GattCharacteristicInterface;
    private IDisposable? _propertyWatcher;

    private GattCharacteristic(BluezService service, ObjectPath path) : base(service, path)
    {
    }

    public static async Task<GattCharacteristic> CreateAsync(BluezService service, ObjectPath path)
    {
        var characteristic = new GattCharacteristic(service, path);
        characteristic._propertyWatcher = await characteristic.WatchPropertiesChangedAsync(characteristic.OnPropertyChanges);

        return characteristic;
    }

    private void OnPropertyChanges(Exception? ex, PropertyChanges<GattCharacteristicProperties> changes)
    {
        foreach (var propertyName in changes.Changed)
        {
            switch (propertyName)
            {
                case "Value":
                    ValueChanged?.Invoke(this, changes.Properties.Value);
                    break;
            }
        }
    }

    public Task<byte[]> ReadValueAsync(Dictionary<string, VariantValue> options)
    {
        return Connection.CallMethodAsync(CreateMessage(), (m, s) => ReadMessage_ay(m, (BluezObject)s!), this);

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "a{sv}",
                member: "ReadValue");
            writer.WriteDictionary(options);
            return writer.CreateMessage();
        }
    }

    public Task WriteValueAsync(byte[] value, Dictionary<string, VariantValue> options)
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "aya{sv}",
                member: "WriteValue");
            writer.WriteArray(value);
            writer.WriteDictionary(options);
            return writer.CreateMessage();
        }
    }

    public Task<(System.Runtime.InteropServices.SafeHandle Fd, ushort Mtu)> AcquireWriteAsync(Dictionary<string, VariantValue> options)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (m, s) => ReadMessage_hq(m, (BluezObject)s!), this);

        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "a{sv}",
                member: "AcquireWrite");
            writer.WriteDictionary(options);
            return writer.CreateMessage();
        }
    }

    public Task<(System.Runtime.InteropServices.SafeHandle Fd, ushort Mtu)> AcquireNotifyAsync(Dictionary<string, VariantValue> options)
    {
        return this.Connection.CallMethodAsync(CreateMessage(), (m, s) => ReadMessage_hq(m, (BluezObject)s!), this);

        MessageBuffer CreateMessage()
        {
            var writer = this.Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                signature: "a{sv}",
                member: "AcquireNotify");
            writer.WriteDictionary(options);
            return writer.CreateMessage();
        }
    }

    public Task StartNotifyAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "StartNotify");
            return writer.CreateMessage();
        }
    }

    public Task StopNotifyAsync()
    {
        return Connection.CallMethodAsync(CreateMessage());

        MessageBuffer CreateMessage()
        {
            var writer = Connection.GetMessageWriter();
            writer.WriteMethodCallHeader(
                destination: Service.Destination,
                path: Path,
                @interface: Interface,
                member: "StopNotify");
            return writer.CreateMessage();
        }
    }

    public Task<ushort> GetHandleAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Handle"), (m, s) => ReadMessage_v_q(m, (BluezObject)s!), this);

    public Task<string> GetUUIDAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "UUID"), (m, s) => ReadMessage_v_s(m, (BluezObject)s!), this);

    public Task<ObjectPath> GetServiceAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Service"), (m, s) => ReadMessage_v_o(m, (BluezObject)s!), this);

    public Task<byte[]> GetValueAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Value"), (m, s) => ReadMessage_v_ay(m, (BluezObject)s!), this);

    public Task<bool> GetNotifyingAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Notifying"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<string[]> GetFlagsAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "Flags"), (m, s) => ReadMessage_v_as(m, (BluezObject)s!), this);

    public Task<bool> GetWriteAcquiredAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "WriteAcquired"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<bool> GetNotifyAcquiredAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "NotifyAcquired"), (m, s) => ReadMessage_v_b(m, (BluezObject)s!), this);

    public Task<ushort> GetMTUAsync()
        => Connection.CallMethodAsync(CreateGetPropertyMessage(Interface, "MTU"), (m, s) => ReadMessage_v_q(m, (BluezObject)s!), this);

    public Task<GattCharacteristicProperties> GetPropertiesAsync()
    {
        return Connection.CallMethodAsync(CreateGetAllPropertiesMessage(Interface), (m, _) => ReadMessage(m), this);

        static GattCharacteristicProperties ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            return ReadProperties(ref reader);
        }
    }

    private ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<Exception?, PropertyChanges<GattCharacteristicProperties>> handler, bool emitOnCapturedContext = true, ObserverFlags flags = ObserverFlags.None)
    {
        return base.WatchPropertiesChangedAsync(Interface, (m, _) => ReadMessage(m), handler, emitOnCapturedContext, flags);

        static PropertyChanges<GattCharacteristicProperties> ReadMessage(Message message)
        {
            var reader = message.GetBodyReader();
            reader.ReadString();
            List<string> changed = [];
            return new PropertyChanges<GattCharacteristicProperties>(ReadProperties(ref reader, changed), ReadInvalidated(ref reader), changed.ToArray());
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
                    case "Service": invalidated.Add("Service"); break;
                    case "Value": invalidated.Add("Value"); break;
                    case "Notifying": invalidated.Add("Notifying"); break;
                    case "Flags": invalidated.Add("Flags"); break;
                    case "WriteAcquired": invalidated.Add("WriteAcquired"); break;
                    case "NotifyAcquired": invalidated.Add("NotifyAcquired"); break;
                    case "MTU": invalidated.Add("MTU"); break;
                }
            }

            return invalidated?.ToArray() ?? [];
        }
    }

    private static GattCharacteristicProperties ReadProperties(ref Reader reader, List<string>? changedList = null)
    {
        var props = new GattCharacteristicProperties();
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
                case "Service":
                    reader.ReadSignature("o"u8);
                    props.Service = reader.ReadObjectPath();
                    changedList?.Add("Service");
                    break;
                case "Value":
                    reader.ReadSignature("ay"u8);
                    props.Value = reader.ReadArrayOfByte();
                    changedList?.Add("Value");
                    break;
                case "Notifying":
                    reader.ReadSignature("b"u8);
                    props.Notifying = reader.ReadBool();
                    changedList?.Add("Notifying");
                    break;
                case "Flags":
                    reader.ReadSignature("as"u8);
                    props.Flags = reader.ReadArrayOfString();
                    changedList?.Add("Flags");
                    break;
                case "WriteAcquired":
                    reader.ReadSignature("b"u8);
                    props.WriteAcquired = reader.ReadBool();
                    changedList?.Add("WriteAcquired");
                    break;
                case "NotifyAcquired":
                    reader.ReadSignature("b"u8);
                    props.NotifyAcquired = reader.ReadBool();
                    changedList?.Add("NotifyAcquired");
                    break;
                case "MTU":
                    reader.ReadSignature("q"u8);
                    props.MTU = reader.ReadUInt16();
                    changedList?.Add("MTU");
                    break;
                default:
                    reader.ReadVariantValue();
                    break;
            }
        }

        return props;
    }
}