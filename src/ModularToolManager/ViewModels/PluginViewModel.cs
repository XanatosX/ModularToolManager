using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View used to show a single plugin information data
/// </summary>
internal partial class PluginViewModel : ObservableObject
{
	/// <summary>
	/// Service to use for opening urls
	/// </summary>
	private readonly IUrlOpenerService urlOpenerService;

	/// <summary>
	/// The name of the plugin
	/// </summary>
	[ObservableProperty]
	private string name;

	/// <summary>
	/// The description of the plugin
	/// </summary>
	[ObservableProperty]
	private string? description;

	/// <summary>
	/// All the authors of the plugin
	/// </summary>
	[ObservableProperty]
	private string authors;

	/// <summary>
	/// The license of the plugin
	/// </summary>
	[ObservableProperty]
	private string? license;

	/// <summary>
	/// The url of the plugin
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(OpenProjectUrlCommand))]
	private string? url;

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="urlOpenerService">The service used to open up the url</param>
	public PluginViewModel(IUrlOpenerService urlOpenerService)
	{
		this.urlOpenerService = urlOpenerService;
	}

	/// <summary>
	/// Set the plugin for the view
	/// </summary>
	/// <param name="plugin">The plugin to display</param>
	public void SetPlugin(IFunctionPlugin plugin)
	{
		PluginInformation pluginInformation = plugin.GetPluginInformation();
		Authors = string.Join(", ", pluginInformation.Authors);
		Description = pluginInformation.Description;
		Name = plugin.GetDisplayName();
		License = pluginInformation.License;
		Url = pluginInformation.ProjectUrl;
	}

	/// <summary>
	/// Command to open the provided url
	/// </summary>
	[RelayCommand(CanExecute = nameof(CanExecuteOpenProjectUrl))]
	private void OpenProjectUrl()
	{
		urlOpenerService?.OpenUrl(Url!);
	}

	/// <summary>
	/// Can the project url be opened
	/// </summary>
	/// <returns></returns>
	private bool CanExecuteOpenProjectUrl()
	{
		try
		{
			Uri uri = new Uri(Url ?? string.Empty);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

}
