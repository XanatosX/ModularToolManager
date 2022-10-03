using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class BoolPluginSettingViewModel : ObservableObject
{
    private readonly SettingModel storedModel;

    [ObservableProperty]
    private string displayName;

    [ObservableProperty]
    private bool isChecked;

    public BoolPluginSettingViewModel(SettingModel settingModel)
    {
        storedModel = settingModel;
        DisplayName = settingModel.DisplayName ?? string.Empty;
        IsChecked = settingModel.GetData<bool>();
    }
}
