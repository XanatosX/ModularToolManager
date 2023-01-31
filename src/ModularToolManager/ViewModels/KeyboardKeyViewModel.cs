using CommunityToolkit.Mvvm.ComponentModel;

namespace ModularToolManager.ViewModels;
internal partial class KeyboardKeyViewModel : ObservableObject
{
	[ObservableProperty]
	private string? buttonText;


	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PrefixSet))]
	private string? prefixText;

	public bool PrefixSet => !string.IsNullOrWhiteSpace(PrefixText);

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PostfixSet))]
	private string? postfixText;

	public bool PostfixSet => !string.IsNullOrWhiteSpace(PostfixText);

	public KeyboardKeyViewModel(string buttonText) : this(buttonText, string.Empty, string.Empty)
	{
	}

	public KeyboardKeyViewModel(string buttonText, string prefixText, string postfixText)
	{
		ButtonText = buttonText;
		PrefixText = prefixText;
		PostfixText = postfixText;
	}
}
