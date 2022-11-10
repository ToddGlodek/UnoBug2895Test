
using Microsoft.UI.Xaml.Navigation;

namespace UnoBug2895Test.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SideBySide_ControlPage : Microsoft.UI.Xaml.Controls.Page
    {
        public SideBySide_ControlPage()
        {
            this.Log().MethodInvoked();

            this.InitializeComponent();

            this.DataContextChanged += OnDataContextChanged;
            this.Loaded += OnLoaded;

            ShapesDisplayPageVieModel vm = vuMod;
            vm.PropertyChanged += OnPropertyChanged;
        }

        private void OnDataContextChanged(Microsoft.UI.Xaml.FrameworkElement sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
        {
            this.Log().MethodInvoked();
        }

        private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Log().MethodInvoked();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Log().MethodInvoked();

            base.OnNavigatedTo(e);

            ShapesDisplayPageVieModel vm = this.vuMod;

            vm.ShapesList = (List<Tuple<Shape, double>>)e.Parameter;

        }


        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Log().PropertyChanged(e);

        }


    }
}
