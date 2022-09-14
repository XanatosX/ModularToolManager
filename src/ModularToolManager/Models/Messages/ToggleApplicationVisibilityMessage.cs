using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Models.Messages;
internal class ToggleApplicationVisibilityMessage : RequestMessage<bool>
{
	public bool Hide { get; init; }

	public ToggleApplicationVisibilityMessage(bool hide)
	{
		Hide = hide;
	}
}
