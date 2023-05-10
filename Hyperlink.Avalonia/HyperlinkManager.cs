using Microsoft.Maui.ApplicationModel;

namespace Hyperlink.Avalonia;

public class HyperlinkManager
{
    public static HyperlinkManager Instance { get; set; } = new HyperlinkManager();

    public virtual void OpenUrl(string url)
    {
        Browser.OpenAsync(url);
    }
}
