This compares the sequence of method calls (WinUI vs WASM) when the USING CUSTOM CONTROL menu option is selected on each platform.   Because the DataContext does not appear to have been set before the Loaded event is fired on WASM, the control's view model data does not seem to be available to continue the rendering process then it simply ABENDS after only a few steps. However, its not just that the order is different, some method calls are never made on WASM.  For example, OnApplyTemplate is never called on WASM.   As a consequence, the two canvases (which are declared in the template) never get instantiated.


WinUI
===========================================================================
UnoBug2895Test.Views.SideBySide_ControlPage: Critical: .ctor
UnoBug2895Test.ViewModels.ItemsDisplayPageVieModel: Critical: .ctor
UnoBug2895Test.Views.SideBySideGrid: Critical: .ctor
UnoBug2895Test.Views.SideBySide_ControlPage: Critical: OnNavigatedTo
UnoBug2895Test.Views.SideBySide_ControlPage: Critical: PointList Changed
UnoBug2895Test.Views.SideBySide_ControlPage: Critical: OnDataContextChanged
UnoBug2895Test.Views.SideBySideGrid: Critical: OnDataContextChanged
UnoBug2895Test.Views.SideBySideGrid: Critical: OnDataContextChanged
UnoBug2895Test.Views.CanvasAbstract: Critical: .ctor
UnoBug2895Test.ViewModels.ViewModelAbstract: Critical: .ctor
UnoBug2895Test.Views.CanvasAbstract: Critical: .ctor
UnoBug2895Test.ViewModels.ViewModelAbstract: Critical: .ctor
UnoBug2895Test.Views.SideBySideGrid: Critical: OnApplyTemplate
UnoBug2895Test.Views.SideBySideGrid: Critical: OnLoaded
UnoBug2895Test.Views.SideBySide_ControlPage: Critical: OnLoaded



WASM  ** NOTE:  SideBySideGrid: OnApplyTemplate  IS NEVER CALLED **
===========================================================================
crit: UnoBug2895Test.Views.SideBySide_ControlPage[0]      .ctor
crit: UnoBug2895Test.Views.SideBySideGrid[0]      .ctor
crit: UnoBug2895Test.ViewModels.ItemsDisplayPageVieModel[0]      .ctor
crit: UnoBug2895Test.Views.SideBySideGrid[0]      OnDataContextChanged
crit: UnoBug2895Test.Views.SideBySide_ControlPage[0]      OnLoaded
crit: UnoBug2895Test.Views.SideBySideGrid[0]      OnLoaded
crit: UnoBug2895Test.Views.SideBySide_ControlPage[0]      OnNavigatedTo
crit: UnoBug2895Test.Views.SideBySide_ControlPage[0]      PointList Changed
