using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using Avalonia.Threading;

namespace Hyperlink.Avalonia;

/// <summary>
/// A TextBlock control that functions as a navigateable hyperlink.
/// </summary>
[PseudoClasses(pcVisited)]
public class Hyperlink : TextBlock
{
    #region Launcher

    public static ILauncher Launcher { get; set; } = new HyperlinkLauncher();

    #endregion
    
    // See: https://www.w3schools.com/cssref/sel_visited.php
    private const string pcVisited = ":visited";

    /// <summary>
    /// Defines the <see cref="NavigateUri"/> property.
    /// </summary>
    public static readonly StyledProperty<Uri?> NavigateUriProperty = AvaloniaProperty.Register<Hyperlink, Uri?>(nameof(NavigateUri), defaultValue: null);
        
    /// <summary>
    /// Defines the <see cref="IsVisited"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> IsVisitedProperty = AvaloniaProperty.Register<Hyperlink, bool>(nameof(IsVisited), defaultValue: false);
        
    /// <summary>
    /// Defines the <see cref="TrackIsVisited"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> TrackIsVisitedProperty = AvaloniaProperty.Register<Hyperlink, bool>(nameof(TrackIsVisited), defaultValue: true);

    /// <summary>
    /// Initializes a new instance of the <see cref="Hyperlink"/> class.
    /// </summary>
    public Hyperlink()
    {
        Classes.Add("Hyperlink");
    }

    /// <summary>
    /// Gets or sets the Uniform Resource Identifier (URI) navigated to when the <see cref="Hyperlink"/> is clicked.
    /// </summary>
    /// <remarks>
    /// The URI may be any website or file location that can be launched using the <see cref="ILauncher"/> service.
    /// </remarks>
    public Uri? NavigateUri
    {
        get { return GetValue(NavigateUriProperty); }
        set { SetValue(NavigateUriProperty, value); }
    }
        
    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="NavigateUri"/> has been visited.
    /// </summary>
    public bool IsVisited
    {
        get { return GetValue(IsVisitedProperty); }
        set { SetValue(IsVisitedProperty, value); }
    }
        
    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="IsVisited"/> flag should be set upon visiting the Uri (default: false).
    /// </summary>
    public bool TrackIsVisited
    {
        get { return GetValue(TrackIsVisitedProperty); }
        set { SetValue(TrackIsVisitedProperty, value); }
    }

    #region Overrides of StyledElement

    protected override Type StyleKeyOverride => typeof(TextBlock);

    #endregion

    /// <inheritdoc/>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IsVisitedProperty)
            PseudoClasses.Set(pcVisited, change.GetNewValue<bool>());
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (NavigateUri == null)
            return;
            
        Dispatcher.UIThread.Post(async () =>
        {
            var success = await Launcher.LaunchUriAsync(NavigateUri);
            if (success && TrackIsVisited)
                SetCurrentValue(IsVisitedProperty, true);
        });
    }
}
