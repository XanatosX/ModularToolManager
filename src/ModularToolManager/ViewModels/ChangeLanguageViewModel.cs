using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManager.ViewModels.Extenions;
using ModularToolManagerModel.Data;
using ModularToolManagerModel.Services.Language;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model used to change the language
/// </summary>
public partial class ChangeLanguageViewModel : ObservableObject, IModalWindowEvents
{
    /// <summary>
    /// The language service to use
    /// </summary>
    private readonly ILanguageService? languageService;

    /// <summary>
    /// A list with all the cultures available
    /// </summary>
    public ObservableCollection<CultureInfoViewModel> Cultures { get; }

    /// <summary>
    /// The currently selected culture full field
    /// </summary>
    [ObservableProperty]
    private CultureInfoViewModel? selectedCulture;

    /// <summary>
    /// The command to change the language
    /// </summary>
    public ICommand ChangeLanguageCommand { get; }

    /// <summary>
    /// Command to abort the window execution
    /// </summary>
    public ICommand AbortCommand { get; }

    /// <summary>
    /// Event handler if the modal is getting closed
    /// </summary>
    public event EventHandler? Closing;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public ChangeLanguageViewModel(ILanguageService? languageService)
    {
        this.languageService = languageService;
        Cultures = new ObservableCollection<CultureInfoViewModel>(languageService.GetAvailableCultures().Select(culture => new CultureInfoViewModel(new CultureInfoModel(culture.DisplayName, culture))));

        SelectedCulture = Cultures.FirstOrDefault(cultureViewModel => cultureViewModel.Culture == CultureInfo.CurrentCulture);
        SelectedCulture = SelectedCulture is null ? Cultures.First() : SelectedCulture;

        AbortCommand = new RelayCommand(() => Closing?.Invoke(this, EventArgs.Empty));
    }
}
