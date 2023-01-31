using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManager.Models;
using ModularToolManagerModel.Services.IO;
using System.Data;

namespace ModularToolManager.ViewModels;
internal partial class DependencyViewModel : ObservableObject
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Name))]
	[NotifyPropertyChangedFor(nameof(Version))]
	[NotifyPropertyChangedFor(nameof(ProjectUrl))]
	[NotifyPropertyChangedFor(nameof(LicenseUrl))]
	[NotifyPropertyChangedFor(nameof(ProjectSet))]
	[NotifyPropertyChangedFor(nameof(LicenseSet))]
	private DependencyModel? dependency;
	private readonly IUrlOpenerService urlOpenerService;

	public string? Name => dependency.Name;

	public string? Version => dependency.Version;

	public string? ProjectUrl => dependency.ProjectUrl;

	public bool ProjectSet => !string.IsNullOrEmpty(ProjectUrl);

	public string? LicenseUrl => dependency.LicenseUrl;

	public bool LicenseSet => !string.IsNullOrEmpty(LicenseUrl);



	public DependencyViewModel(DependencyModel dependency, IUrlOpenerService urlOpenerService)
	{
		Dependency = dependency;
		this.urlOpenerService = urlOpenerService;
	}

	[RelayCommand]
	public void OpenUrl(string url)
	{
		urlOpenerService.OpenUrl(url);
	}
}
