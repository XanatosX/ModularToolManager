using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class ApplicationStyleViewModel : ObservableObject
{
    private readonly ApplicationStyle applicationStyle;

    public int Id => applicationStyle.Id;

    [ObservableProperty]
    private string displayName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TooltipShowTime))]
    private string description;

    public int TooltipShowTime => string.IsNullOrEmpty(description) ? int.MaxValue : 500;

    public ApplicationStyleViewModel(ApplicationStyle applicationStyle)
    {
        this.applicationStyle = applicationStyle;
        DisplayName = applicationStyle.Name ?? string.Empty;
        description = applicationStyle.Description ?? string.Empty;
    }
}
