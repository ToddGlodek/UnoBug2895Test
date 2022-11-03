using System.IO;
using System.Reflection;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoBug2895Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        const string README_FILE = "README_WinUI_vs_WASM.txt";

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += On_Loaded;

            this.NavView.DataContext = "Hello, world!";

        }

        private async void On_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var assembly = Assembly.GetEntryAssembly();

            string resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(README_FILE));

            using var resourceStream = assembly.GetManifestResourceStream(resourcePath);

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                this.ReadMeText.Text = await reader.ReadToEndAsync();
            }

        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {


            if (args.IsSettingsInvoked)
            {
                //  NavContent.Navigate(typeof(SettingsPage));
            }
            else
            {
                int itemsCount = int.Parse(args.InvokedItemContainer.Name.Substring(1));

                Type pageType = DetermineNavigationTargetFromMenuItem(args.InvokedItemContainer);

                var shapes = ShapeListFactory();

                try
                {
                    NavView.Header = args.InvokedItem.ToString();
                    NavContent.Navigate(pageType, shapes);
                }
                catch (Exception ex)
                {
                    this.Log().LogError(ex, "Failed to NavigateTo ItemsDisplayPage");
                }
            }

        }
        private static Type DetermineNavigationTargetFromMenuItem(NavigationViewItemBase invokedItemContainer) {

            string menuOption = invokedItemContainer.Name.Substring(0, 1);

            switch (menuOption) {
                case "I":
                    return typeof(SideBySideGridPage);
                case "T":
                    return typeof(SideBySide_ControlPage);
                default:
                    throw new ArgumentOutOfRangeException( $"Unknown Menu Option {menuOption}" );
            }

        }
        private static List<Tuple<Shape, double>> ShapeListFactory()
            {

            /**  This shores the shape and the rotation fudge factor **/
            List<Tuple<Shape, double>> shapes = new();

            {
                Rectangle rectangle;
                rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(Colors.Blue);
                rectangle.IsHitTestVisible = true;
                Canvas.SetZIndex(rectangle, 1);
                var nuRect = Tuple.Create<Shape, double>(rectangle, 0.0);
                shapes.Add(nuRect);
            }

            {
                Ellipse ellipse;
                ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Colors.White);
                ellipse.IsHitTestVisible = true;
                Canvas.SetZIndex(ellipse, 2);
                var nuEllipse = Tuple.Create<Shape, double>(ellipse, 0.0);
                shapes.Add(nuEllipse);
            }

            {
                Rectangle diamond;
                diamond = new Rectangle();
                diamond.Fill = new SolidColorBrush(Colors.Red);
                diamond.IsHitTestVisible = true;
                Canvas.SetZIndex(diamond, 3);
                var nuDiamond = Tuple.Create<Shape, double>(diamond, 45.0);
                shapes.Add(nuDiamond);
            }

            return shapes;
        }
    }
}
