using PD_Helper.Library;
using System.Diagnostics;

namespace PD_Helper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppExceptionHandler);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Application.Run(new LabForm());
        }

        static void AppExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            if (e is AppException appException)
            {
                // Exceptions of this type we want to display to the user...
                MessageBox.Show(appException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // All other exceptions? Well, just log them in the debug output for now ;)
                Debug.WriteLine("AppExceptionHandler caught : " + e.Message);
            }
        }
    }
}