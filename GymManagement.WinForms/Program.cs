using System;
using System.Windows.Forms;

namespace GymManagement.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm()); // Start with MainForm instead of Form1
        }
    }
}