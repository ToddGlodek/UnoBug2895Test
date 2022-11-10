using System.Reflection;

namespace UnoBug2895Test.Views;

internal abstract class UnoBug2895ControlAbnstract : Microsoft.UI.Xaml.Controls.Control {

    /*************************************************************************************
     *   This demonstrates the problem with the sequence of events being fired between
     *   WinUI and WASM.  On WinUI the sequence is as below, but on WASM the first 
     *   step, OnDataContextChanged() is never called - even though ViewModel is 
     *   instantiated already BEFORE OnApplyTemplate().  (Note: there may be additional
     *   intermediate steps in the middle that I am not tracking)
     *  
     *   1)  OnDataContextChanged()
     *   2)  OnApplyTemplate()
     *   3)  OnLoaded()
     *   
     *   
     ************************************************************************************/


    protected override void OnApplyTemplate()
    {

//#if HAS_UNO

//        if (DataContext != null  && DataContext is ObservableObject) {

//            EventHandler eventHandler = GetEventHandler(this, "DataContextChanged");

//            if (eventHandler != null) {

//                //Microsoft.UI.Xaml.PropertyChangedEventHandler? dataChangesArgs = new Microsoft.UI.Xaml.DataContextChangedEventArgs(DataContext)
//                //{
//                //    Handled = false,
//                //};

//                //eventHandler?.Invoke(this, dataChangesArgs);

//            }


//        }  

//        //if (this.DataContext != null && DataContext is ObservableObject && (DataContext as ObservableObject).  != null) {


//        //    DataContextChanged

//        //}


////        var vuMod = (DataContext as ObservableObject);
        

////        vuMod.PropertyChanged += On_PropertyChanged;
//#endif

        this.Log().MethodInvoked();

        base.OnApplyTemplate();



        #if HAS_UNO
                var vuMod = (DataContext as ObservableObject);
                vuMod.PropertyChanged += On_PropertyChanged;
        #endif



    }

    protected abstract void On_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e);


    //private static EventHandler GetEventHandler(object classInstance, string eventName)
    //{
    //    /**** This code doesnt run on UNO -   so it doesnt work as planned... ****/

    //    /***  https://stackoverflow.com/questions/1129517/c-sharp-how-to-find-if-an-event-is-hooked-up   ***/

    //    Type classType = classInstance.GetType();
    //    FieldInfo eventField = classType.GetField(eventName, BindingFlags.GetField
    //                                                       | BindingFlags.NonPublic
    //                                                       | BindingFlags.Instance);

    //    EventHandler eventDelegate = (EventHandler)eventField.GetValue(classInstance);

    //    // eventDelegate will be null if no listeners are attached to the event
    //    if (eventDelegate == null)
    //    {
    //        return null;
    //    }

    //    return eventDelegate;
    //}


}

internal class SideBySideGrid : UnoBug2895ControlAbnstract
{
    public SideBySideGrid()
    {

        this.Log().MethodInvoked();

        DataContextChanged += OnDataContextChanged;
        Loaded += OnLoaded;

        DefaultStyleKey = typeof(SideBySideGrid);
    }

    private void OnDataContextChanged(Microsoft.UI.Xaml.FrameworkElement sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
    {
        //**** WinUI  #1
        //**** WASM @ NEVER CALLED -  The ViewModel is instantiated already BEFORE OnApplyTemplate().  As a consequence, no
        //****        event handlers (e.g. On_PropertyChanged) that a developer would attempt to attach here would ever get
        //****        registered.   So instead, we will attach them in OnApplyTemplate instead when HAS_UNO.
        

#if !HAS_UNO 
        var vuMod = (args.NewValue as ObservableObject);
        vuMod.PropertyChanged += On_PropertyChanged;
#endif

        this.Log().MethodInvoked();

    }

    protected override void OnApplyTemplate()
    {
        //**** WinUI #2
        //**** WASM #1

        this.Log().MethodInvoked();

        base.OnApplyTemplate();
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //**** WinUI  #3
        //**** WASM #2

        this.Log().MethodInvoked();

        this.On_PropertyChanged(this.DataContext, new System.ComponentModel.PropertyChangedEventArgs(nameof(ShapesDisplayPageVieModel.ShapesList)));
    }


    override protected void On_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.Log().PropertyChanged(e);

        LeftPanel leftPanel = (LeftPanel) GetTemplateChild("LeftPanel");

        if (leftPanel.IsLoaded)
        {
            if (e.PropertyName == nameof(ShapesDisplayPageVieModel.ShapesList))
            {
                (leftPanel.DataContext as ViewModelLeftCanvas).Shapes = ((ShapesDisplayPageVieModel) sender).ShapesList;
            }
        }

    }
}
