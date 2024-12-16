using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class BluezObject
{
    protected BluezService Service { get; }
    public ObjectPath Path { get; }
    protected Connection Connection => Service.Connection;

    protected BluezObject(BluezService service, ObjectPath path)
        => (Service, Path) = (service, path);

    protected MessageBuffer CreateGetPropertyMessage(string @interface, string property)
    {
        var writer = this.Connection.GetMessageWriter();
        writer.WriteMethodCallHeader(
            destination: Service.Destination,
            path: Path,
            @interface: "org.freedesktop.DBus.Properties",
            signature: "ss",
            member: "Get");
        writer.WriteString(@interface);
        writer.WriteString(property);
        return writer.CreateMessage();
    }

    protected MessageBuffer CreateGetAllPropertiesMessage(string @interface)
    {
        var writer = this.Connection.GetMessageWriter();
        writer.WriteMethodCallHeader(
            destination: Service.Destination,
            path: Path,
            @interface: "org.freedesktop.DBus.Properties",
            signature: "s",
            member: "GetAll");
        writer.WriteString(@interface);
        return writer.CreateMessage();
    }

    protected ValueTask<IDisposable> WatchPropertiesChangedAsync<TProperties>(string @interface, MessageValueReader<PropertyChanges<TProperties>> reader, Action<Exception?, PropertyChanges<TProperties>> handler, bool emitOnCapturedContext, ObserverFlags flags)
    {
        var rule = new MatchRule
        {
            Type = MessageType.Signal,
            Sender = Service.Destination,
            Path = Path,
            Interface = "org.freedesktop.DBus.Properties",
            Member = "PropertiesChanged",
            Arg0 = @interface
        };
        return this.Connection.AddMatchAsync(rule, reader,
            (ex, changes, _, hs) => ((Action<Exception?, PropertyChanges<TProperties>>)hs!).Invoke(ex, changes),
            this, handler, emitOnCapturedContext, flags);
    }

    protected ValueTask<IDisposable> WatchSignalAsync<TArg>(string sender, string @interface, ObjectPath path, string signal, MessageValueReader<TArg> reader, Action<Exception?, TArg> handler, bool emitOnCapturedContext, ObserverFlags flags)
    {
        var rule = new MatchRule
        {
            Type = MessageType.Signal,
            Sender = sender,
            Path = path,
            Member = signal,
            Interface = @interface
        };
        return this.Connection.AddMatchAsync(rule, reader,
            (ex, arg, _, hs) => ((Action<Exception?, TArg>)hs!).Invoke(ex, arg),
            this, handler, emitOnCapturedContext, flags);
    }

    protected ValueTask<IDisposable> WatchSignalAsync(string sender, string @interface, ObjectPath path, string signal, Action<Exception?> handler, bool emitOnCapturedContext, ObserverFlags flags)
    {
        var rule = new MatchRule
        {
            Type = MessageType.Signal,
            Sender = sender,
            Path = path,
            Member = signal,
            Interface = @interface
        };
        return this.Connection.AddMatchAsync(rule, (_, _) => null!,
            (Exception? ex, object _, object? _, object? hs) => ((Action<Exception?>)hs!).Invoke(ex), this, handler, emitOnCapturedContext, flags);
    }

    protected static Dictionary<ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>> ReadMessage_aeoaesaesv(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        return ReadType_aeoaesaesv(ref reader);
    }

    protected static (ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>) ReadMessage_oaesaesv(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        var arg0 = reader.ReadObjectPath();
        var arg1 = ReadType_aesaesv(ref reader);
        return (arg0, arg1);
    }

    protected static (ObjectPath, string[]) ReadMessage_oas(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        var arg0 = reader.ReadObjectPath();
        var arg1 = reader.ReadArrayOfString();
        return (arg0, arg1);
    }

    protected static string[] ReadMessage_as(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        return reader.ReadArrayOfString();
    }

    protected static string ReadMessage_v_s(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("s"u8);
        return reader.ReadString();
    }

    protected static uint ReadMessage_v_u(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("u"u8);
        return reader.ReadUInt32();
    }

    protected static bool ReadMessage_v_b(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("b"u8);
        return reader.ReadBool();
    }

    protected static string[] ReadMessage_v_as(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("as"u8);
        return reader.ReadArrayOfString();
    }

    protected static ushort ReadMessage_v_q(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("q"u8);
        return reader.ReadUInt16();
    }

    protected static byte ReadMessage_v_y(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("y"u8);
        return reader.ReadByte();
    }

    protected static Dictionary<string, VariantValue> ReadMessage_v_aesv(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("a{sv}"u8);
        return reader.ReadDictionaryOfStringToVariantValue();
    }

    protected static short ReadMessage_v_n(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("n"u8);
        return reader.ReadInt16();
    }

    protected static ObjectPath ReadMessage_v_o(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("o"u8);
        return reader.ReadObjectPath();
    }

    protected static Dictionary<ushort, VariantValue> ReadMessage_v_aeqv(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("a{qv}"u8);
        return ReadType_aeqv(ref reader);
    }

    protected static byte[] ReadMessage_v_ay(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("ay"u8);
        return reader.ReadArrayOfByte();
    }

    protected static Dictionary<byte, VariantValue> ReadMessage_v_aeyv(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("a{yv}"u8);
        return ReadType_aeyv(ref reader);
    }

    protected static Dictionary<ObjectPath, Dictionary<string, VariantValue>> ReadMessage_v_aeoaesv(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("a{oa{sv}}"u8);
        return ReadType_aeoaesv(ref reader);
    }

    protected static ObjectPath[] ReadMessage_v_ao(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        reader.ReadSignature("ao"u8);
        return reader.ReadArrayOfObjectPath();
    }

    protected static byte[] ReadMessage_ay(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        return reader.ReadArrayOfByte();
    }

    protected static (System.Runtime.InteropServices.SafeHandle, ushort) ReadMessage_hq(Message message, BluezObject _)
    {
        var reader = message.GetBodyReader();
        var arg0 = reader.ReadHandle<Microsoft.Win32.SafeHandles.SafeFileHandle>();
        var arg1 = reader.ReadUInt16();
        return (arg0, arg1);
    }

    protected static Dictionary<ushort, VariantValue> ReadType_aeqv(ref Reader reader)
    {
        Dictionary<ushort, VariantValue> dictionary = new();
        var dictEnd = reader.ReadDictionaryStart();
        while (reader.HasNext(dictEnd))
        {
            var key = reader.ReadUInt16();
            var value = reader.ReadVariantValue();
            dictionary[key] = value;
        }

        return dictionary;
    }

    protected static Dictionary<byte, VariantValue> ReadType_aeyv(ref Reader reader)
    {
        Dictionary<byte, VariantValue> dictionary = new();
        var dictEnd = reader.ReadDictionaryStart();
        while (reader.HasNext(dictEnd))
        {
            var key = reader.ReadByte();
            var value = reader.ReadVariantValue();
            dictionary[key] = value;
        }

        return dictionary;
    }

    protected static Dictionary<ObjectPath, Dictionary<string, VariantValue>> ReadType_aeoaesv(ref Reader reader)
    {
        var dictionary = new Dictionary<ObjectPath, Dictionary<string, VariantValue>>();
        var dictEnd = reader.ReadDictionaryStart();
        while (reader.HasNext(dictEnd))
        {
            var key = reader.ReadObjectPath();
            var value = reader.ReadDictionaryOfStringToVariantValue();
            dictionary[key] = value;
        }

        return dictionary;
    }

    protected static Dictionary<ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>> ReadType_aeoaesaesv(ref Reader reader)
    {
        Dictionary<ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>> dictionary = new();
        ArrayEnd dictEnd = reader.ReadDictionaryStart();
        while (reader.HasNext(dictEnd))
        {
            var key = reader.ReadObjectPath();
            var value = ReadType_aesaesv(ref reader);
            dictionary[key] = value;
        }

        return dictionary;
    }

    protected static Dictionary<string, Dictionary<string, VariantValue>> ReadType_aesaesv(ref Reader reader)
    {
        Dictionary<string, Dictionary<string, VariantValue>> dictionary = new();
        ArrayEnd dictEnd = reader.ReadDictionaryStart();
        while (reader.HasNext(dictEnd))
        {
            var key = reader.ReadString();
            var value = reader.ReadDictionaryOfStringToVariantValue();
            dictionary[key] = value;
        }

        return dictionary;
    }
}