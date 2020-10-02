using System.Windows;
using System.Windows.Threading;
using Cto.FileSystemMonitor.Graphics;

namespace Cto.FileSystemMonitor
{

    public partial class App
    {

        private void Initialize(object sender, StartupEventArgs args)
        {
            new WnMain().Show();
        }

        private void HandleExceptions(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnException(args.Exception).ShowDialog();
        }

    }

}