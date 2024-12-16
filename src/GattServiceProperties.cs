using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public record GattServiceProperties
{
    public ushort Handle { get; set; } = 0!;
    public string UUID { get; set; } = null!;
    public ObjectPath Device { get; set; } = default!;
    public bool Primary { get; set; } = false;
    public ObjectPath[] Includes { get; set; } = null!;
}