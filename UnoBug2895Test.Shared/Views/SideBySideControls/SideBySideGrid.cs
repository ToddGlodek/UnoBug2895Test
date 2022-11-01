
namespace UnoBug2895Test.Views;

internal class SideBySideGrid : Microsoft.UI.Xaml.Controls.Control
{

    public SideBySideGrid()
    {

        this.Log().MethodInvoked();

        DefaultStyleKey = typeof(SideBySideGrid);

        DataContextChanged += OnDataContextChanged;
        Loaded += OnLoaded;

    }

    private void OnDataContextChanged(Microsoft.UI.Xaml.FrameworkElement sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
    {
        this.Log().MethodInvoked();
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        this.Log().MethodInvoked();
    }

    protected override void OnApplyTemplate()
    {
        this.Log().MethodInvoked();

        base.OnApplyTemplate();

        var booText = GetTemplateChild("txtBoo");

    }
}
