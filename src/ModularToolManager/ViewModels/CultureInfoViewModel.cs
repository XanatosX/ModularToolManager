using ModularToolManagerModel.Data;
using System.Globalization;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for culture infor
/// </summary>
public class CultureInfoViewModel : ViewModelBase
{
    /// <summary>
    /// The culture info model to display
    /// </summary>
    private CultureInfoModel CultureInfoModel;

    /// <summary>
    /// The culture information stored
    /// </summary>
    public CultureInfo Culture => CultureInfoModel.Culture;

    /// <summary>
    /// The name to display
    /// </summary>
    public string DisplayName => CultureInfoModel.DisplayName;

    /// <summary>
    /// Create a new instance for this class
    /// </summary>
    /// <param name="cultureInfoModel">The culture info to display</param>
    public CultureInfoViewModel(CultureInfoModel cultureInfoModel)
    {
        CultureInfoModel = cultureInfoModel;
    }


}
