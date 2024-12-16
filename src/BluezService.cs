using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public class BluezService(Connection connection, string destination)
{
    public Connection Connection { get; } = connection;
    public string Destination { get; } = destination;

    public ObjectManager CreateObjectManager(ObjectPath path) => new(this, path);
    public async Task<Adapter> CreateAdapterAsync(ObjectPath path, ObjectManager objectManager) => await Adapter.CreateAsync(this, path, objectManager);
    public Device CreateDevice(ObjectPath path) => new(this, path);
    public GattService CreateGattService(ObjectPath path) => new(this, path);
    public async Task<GattCharacteristic> CreateGattCharacteristicAsync(ObjectPath path) => await GattCharacteristic.CreateAsync(this, path);
}