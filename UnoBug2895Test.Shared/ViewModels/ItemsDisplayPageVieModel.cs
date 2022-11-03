namespace UnoBug2895Test.ViewModels;

internal partial class ShapesDisplayPageVieModel : ObservableObject
{

    public ShapesDisplayPageVieModel()
    {
        this.Log().MethodInvoked();
    }

    [ObservableProperty]
    List<Tuple<Shape, double>> _shapesList;

}
