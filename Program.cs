using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CheckSum
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // https://msdn.microsoft.com/en-us/library/system.threading.thread.currentuiculture(v=vs.110).aspx
            // Set the user interface to display in the same culture as that set in Control Panel.
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormSum());
        }
    }
}
