using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System.Configuration;

namespace UnoBug2895Test.Views;

internal class LeftCanvas  : CanvasAbstract<ViewModelLeftCanvas>
{
    private const double SIZE_REDUCTION_PRCNT = 0.10;
    private const double SIZE_ROTATION_FUDGE_FACTOR = 30.0;


    Rectangle rectangle;
    Ellipse ellipse;
    Rectangle diamond;

    public LeftCanvas() {
        DataContext = new ViewModelLeftCanvas();
        this.SizeChanged += OnSizeChanged;
        this.Loaded += OnLoaded;
    }

    private void OnSizeChanged(object sender, Microsoft.UI.Xaml.SizeChangedEventArgs args)
    {
        ResizeControls();
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        rectangle = new Rectangle();
        rectangle.Fill = new SolidColorBrush(Colors.Blue);
        Children.Add(rectangle);

        ellipse = new Ellipse();
        ellipse.Fill = new SolidColorBrush(Colors.White);
        Children.Add(ellipse);

        diamond = new Rectangle();
        diamond.Fill = new SolidColorBrush(Colors.Red);
        Children.Add(diamond);

        ResizeControls();
    }

    private void ResizeControls()
    {
        double reduction = 1.00 - (SIZE_REDUCTION_PRCNT * 2);

        if (rectangle != null)
        {

            rectangle.Width = this.ActualWidth * reduction;
            rectangle.Height = this.ActualHeight * reduction;

            SetTop(rectangle, this.ActualHeight * SIZE_REDUCTION_PRCNT);
            SetLeft(rectangle, this.ActualWidth * SIZE_REDUCTION_PRCNT);

            

            if (ellipse != null) {
                ellipse.Width = rectangle.Width * reduction;
                ellipse.Height = rectangle.Height * reduction;

                var rectTop = GetTop(rectangle);
                var rectLeft = GetLeft(rectangle);

                SetTop(ellipse, rectTop + (rectangle.Height * SIZE_REDUCTION_PRCNT) );
                SetLeft(ellipse, rectLeft + (rectangle.Width * SIZE_REDUCTION_PRCNT) );

                if (diamond != null)
                {
                    diamond.Width = ellipse.Width * reduction;
                    diamond.Height = diamond.Width;

                    var ellipseTop = GetTop(ellipse);
                    var ellipseLeft = GetLeft(ellipse);

                    SetTop(diamond, ellipseTop + (ellipse.Height * SIZE_REDUCTION_PRCNT) + SIZE_ROTATION_FUDGE_FACTOR );
                    SetLeft(diamond, ellipseLeft + (ellipse.Width * SIZE_REDUCTION_PRCNT));

                    diamond.RenderTransform = new RotateTransform()
                    {
                        CenterX = diamond.ActualWidth / 2.0,
                        CenterY = (diamond.ActualHeight / 2.0),
                        Angle = 45
                    };

                }

            }

        }
    }

}
