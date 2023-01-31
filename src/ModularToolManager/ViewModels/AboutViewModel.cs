using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Services.IO;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.IO;
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

    [ObservableProperty]
    private List<DependencyViewModel> dependencies;

    public string GitHubUrl => Properties.Properties.GitHubUrl;

    private readonly GetApplicationInformationService getApplicationInformationService;
    private readonly IUrlOpenerService urlOpenerService;

    public AboutViewModel(GetApplicationInformationService getApplicationInformationService,
                          IDependencyResolverService dependencyResolverService,
                          IUrlOpenerService urlOpenerService)
    {
        this.getApplicationInformationService = getApplicationInformationService;
        this.urlOpenerService = urlOpenerService;
        License = getApplicationInformationService.GetLicense() ?? string.Empty;
        Version = getApplicationInformationService.GetVersion()?.ToString() ?? string.Empty;
        Dependencies = getApplicationInformationService.GetDependencies()
                                                       .OrderBy(d => d.Name)
                                                       .Select(dep => dependencyResolverService.GetDependency(provider =>
                                                       {
                                                           return new DependencyViewModel(dep, provider.GetRequiredService<IUrlOpenerService>());
                                                       }))
                                                       .OfType<DependencyViewModel>()
                                                       .ToList();
    }

    [RelayCommand]
    private void OpenUrl(string url)
    {
        urlOpenerService.OpenUrl(url);
    }
}
