using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManagerModel.Data;
using ModularToolManagerModel.Services.Language;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model used to change the language
/// </summary>
public partial class ChangeLanguageViewModel : ObservableObject
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
    /// The currently selected culture full field
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ChangeLanguageCommand))]
    private CultureInfoViewModel? selectedCulture;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public ChangeLanguageViewModel(ILanguageService languageService)
    {
        this.languageService = languageService;
        Cultures = new ObservableCollection<CultureInfoViewModel>(languageService.GetAvailableCultures().Select(culture => new CultureInfoViewModel(new CultureInfoModel(culture.DisplayName, culture))));

        SelectedCulture = Cultures.FirstOrDefault(cultureViewModel => cultureViewModel.Culture == CultureInfo.CurrentCulture);
        SelectedCulture = SelectedCulture is null ? Cultures.First() : SelectedCulture;
    }

    /// <summary>
    /// Command to use for changing the language
    /// </summary>
    [RelayCommand(CanExecute = nameof(ChangeLanguageCanExecute))]
    private void ChangeLanguage()
    {
        if (SelectedCulture is not null)
        {
            languageService.ChangeLanguage(SelectedCulture.Culture);
            WeakReferenceMessenger.Default.Send(new RefreshMainWindow(SelectedCulture.Culture));
            Abort();
        }
    }

    /// <summary>
    /// Check if the language can be changed
    /// </summary>
    /// <returns>True if changing is possible</returns>
    private bool ChangeLanguageCanExecute()
    {
        return SelectedCulture is not null;
    }

    /// <summary>
    /// Abort the change language dialog
    /// </summary>
    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
    }
}
