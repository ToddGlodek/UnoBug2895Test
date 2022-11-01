namespace UnoBug2895Test.ViewModels;

internal partial class ItemsDisplayPageVieModel : ObservableObject
{

    public ItemsDisplayPageVieModel()
    {
        this.Log().MethodInvoked();
    }

    [ObservableProperty]
    List<Tuple<double, double>> _pointList;

    //[ObservableProperty]
    //string _itemName;

}
