// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Microsoft.UI.Xaml.Navigation;

namespace UnoBug2895Test.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary> 
    public sealed partial class SideBySideGridPage : Page
    {
        public SideBySideGridPage()
        {
            this.Log().MethodInvoked();

            this.InitializeComponent();

            ShapesDisplayPageVieModel vm = vuMod;
            vm.PropertyChanged += OnPropertyChanged;

            /*******************************************************************************
             * on WInUI - this fires after the constructor has completed even though the   *
             * the DataContext change happens BEFORE the event handler has been registered *
             *******************************************************************************/
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

            (LeftPanel.DataContext as ViewModelLeftCanvas).Shapes = vuMod.ShapesList;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Log().MethodInvoked();

            base.OnNavigatedTo(e);

            ShapesDisplayPageVieModel vm = vuMod;

            vm.ShapesList = (List<Tuple<Shape, double>>)e.Parameter;

        }


        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Log().PropertyChanged(e);

            if (LeftPanel.IsLoaded)
            {
                if (e.PropertyName == nameof(ShapesDisplayPageVieModel.ShapesList))
                {
                    (LeftPanel.DataContext as ViewModelLeftCanvas).Shapes = vuMod.ShapesList;
                }
            }

        }


    }
}
