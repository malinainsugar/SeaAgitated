using System;
using System.Windows.Forms;
using SeaAgitated;

namespace Program
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new SeaAgitatedForm());
        }
    }
}