// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Microsoft.UI.Xaml.Navigation;

namespace UnoBug2895Test.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SideBySideGridPage : Microsoft.UI.Xaml.Controls.Page
    {
        public SideBySideGridPage()
        {
            this.Log().MethodInvoked();

            this.InitializeComponent();

            this.DataContextChanged += OnDataContextChanged;
            this.Loaded += OnLoaded;

            ItemsDisplayPageVieModel vm = vuMod;
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

            ItemsDisplayPageVieModel vm = this.vuMod;

            vm.PointList = (List<Tuple<double, double>>)e.Parameter;

        }


        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Log().LogCritical($"{e.PropertyName} Changed");
        }


    }
}
