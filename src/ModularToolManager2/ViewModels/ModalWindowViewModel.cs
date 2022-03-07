using Avalonia.Media;
using Avalonia.Styling;
using ModularToolManager2.Services.Styling;
using ModularToolManager2.ViewModels.Extenions;
using ReactiveUI;
using Splat;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ModularToolManager2.ViewModels
{
    public class ModalWindowViewModel : ViewModelBase
    {
        private readonly IStyleService styleService;

        public StreamGeometry? PathIcon { get; }

        public string Title { get; }

        public ViewModelBase ModalContent { get; }

        public Interaction<Unit, Unit> CloseWindowInteraction { get; }

        public ModalWindowViewModel(string title, string pathName, ViewModelBase modalContent)
        {
            Title = title;
            ModalContent = modalContent;
            styleService = Locator.Current.GetService<IStyleService>();
            CloseWindowInteraction = new Interaction<Unit, Unit>();

            if (modalContent is IModalWindowEvents windowEvents)
            {
                windowEvents.Closing += (_, _) =>
                {
                    Task.Run(async () =>
                    {
                        _ = await CloseWindowInteraction.Handle(new Unit());
                    }).GetAwaiter().GetResult();
                };
            }

            IStyle? styles = styleService.GetCurrentAppIncludeStyles().FirstOrDefault();
            PathIcon = styleService.GetStyleByName<StreamGeometry>(pathName) ?? null;
        }
    }
}