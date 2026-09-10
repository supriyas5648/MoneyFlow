using MoneyFlow.View;

namespace MoneyFlow;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
            // Temporary test startup for the only current database record (c_user_id = 1).
            Application.Run(new FrmChangePassword(1));
    }    
}