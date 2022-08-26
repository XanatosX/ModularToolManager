using ModularToolManagerPlugin.Services;

namespace ModularToolManager.Services.Language;

/// <summary>
/// Factory to create translation services
/// </summary>
internal interface IPluginTranslationFactoryService
{
    /// <summary>
    /// Create a new translation service
    /// </summary>
    /// <returns>A new useable translation service</returns>
    IPluginTranslationService CreatePluginTranslationService();
}
