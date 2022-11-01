namespace UnoBug2895Test.Views;

internal class LeftCanvas  : CanvasAbstract<ViewModelLeftCanvas>
{

    public LeftCanvas() {
        DataContext = new ViewModelLeftCanvas();
    }

}
