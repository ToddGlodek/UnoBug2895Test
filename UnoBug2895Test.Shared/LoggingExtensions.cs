
using System.Runtime.CompilerServices;

namespace UnoBug2895Test;

public static class LoggingExtensions
{

    /********************************************************************************
     * @jeromelaban - you should add this to your Uno.Extensions because it is so   *
     *                incredibly useful                                             *
     ********************************************************************************/

    [System.Diagnostics.DebuggerStepThrough]
    public static void MethodInvoked(this ILogger l, [CallerMemberName] string methodName = "")
    {

#if DEBUG  
        /** Only log the MethodInvoked when in DEBUG **/


        if ( l.IsEnabled(LogLevel.Critical )) {
            /********************************************************************************************
             * I am using *Critical* for UnoBug2895Test just so that no logging events disapper in the  *
             * shift between WinUI and WASM                                                             *
             ********************************************************************************************/
            l.LogCritical(methodName);
        }
#endif
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static void PropertyChanged(this ILogger l, System.ComponentModel.PropertyChangedEventArgs e)
    {

#if DEBUG  
        /** Only log the PropertyChanged when in DEBUG **/


        if (l.IsEnabled(LogLevel.Critical))
        {
            /********************************************************************************************
             * I am using *Critical* for UnoBug2895Test just so that no logging events disapper in the  *
             * shift between WinUI and WASM                                                             *
             ********************************************************************************************/
            l.LogCritical(  $"Changed: {e.PropertyName}" );
        }
#endif
    }



    
}