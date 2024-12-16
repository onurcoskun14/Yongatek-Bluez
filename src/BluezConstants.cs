namespace Yongatek.Bluez;

public static class BluezConstants
{
    public const string BluezServiceName = "org.bluez";
    public const string ObjectManagerPath = "/";
    public const string ObjectManagerInterface = "org.freedesktop.DBus.ObjectManager";
    public const string AdapterPathPrefix = "/org/bluez/hci";
    public const string AdapterInterface = "org.bluez.Adapter1";
    public const string DeviceInterface = "org.bluez.Device1";
    public const string GattServiceInterface = "org.bluez.GattService1";
    public const string GattCharacteristicInterface = "org.bluez.GattCharacteristic1";
}