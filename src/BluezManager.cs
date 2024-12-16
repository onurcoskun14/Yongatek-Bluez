using Tmds.DBus.Protocol;

namespace Yongatek.Bluez;

public static class BluezManager
{
    private static BluezService? _bluezService;
    private static ObjectManager? _objectManager;

    private static async Task CreateBluezServiceAsync()
    {
        var connection = new Connection(Address.System!);
        await connection.ConnectAsync();
        _bluezService = new BluezService(connection, BluezConstants.BluezServiceName);
        _objectManager = _bluezService.CreateObjectManager(BluezConstants.ObjectManagerPath);
    }


    public static async Task<IReadOnlyList<Adapter>> GetAdaptersAsync()
    {
        await CreateBluezServiceAsync();

        if (_bluezService == null || _objectManager == null)
            return [];

        return await Task.WhenAll((await GetObjectPathsAsync(BluezConstants.AdapterInterface, null))
            .Select(async objectPath => await _bluezService.CreateAdapterAsync(objectPath, _objectManager))
            .ToList());
    }

    public static async Task<Device?> GetDevice(this Adapter adapter, string deviceAddress)
    {
        if (_bluezService == null || _objectManager == null)
            return null;

        return (await Task.WhenAll((await GetObjectPathsAsync(BluezConstants.DeviceInterface, adapter.Path))
            .Select(async objectPath =>
            {
                var device = _bluezService.CreateDevice(objectPath);
                return string.Equals(await device.GetAddressAsync(), deviceAddress, StringComparison.OrdinalIgnoreCase) ? device : null;
            })))
            .FirstOrDefault(device => device != null);
    }

    public static async Task<GattService?> GetGattService(this Device device, string uuid)
    {
        if (_bluezService == null || _objectManager == null)
            return null;

        return (await Task.WhenAll((await GetObjectPathsAsync(BluezConstants.GattServiceInterface, device.Path))
            .Select(async objectPath =>
            {
                var gattService = _bluezService.CreateGattService(objectPath);
                return string.Equals(await gattService.GetUUIDAsync(), uuid, StringComparison.OrdinalIgnoreCase) ? gattService : null;
            })))
            .FirstOrDefault(gattService => gattService != null);
    }

    public static async Task<GattCharacteristic?> GetGattCharacteristic(this GattService gattService, string uuid)
    {
        if (_bluezService == null || _objectManager == null)
            return null;

        return (await Task.WhenAll((await GetObjectPathsAsync(BluezConstants.GattCharacteristicInterface, gattService.Path))
            .Select(async objectPath =>
            {
                var gattCharacteristic = await _bluezService.CreateGattCharacteristicAsync(objectPath);
                return string.Equals(await gattCharacteristic.GetUUIDAsync(), uuid, StringComparison.OrdinalIgnoreCase) ? gattCharacteristic : null;
            })))
            .FirstOrDefault(gattCharacteristic => gattCharacteristic != null);
    }

    private static async Task<IReadOnlyList<ObjectPath>> GetObjectPathsAsync(string interfaceName, ObjectPath? rootObjectPath)
    {

        var objects = await _objectManager!.GetManagedObjectsAsync();

        return objects
            .Where(obj => IsMatch(interfaceName, obj.Key, obj.Value.Keys, rootObjectPath))
            .Select(obj => obj.Key)
            .ToList();
    }

    internal static bool IsMatch(string interfaceName, ObjectPath objectPath, ICollection<string> interfaces, ObjectPath? rootObjectPath)
    {
        if (rootObjectPath.ToString() is not null && !objectPath.ToString().StartsWith($"{rootObjectPath}/"))
        {
            return false;
        }

        return interfaces.Contains(interfaceName);
    }
}