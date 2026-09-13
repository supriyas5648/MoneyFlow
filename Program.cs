using System;
using System.Windows.Forms;
using MoneyFlow.Data;
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
        ApplicationConfiguration.Initialize();

        try
        {
            DatabaseInitializer.Initialize();
        }
        catch
        {
            // Database initialization will gracefully fallback if server is offline
        }

        Application.Run(new FrmLogin());
    }    
}