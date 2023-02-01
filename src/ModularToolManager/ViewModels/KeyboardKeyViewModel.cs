using CommunityToolkit.Mvvm.ComponentModel;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Model class for a single keyboard key
/// </summary>
internal partial class KeyboardKeyViewModel : ObservableObject
{
	/// <summary>
	/// The text which should be shown for the button
	/// </summary>
	[ObservableProperty]
	private string? buttonText;

	/// <summary>
	/// Any prefix before the button text
	/// </summary>
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PrefixSet))]
	private string? prefixText;

	/// <summary>
	/// Is there a prefix for the button
	/// </summary>
	public bool PrefixSet => !string.IsNullOrWhiteSpace(PrefixText);

	/// <summary>
	/// Any postfix for the button
	/// </summary>
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PostfixSet))]
	private string? postfixText;

	/// <summary>
	/// Is there a postfix set for the button
	/// </summary>
	public bool PostfixSet => !string.IsNullOrWhiteSpace(PostfixText);

	/// <summary>
	/// Create a new instance of this class without prefix and postfix
	/// </summary>
	/// <param name="buttonText">The button text to show</param>
	public KeyboardKeyViewModel(string buttonText) : this(buttonText, string.Empty, string.Empty)
	{
	}

	/// <summary>
	/// Create a new instance of this class with a prefix and/or Ppostfix
	/// </summary>
	/// <param name="buttonText">The button text to show</param>
	/// <param name="prefixText">The prefix text to show</param>
	/// <param name="postfixText">The postfix text to show</param>
	public KeyboardKeyViewModel(string buttonText, string? prefixText, string? postfixText)
	{
		ButtonText = buttonText;
		PrefixText = prefixText;
		PostfixText = postfixText;
	}
}
