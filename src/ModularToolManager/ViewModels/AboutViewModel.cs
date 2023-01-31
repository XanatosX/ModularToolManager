using CommunityToolkit.Mvvm.ComponentModel;
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
    private string gitHubUrl;

    [ObservableProperty]
    private string version;

    //@TODO: Add new view for this!
    [ObservableProperty]
    private List<DependencyViewModel> dependencies;

    private readonly GetApplicationInformationService getApplicationInformationService;

    public AboutViewModel(GetApplicationInformationService getApplicationInformationService, IDependencyResolverService dependencyResolverService)
    {
        this.getApplicationInformationService = getApplicationInformationService;
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
}
