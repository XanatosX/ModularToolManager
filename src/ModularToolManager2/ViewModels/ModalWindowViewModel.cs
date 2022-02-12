using Avalonia.Media;
using Avalonia.Styling;
using ModularToolManager2.Services.Styling;
using Splat;
using System.Linq;

namespace ModularToolManager2.ViewModels
{
    public class ModalWindowViewModel : ViewModelBase
    {
        private readonly IStyleService styleService;

        public StreamGeometry? PathIcon { get; }

        public string Title { get; }

        public ViewModelBase ModalContent { get; }

        public ModalWindowViewModel(string title, string pathName, ViewModelBase modalContent)
        {
            Title = title;
            ModalContent = modalContent;
            styleService = Locator.Current.GetService<IStyleService>();

            IStyle? styles = styleService.GetCurrentAppIncludeStyles().FirstOrDefault();
            PathIcon = styleService.GetStyleByName<StreamGeometry>(pathName) ?? null;
        }
    }
}