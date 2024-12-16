using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public record DeviceProperties
{
    public string Address { get; set; } = null!;
    public string AddressType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Alias { get; set; } = null!;
    public uint Class { get; set; }
    public ushort Appearance { get; set; }
    public string Icon { get; set; } = null!;
    public bool Paired { get; set; }
    public bool Bonded { get; set; }
    public bool Trusted { get; set; }
    public bool Blocked { get; set; }
    public bool LegacyPairing { get; set; }
    public short RSSI { get; set; }
    public bool Connected { get; set; }
    public string[] UUIDs { get; set; } = null!;
    public string Modalias { get; set; } = null!;
    public ObjectPath Adapter { get; set; } = default!;
    public Dictionary<ushort, VariantValue> ManufacturerData { get; set; } = null!;
    public Dictionary<string, VariantValue> ServiceData { get; set; } = null!;
    public short TxPower { get; set; }
    public bool ServicesResolved { get; set; }
    public byte[] AdvertisingFlags { get; set; } = null!;
    public Dictionary<byte, VariantValue> AdvertisingData { get; set; } = null!;
    public bool WakeAllowed { get; set; }
    public Dictionary<ObjectPath, Dictionary<string, VariantValue>> Sets { get; set; } = null!;
}