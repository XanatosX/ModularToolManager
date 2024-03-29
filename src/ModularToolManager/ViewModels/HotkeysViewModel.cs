﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.IO;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// The view model for all the possible hot keys
/// </summary>
internal partial class HotkeysViewModel : ObservableObject
{
	/// <summary>
	/// A list with all the hot keys
	/// </summary>
	[ObservableProperty]
	private List<SingleHotkeyViewModel>? hotkeys;

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="applicationInformationService">The service used to load the hotkeys</param>
	public HotkeysViewModel(GetApplicationInformationService applicationInformationService)
	{
		Hotkeys = applicationInformationService.GetHotkeys()
										 .OrderBy(hotkey => hotkey.OrderId)
										 .Select(hotkey => new SingleHotkeyViewModel(hotkey))
										 .ToList();
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
