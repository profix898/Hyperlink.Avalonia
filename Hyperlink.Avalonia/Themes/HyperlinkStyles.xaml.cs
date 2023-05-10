using System;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Hyperlink.Avalonia.Themes
{
    public class HyperlinkStyles : Styles
    {
        public HyperlinkStyles(IServiceProvider? sp = null)
        {
            AvaloniaXamlLoader.Load(sp, this);
        }
    }
}
