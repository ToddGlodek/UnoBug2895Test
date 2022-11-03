namespace UnoBug2895Test.ViewModels;

partial class ViewModelLeftCanvas : ViewModelAbstract
{

    public ViewModelLeftCanvas() : base() {
        this.Log().MethodInvoked();
    }

    [ObservableProperty]
    public List<Tuple<Shape, double>> _shapes;

}
