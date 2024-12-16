namespace Yongatek.Bluez;

public class PropertyChanges<TProperties>(TProperties properties, string[] invalidated, string[] changed)
{
    public TProperties Properties { get; } = properties;
    public string[] Invalidated { get; } = invalidated;
    public string[] Changed { get; } = changed;
    public bool HasChanged(string property) => Array.IndexOf(Changed, property) != -1;
    public bool IsInvalidated(string property) => Array.IndexOf(Invalidated, property) != -1;
}