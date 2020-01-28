﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace Opera_Beta_x64_Launcher
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo culture1 = CultureInfo.CurrentUICulture;
            if (File.Exists(@"Opera Beta x64\Launcher.exe"))
            {
                if (!File.Exists(@"Opera Beta x64\Profile.txt"))
                {
                    if (culture1.Name == "de-DE")
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                        String Arguments = File.ReadAllText(@"Opera Beta x64\Profile.txt");
                        _ = Process.Start(@"Opera Beta x64\Launcher.exe", Arguments);
                    }
                    else
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form2());
                        String Arguments = File.ReadAllText(@"Opera Beta x64\Profile.txt");
                        _ = Process.Start(@"Opera Beta x64\Launcher.exe", Arguments);
                    }
                    }
                else
                {
                    String Arguments = File.ReadAllText(@"Opera Beta x64\Profile.txt");
                    _ = Process.Start(@"Opera Beta x64\Launcher.exe", Arguments);
                }
            }
            else if (culture1.Name == "de-DE")
            {
                string message = "Opera Beta x64 ist nicht installiert";
                _ = MessageBox.Show(message, "Opera Beta x64 Launcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string message = "Opera Beta x64 is not installed";
                _ = MessageBox.Show(message, "Opera Beta x64 Launcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
