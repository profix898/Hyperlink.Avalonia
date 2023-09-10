using Microsoft.Maui.ApplicationModel;

namespace Hyperlink.Avalonia;

public class HyperlinkManager : IHyperlinkManager
{
    #region Implementation of IHyperlinkManager

    public virtual void OpenUrl(string url)
    {
        Browser.OpenAsync(url);
    }

    #endregion
}
