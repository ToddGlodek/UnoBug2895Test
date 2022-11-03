
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace UnoBug2895Test.Views;

internal abstract class CanvasAbstract<V> : Microsoft.UI.Xaml.Controls.Canvas where V : IRealyComplicatedInteraceWithManyProperties
{

    private Brush _workingBrush;

    protected CanvasAbstract() {

        this.Log().MethodInvoked();

        PointerEntered += OnPointerEntered;
        PointerExited += OnPointerExited;
        PointerPressed += OnPointerPressed;
    }

    private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
    {
        _workingBrush = Background;
        Background = new SolidColorBrush(Colors.Transparent);
    }

    private void OnPointerExited(object sender, PointerRoutedEventArgs e)
    {
        Background = _workingBrush;
    }

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType == Microsoft.UI.Input.PointerDeviceType.Mouse)
        {
            var properties = e.GetCurrentPoint(this).Properties;
            if (properties.IsLeftButtonPressed)
            {

                IEnumerable<UIElement> elements = GetUIElementsAtPointerPosition(e);

                try
                {
                    OnPointerPressed(e, elements);
                }
                catch (Exception ex)
                {
                    this.Log().LogError(ex, "Failed to process pointer event");
                    throw;
                }

            }

        }
    }

    protected Point GetCanvasPoint(PointerRoutedEventArgs e)
    {
        var rtnPoint = e.GetCurrentPoint(this).Position;
        return rtnPoint;
    }

    protected IEnumerable<UIElement> GetUIElementsAtPointerPosition(PointerRoutedEventArgs e)
    {
        IEnumerable<UIElement> elementsAtPosit = null;

//#if !__WASM__
        Point canvasPoint = GetCanvasPoint(e);
        elementsAtPosit = VisualTreeHelper.FindElementsInHostCoordinates(canvasPoint, this, true);
//#else
//        Point pagePoint = GetPagePoint(e);

//        elementsAtPosit = VisualTreeHelper.FindElementsInHostCoordinates(pagePoint, this.MyCanvas);

//#endif

        return elementsAtPosit;
    }



    protected abstract void OnPointerPressed(PointerRoutedEventArgs e, IEnumerable<UIElement> elements);

    protected V VuMod {
        get => (V)DataContext;
    }

}
