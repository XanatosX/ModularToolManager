using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Services.IO;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.IO;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the about view
/// </summary>
internal partial class AboutViewModel : ObservableObject
{
    /// <summary>
    /// The license string to show
    /// </summary>
    [ObservableProperty]
    private string? license;

    /// <summary>
    /// The version string to show
    /// </summary>
    [ObservableProperty]
    private string? version;

    /// <summary>
    /// The dependencies to show
    /// </summary>
    [ObservableProperty]
    private List<DependencyViewModel>? dependencies;

    /// <summary>
    /// The github project url
    /// </summary>
    [ObservableProperty]
    private string? gitHubUrl;

    /// <summary>
    /// Service used to open url
    /// </summary>
    private readonly IUrlOpenerService urlOpenerService;

    /// <summary>
    /// Create a new instance of this view model
    /// </summary>
    /// <param name="getApplicationInformationService">The service used to get application information</param>
    /// <param name="dependencyResolverService">The service used to resolve dependencies</param>
    /// <param name="urlOpenerService">The service used to open an url</param>
    public AboutViewModel(GetApplicationInformationService getApplicationInformationService,
                          IDependencyResolverService dependencyResolverService,
                          IUrlOpenerService urlOpenerService)
    {
        this.urlOpenerService = urlOpenerService;
        License = getApplicationInformationService.GetLicense();
        Version = getApplicationInformationService.GetVersion()?.ToString();
        GitHubUrl = getApplicationInformationService.GetGithubUrl();
        Dependencies = getApplicationInformationService.GetDependencies()
                                                       .OrderBy(d => d.Name)
                                                       .Select(dep => dependencyResolverService.GetDependency(provider =>
                                                       {
                                                           return new DependencyViewModel(dep, provider.GetRequiredService<IUrlOpenerService>());
                                                       }))
                                                       .OfType<DependencyViewModel>()
                                                       .ToList();
    }

    /// <summary>
    /// Command to open url in browser
    /// </summary>
    /// <param name="url">The url to open</param>
    [RelayCommand]
    private void OpenUrl(string url)
    {
        urlOpenerService.OpenUrl(url);
    }
}
