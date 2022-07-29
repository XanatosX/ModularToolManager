using ModularToolManagerPlugin.Services;

namespace ModularToolManager.Services.Language;

/// <summary>
/// Create new instances of the PluginTranslationService Class
/// </summary>
internal class PluginTranslationFactoryService : IPluginTranslationFactoryService
{
    /// <inheritdoc/>
    public IPluginTranslationService createPluginTranslationService() => new PluginTranslationService();
}
