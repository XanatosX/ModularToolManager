using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Services.IO;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;
internal partial class HotkeysViewModel : ObservableObject
{
	[ObservableProperty]
	private List<SingleHotkeyViewModel>? hotkeys;

	public HotkeysViewModel(GetApplicationInformationService applicationInformationService)
	{
		Hotkeys = applicationInformationService.GetHotkeys()
										 .Select(hotkey => new SingleHotkeyViewModel(hotkey))
										 .ToList();
	}
}
