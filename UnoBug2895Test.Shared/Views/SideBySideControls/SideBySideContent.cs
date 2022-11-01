
namespace UnoBug2895Test.Views;

internal class SideBySideContent : Microsoft.UI.Xaml.Controls.ContentControl
{

    public SideBySideContent()
    {

        this.Log().MethodInvoked();

        this.DefaultStyleKey = typeof(SideBySideContent);

        this.DataContextChanged += OnDataContextChanged;
        this.Loaded += OnLoaded;

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
    }
}
