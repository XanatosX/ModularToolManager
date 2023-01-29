using ModularToolManager.ViewModels;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;

namespace ModularToolManager.Services.Ui;
internal class PluginSettingViewModelService
{
    public IPluginSettingModel? GetViewModel(SettingModel settingModel)
    {
        return settingModel.Type switch
        {
            SettingType.Boolean => new BoolPluginSettingViewModel(settingModel),
            SettingType.String => new StringPluginSettingViewModel(settingModel),
            SettingType.Float => new FloatPluginSettingViewModel(settingModel),
            SettingType.Int => new IntPluginSettingViewModel(settingModel),
            _ => null
        };
    }
}
