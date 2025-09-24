using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Edgeify
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageBox.Show(
              "WARNING:\n \nThis software is provided as-is without any warranties. " +
               "The author assumes no responsibility for any damage, data loss, " +
               "or issues caused by using this program. Proceed at your own risk.",
                "DISCLAIMER",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            Application.Run(new Edgefiy());
        }
    }
}
