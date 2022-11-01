using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System;
using Uno.Extensions;
using Windows.UI.ApplicationSettings;
using System.Data;
using System.IO;
using System.Reflection;
using System.Linq;

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

                var points = BuildTheListOfPoints(itemsCount);

                try
                {
                    NavView.Header = args.InvokedItem.ToString();
                    NavContent.Navigate(pageType, points);
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
        private static List<Tuple<double, double>> BuildTheListOfPoints(int itemsCount)
        {
            List<Tuple<double, double>> points = new();


            Random rnd = new Random();

            for (int i = 0; i < itemsCount; i++)
            {


                var nuPoint = Tuple.Create<double, double>(rnd.NextDouble(), rnd.NextDouble());

                points.Add(nuPoint);

            }

            return points;
        }
    }
}
