using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManager.Models;
using ModularToolManagerModel.Services.IO;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to show a single dependency
/// </summary>
internal partial class DependencyViewModel : ObservableObject
{
	/// <summary>
	/// The dependency to show
	/// </summary>
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Name))]
	[NotifyPropertyChangedFor(nameof(Version))]
	[NotifyPropertyChangedFor(nameof(ProjectUrl))]
	[NotifyPropertyChangedFor(nameof(LicenseUrl))]
	[NotifyPropertyChangedFor(nameof(ProjectSet))]
	[NotifyPropertyChangedFor(nameof(LicenseSet))]
	private DependencyModel? dependency;


	/// <summary>
	/// The service used to open an url
	/// </summary>
	private readonly IUrlOpenerService urlOpenerService;

	/// <summary>
	/// The name of the dependency
	/// </summary>
	public string? Name => dependency?.Name;

	/// <summary>
	/// The version of the dependency
	/// </summary>
	public string? Version => dependency?.Version;

	/// <summary>
	/// The url to the project of the dependency
	/// </summary>
	public string? ProjectUrl => dependency?.ProjectUrl;

	/// <summary>
	/// Is the project url set
	/// </summary>
	public bool ProjectSet => !string.IsNullOrEmpty(ProjectUrl);

	/// <summary>
	/// The url to the license of the dependency
	/// </summary>
	public string? LicenseUrl => dependency?.LicenseUrl;

	/// <summary>
	/// is the license for the dependency set
	/// </summary>
	public bool LicenseSet => !string.IsNullOrEmpty(LicenseUrl);


	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="dependency">The depencency to show</param>
	/// <param name="urlOpenerService">The service used to open the url</param>
	public DependencyViewModel(DependencyModel dependency, IUrlOpenerService urlOpenerService)
	{
		Dependency = dependency;
		this.urlOpenerService = urlOpenerService;
	}

	/// <summary>
	/// Command to open a given url
	/// </summary>
	/// <param name="url">The url to open</param>
	[RelayCommand]
	public void OpenUrl(string url)
	{
		urlOpenerService.OpenUrl(url);
	}
}
