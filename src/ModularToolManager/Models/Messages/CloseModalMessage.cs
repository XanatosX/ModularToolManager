using Avalonia.Data.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Models.Messages;
internal class CloseModalMessage : RequestMessage<bool>
{
	public ObservableObject ModalToClose { get; init; }

	public CloseModalMessage(ObservableObject objectToClose)
	{
		ModalToClose = objectToClose;
	}

}
