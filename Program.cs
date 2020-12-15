using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio;
using NAudio.Mixer;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using MidiVstTest;
using BInrerface;

namespace Program
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
            Application.Run(new MainForm());
        }
    }
}
