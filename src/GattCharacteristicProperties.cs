using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public record GattCharacteristicProperties
{
    public ushort Handle { get; set; }
    public string UUID { get; set; } = null!;
    public ObjectPath Service { get; set; }
    public byte[] Value { get; set; } = null!;
    public bool Notifying { get; set; }
    public string[] Flags { get; set; } = null!;
    public bool WriteAcquired { get; set; }
    public bool NotifyAcquired { get; set; }
    public ushort MTU { get; set; }
}