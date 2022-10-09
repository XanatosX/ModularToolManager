using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class BoolPluginSettingViewModel : ObservableObject, IPluginSettingModel
{
    private readonly SettingModel storedModel;

    public string DisplayName => $"{TranslationKey}:";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    private string translationKey;

    [ObservableProperty]
    private bool isChecked;

    public BoolPluginSettingViewModel(SettingModel settingModel)
    {
        storedModel = settingModel;
        TranslationKey = settingModel.DisplayName ?? string.Empty;
        IsChecked = settingModel.GetData<bool>();
    }

    public SettingModel GetSettingsModel()
    {
        storedModel.SetValue(IsChecked);
        return storedModel;
    }
}
