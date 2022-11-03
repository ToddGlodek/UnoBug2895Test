
using Microsoft.UI.Xaml;

namespace UnoBug2895Test.Views;

internal class RightCanvas : CanvasAbstract<ViewModelRightCanvas>
{

    public RightCanvas()
    {
        DataContext = new ViewModelRightCanvas();
    }

    protected override void OnPointerPressed(PointerRoutedEventArgs e, IEnumerable<UIElement> elements)
    {
    }
}
