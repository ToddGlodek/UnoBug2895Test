
namespace UnoBug2895Test.Views;

internal abstract class CanvasAbstract<V> : Microsoft.UI.Xaml.Controls.Canvas where V : IRealyComplicatedInteraceWithManyProperties
{

    protected CanvasAbstract() {

        this.Log().MethodInvoked();

    }

    protected V VuMod {
        get => (V)DataContext;
    }

}
