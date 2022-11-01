

namespace UnoBug2895Test.ViewModels;

interface IRealyComplicatedInteraceWithManyProperties
{
    /*****************************************************************************************
     *  Hopefully this will contain many properties exposed as default interface methods,    *
     *  However, this cannot be acheived due to a known Uno/.Net issue, and so all of the    *
     *  mehtods must be code-replicated in all of the ViewModels that implement this         *
     *  interface.                                                                           *
     *                                                                                       *
     *  SEE -[WASM] "Uncaught ExitStatus ExitStatus" is encountered when a                   *
     *  default method is defined inside the interface - NOT when it is defined inside a     *
     *  derived class. #77244                                                                *
     *                                                                                       *
     *  https://github.com/dotnet/runtime/issues/77244                                       *
     *                                                                                       *
     *****************************************************************************************/
}

abstract class ViewModelAbstract : ObservableObject, IRealyComplicatedInteraceWithManyProperties
{
    public ViewModelAbstract() {

        this.Log().MethodInvoked();

    }

}
