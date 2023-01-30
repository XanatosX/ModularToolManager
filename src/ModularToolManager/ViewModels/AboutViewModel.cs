using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Services.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class AboutViewModel : ObservableObject
{
    [ObservableProperty]
    private string license;

    [ObservableProperty]
    private string version;

    //@TODO: Add new view for this!
    [ObservableProperty]
    private List<string> dependencies;

    private readonly GetApplicationInformationService getApplicationInformationService;

    public AboutViewModel(GetApplicationInformationService getApplicationInformationService)
    {
        this.getApplicationInformationService = getApplicationInformationService;
        License = getApplicationInformationService.GetLicense() ?? string.Empty;
        Version = getApplicationInformationService.GetVersion()?.ToString() ?? string.Empty;
        Dependencies = new();
    }
}
