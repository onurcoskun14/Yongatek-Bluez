namespace Yongatek.Bluez;

public record AdapterProperties
{
    public string Address { get; set; } = null!;
    public string AddressType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Alias { get; set; } = null!;
    public uint Class { get; set; }
    public bool Connectable { get; set; }
    public bool Powered { get; set; }
    public string PowerState { get; set; } = null!;
    public bool Discoverable { get; set; }
    public uint DiscoverableTimeout { get; set; }
    public bool Pairable { get; set; }
    public uint PairableTimeout { get; set; }
    public bool Discovering { get; set; }
    public string[] UUIDs { get; set; } = null!;
    public string Modalias { get; set; } = null!;
    public string[] Roles { get; set; } = null!;
    public string[] ExperimentalFeatures { get; set; } = null!;
    public ushort Manufacturer { get; set; }
    public byte Version { get; set; }
}