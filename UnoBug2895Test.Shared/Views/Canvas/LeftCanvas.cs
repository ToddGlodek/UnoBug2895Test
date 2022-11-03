using Microsoft.UI.Xaml;

namespace UnoBug2895Test.Views;

internal class LeftCanvas  : CanvasAbstract<ViewModelLeftCanvas>
{
    private const double SIZE_REDUCTION_PRCNT = 0.10;
    private const double SIZE_ROTATION_FUDGE_FACTOR = 30.0;


    public LeftCanvas() {

        SizeChanged += OnSizeChanged;
        Loaded += OnLoaded;
        DataContextChanged += OnDataContextChanged;
        DataContext = new ViewModelLeftCanvas();
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        /** Perform this check because a user MIGHT use XAML to set the ViewModel to something inappropropriat  **/

        if (args.NewValue is ViewModelLeftCanvas nuVuMod) {
            nuVuMod.PropertyChanged += OnPropertyChanged;
        }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs args)
    {
        this.Log().MethodInvoked();
        ResizeControls();
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

            ResizeControls();
        }

    }

    private void ResizeControls()
    {

        if (VuMod.Shapes != null)
        {
            FrameworkElement previousShape = this;
            foreach (var shapeItem in VuMod.Shapes)
            {
                ResizeControls(shapeItem.Item1, previousShape, shapeItem.Item2);
                previousShape = shapeItem.Item1;
            }
        }

    }

    private void ResizeControls(Shape currentShape, FrameworkElement previousShape, double rotationAngle)
    {
        this.Log().LogCritical($"Resizing : { DescribeShape(currentShape)}");

        double reductionFactor = 1.00 - (SIZE_REDUCTION_PRCNT * 2);

        currentShape.Width = previousShape.ActualWidth * reductionFactor;
        currentShape.Height = previousShape.ActualHeight * reductionFactor;

        var previousTop = GetTop(previousShape);
        var previousLeft = GetLeft(previousShape);


        SetTop(currentShape, previousTop + (previousShape.ActualHeight * SIZE_REDUCTION_PRCNT) );
        SetLeft(currentShape, previousLeft + (previousShape.ActualWidth * SIZE_REDUCTION_PRCNT));

        currentShape.Opacity = 0.5;

    }

    protected override void OnPointerPressed(PointerRoutedEventArgs e, IEnumerable<UIElement> elements)
    {

        foreach (var element in elements)
        {
            this.Log().LogCritical($"{ DescribeShape(element) }");
        }

    }

    private string DescribeShape(UIElement currentUIElement) {

        string posit = string.Empty;
        if (currentUIElement is FrameworkElement element) {
            posit = $"H/W({element.Height.ToString("F0")}/{element.Width.ToString("F0")}) AH/AW({element.ActualHeight.ToString("F0")}/{element.ActualWidth.ToString("F0")})";
        }

        return $"Resizing : {currentUIElement.GetType()} Z({GetZIndex(currentUIElement)}) {posit}";

    }


}
