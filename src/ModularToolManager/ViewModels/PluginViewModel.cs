using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System;

namespace ModularToolManager.ViewModels;
internal partial class PluginViewModel : ObservableObject
{
	/// <summary>
	/// Service to use for opening urls
	/// </summary>
	private readonly IUrlOpenerService urlOpenerService;

	[ObservableProperty]
	private string name;

	[ObservableProperty]
	private string? description;

	[ObservableProperty]
	private string authors;

	[ObservableProperty]
	private string? license;

	[ObservableProperty]
	private string? url;


	public PluginViewModel(IUrlOpenerService urlOpenerService)
	{
		this.urlOpenerService = urlOpenerService;
	}

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
	[RelayCommand(CanExecute = nameof(CanExecuteOpenUrlCommand))]
	private void OpenUrlCommand()
	{
		urlOpenerService.OpenUrl(Url!);
	}

	/// <summary>
	/// Check if the url can be opened
	/// </summary>
	/// <returns>True if is a valid url</returns>
	private bool CanExecuteOpenUrlCommand()
	{
		if (string.IsNullOrEmpty(Url))
		{
			return false;
		}
		try
		{
			var uri = new Uri(Url);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

}
