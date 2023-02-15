using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Models.Messages;
internal class ApplicationThemeUpdated : ValueChangedMessage<int>
{
    public ApplicationThemeUpdated(int value) : base(value)
    {
    }
}
