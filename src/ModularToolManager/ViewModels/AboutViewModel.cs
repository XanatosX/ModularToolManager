using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Settings;
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
    /// The github project user manual url
    /// </summary>
    [ObservableProperty]
    private string? gitHubUserManualUrl;

    /// <summary>
    /// The url to open for the avalonia project
    /// </summary>
    [ObservableProperty]
    private string? avaloniaProjectUrl;

    /// <summary>
    /// String to save the app description
    /// </summary>
    [ObservableProperty]
    private string? description;

    /// <summary>
    /// Service used to open url
    /// </summary>
    private readonly IUrlOpenerService urlOpenerService;

    /// <summary>
    /// Create a new instance of this view model
    /// </summary>
    /// <param name="applicationInformationService">The service used to get application information</param>
    /// <param name="dependencyResolverService">The service used to resolve dependencies</param>
    /// <param name="urlOpenerService">The service used to open an url</param>
    public AboutViewModel(GetApplicationInformationService applicationInformationService,
                          IDependencyResolverService dependencyResolverService,
                          IUrlOpenerService urlOpenerService,
                          ResourceReaderService resourceReader,
                          ISettingsService settingsService)
    {
        this.urlOpenerService = urlOpenerService;
        License = applicationInformationService.GetLicense();
        Version = applicationInformationService.GetVersion()?.ToString();
        GitHubUrl = applicationInformationService.GetGithubUrl();
        GitHubUserManualUrl = applicationInformationService.GetGithubUserManualUrl();
        AvaloniaProjectUrl = applicationInformationService.GetAvaloniaProjectUrl();
        var appSettings = settingsService.GetApplicationSettings();
        description = resourceReader.GetResourceData("description", "md", appSettings.CurrentLanguage);
        Dependencies = applicationInformationService.GetDependencies()
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

    /// <summary>
    /// Command to use to close the modal
    /// </summary>
    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
    }
}
