using doot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doot_gen
{

    class Options
    {
        public string chunkDir;
        public string wwsieCLIexe;
        public string wwiseProject;
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            doot.Horns.Hrn018.GetHornNames().
            GetHornName(Horns.Hrn018);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

}
