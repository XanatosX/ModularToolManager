using Avalonia;
using Avalonia.ReactiveUI;
using ModularToolManager2.Models;
using ModularToolManager2.Services.Language;
using ModularToolManager2.ViewModels.Extenions;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager2.ViewModels
{
    public class ChangeLanguageViewModel : ViewModelBase, IModalWindowEvents
    {
        private readonly ILanguageService languageService;

        public ObservableCollection<CultureInfoViewModel> Cultures { get; }

        public CultureInfoViewModel SelectedCulture
        {
            get => selectedCulture;
            set => this.RaiseAndSetIfChanged(ref selectedCulture, value);
        }

        private CultureInfoViewModel selectedCulture;

        public ICommand ChangeLanguageCommand { get; }

        public ICommand AbortCommand { get; }

        public event EventHandler Closing;

        public ChangeLanguageViewModel()
        {
            languageService = Locator.Current.GetService<ILanguageService>();
            Cultures = new ObservableCollection<CultureInfoViewModel>(languageService.GetAvailableCultures().Select(culture => new CultureInfoViewModel(new CultureInfoModel(culture.DisplayName, culture))));

            //SelectedCulture = Cultures.First(cultureViewModel => cultureViewModel.Culture == Application.Current.cul);

            AbortCommand = ReactiveCommand.Create(async () =>
            {
                Closing?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
