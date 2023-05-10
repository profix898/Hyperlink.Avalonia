using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;

namespace Hyperlink.Avalonia;

public class Hyperlink : TextBlock, IStyleable
{
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

    #region IStyleable

    protected Type StyleKey => typeof(TextBlock);

    #endregion

    #region Overrides of InputElement

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (!String.IsNullOrEmpty(Url))
            HyperlinkManager.Instance.OpenUrl(Url);
    }

    #endregion
}
