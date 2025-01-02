using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace Medical
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                File.WriteAllText("error.log", args.ExceptionObject.ToString());
            };

            base.OnStartup(e);
        }
    }

}
