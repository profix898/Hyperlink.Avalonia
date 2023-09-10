using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace Hyperlink.Avalonia;

public class Hyperlink : TextBlock
{
    #region HyperlinkManager

    public static IHyperlinkManager HyperlinkManager { get; set; } = new HyperlinkManager();

    #endregion

    public static readonly StyledProperty<string?> UrlProperty = AvaloniaProperty.Register<Hyperlink, string?>(nameof(Url));

    public Hyperlink()
    {
        Classes.Add("Hyperlink");
    }

    public string? Url
    {
        get { return GetValue(UrlProperty); }
        set
        {
            SetValue(UrlProperty, value);
            SetValue(ToolTip.TipProperty, value);
        }
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(TextBlock);

    #endregion

    #region Overrides of InputElement

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (!String.IsNullOrEmpty(Url))
            HyperlinkManager.OpenUrl(Url);
    }

    #endregion
}
