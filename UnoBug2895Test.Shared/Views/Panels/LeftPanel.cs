
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace UnoBug2895Test.Views;

internal class LeftPanel : PanelAbstract<ViewModelLeftCanvas>
{
    private const double SIZE_REDUCTION_PRCNT = 0.10;
    private const double SIZE_ROTATION_FUDGE_FACTOR = 30.0;


    public LeftPanel()
    {

        SizeChanged += OnSizeChanged;
        Loaded += OnLoaded;
        DataContextChanged += OnDataContextChanged;
        DataContext = new ViewModelLeftCanvas();
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        /** Perform this check because a user MIGHT use XAML to set the ViewModel to something inappropropriat  **/

        if (args.NewValue is ViewModelLeftCanvas nuVuMod)
        {
            nuVuMod.PropertyChanged += OnPropertyChanged;
        }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs args)
    {
        this.Log().MethodInvoked();

    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        this.Log().MethodInvoked();

    }

    private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {

        this.Log().PropertyChanged(e);

        if (e.PropertyName == nameof(ViewModelLeftCanvas.Shapes))
        {

            Children.Clear();

            if (VuMod.Shapes != null)
            {
                foreach (var shapeItem in VuMod?.Shapes)
                {
                    Children.Add(shapeItem.Item1);
                }
            }

            InvalidateArrange();
        }

    }

    protected override Size MeasureOverride(Size availableSize)
    {

        if (VuMod.Shapes != null)
        {

            int I = 1;
            foreach (var currentShape in Children)
            {
                double reductionFactor = ComputeReductionFactor(I++);

                Size newSize = new Size((availableSize.Width * reductionFactor), (availableSize.Height * reductionFactor));

                currentShape.Measure(newSize);
            }
        }

        return availableSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        int I = 1;
        foreach (var currentShape in Children)
        {
            double reductionFactor = ComputeReductionFactor(I);
            double offset = SIZE_REDUCTION_PRCNT * I++;

            currentShape.Arrange(new Rect(finalSize.Width * offset, finalSize.Height * offset, finalSize.Width * reductionFactor, finalSize.Height * reductionFactor));
        }

        return finalSize;
    }

    private static double ComputeReductionFactor(int i)
    {
        return 1.00 - (SIZE_REDUCTION_PRCNT * 2 * i);
    }


    protected override void OnPointerPressed(PointerRoutedEventArgs e, IEnumerable<UIElement> elements)
    {

        foreach (var element in elements)
        {
            this.Log().LogCritical($"{DescribeShape(element)}");
        }

    }

    private string DescribeShape(UIElement currentUIElement)
    {

        string posit = string.Empty;
        if (currentUIElement is FrameworkElement element)
        {
            posit = $"H/W({element.Height.ToString("F0")}/{element.Width.ToString("F0")}) AH/AW({element.ActualHeight.ToString("F0")}/{element.ActualWidth.ToString("F0")})";
        }

        //  return $"Resizing : {currentUIElement.GetType()} Z({GetZIndex(currentUIElement)}) {posit}";
        return "FIX ME!!";
    }


}
