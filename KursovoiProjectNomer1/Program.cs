using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (new Auth().ShowDialog() == DialogResult.Cancel)
            {
                //Application.Run(new Search());
                if (!string.IsNullOrEmpty(Global.LogginedData))
                {
                    Application.Run(new Main());
                }
            }
        }
    }
}
