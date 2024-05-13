using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Hyperlink.Avalonia;

public class HyperlinkLauncher : ILauncher
{
    #region Implementation of ILauncher

    public async Task<bool> LaunchUriAsync(Uri uri)
    {
        return await GetTopLevel().Launcher.LaunchUriAsync(uri);
    }

    public async Task<bool> LaunchFileAsync(IStorageItem storageItem)
    {
        return await GetTopLevel().Launcher.LaunchFileAsync(storageItem);
    }

    #endregion

    private TopLevel GetTopLevel()
    {
        var appLifetime = Application.Current!.ApplicationLifetime;

        if (appLifetime is ClassicDesktopStyleApplicationLifetime desktopLifetime && desktopLifetime.MainWindow != null)
        {
            return desktopLifetime.Windows.FirstOrDefault(wnd => wnd.IsActive)
                   ?? desktopLifetime.MainWindow
                   ?? throw new ApplicationException("Application does not specify a MainWindow.");
        }

        if (appLifetime is ISingleViewApplicationLifetime singleViewLifetime && singleViewLifetime.MainView != null)
        {
            return TopLevel.GetTopLevel(singleViewLifetime.MainView)
                   ?? throw new ApplicationException("Application does not specify a MainView.");
        }

        throw new NotSupportedException($"IApplicationLifetime of type '{appLifetime.GetType()}' not supported.");
    }
}
