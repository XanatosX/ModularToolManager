namespace ModularToolManagerPlugin.Attributes;

/// <summary>
/// Attribute allowing interfaces to be injected into plugins in the constructor
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class PluginInjectableAttribute : Attribute
{
}
