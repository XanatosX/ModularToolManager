using Avalonia;
using ModularToolManager.Models;
using ModularToolManager.Services.Language;
using ModularToolManager.ViewModels.Extenions;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model used to change the language
/// </summary>
public class ChangeLanguageViewModel : ViewModelBase, IModalWindowEvents
{
    /// <summary>
    /// The language service to use
    /// </summary>
    private readonly ILanguageService languageService;

    /// <summary>
    /// A list with all the cultures available
    /// </summary>
    public ObservableCollection<CultureInfoViewModel> Cultures { get; }

    /// <summary>
    /// The currently selected culture
    /// </summary>
    public CultureInfoViewModel? SelectedCulture
    {
        get => selectedCulture;
        set => this.RaiseAndSetIfChanged(ref selectedCulture, value);
    }

    /// <summary>
    /// The currently selected culture full field
    /// </summary>
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
    public event EventHandler Closing;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public ChangeLanguageViewModel()
    {
        languageService = Locator.Current.GetService<ILanguageService>();
        Cultures = new ObservableCollection<CultureInfoViewModel>(languageService.GetAvailableCultures().Select(culture => new CultureInfoViewModel(new CultureInfoModel(culture.DisplayName, culture))));

        SelectedCulture = Cultures.FirstOrDefault(cultureViewModel => cultureViewModel.Culture == CultureInfo.CurrentCulture);
        SelectedCulture = SelectedCulture is null ? Cultures.First() : SelectedCulture;

        AbortCommand = ReactiveCommand.Create(async () =>
        {
            Closing?.Invoke(this, EventArgs.Empty);
        });
    }
}
