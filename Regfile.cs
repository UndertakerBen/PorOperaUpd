using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portable_Opera_Updater
{
    public partial class Regfile
    {
        public static void RegCreate(string applicationPath, string instDir)
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\Opera.PORTABLE");
            key.SetValue("FriendlyTypeName", "Opera Web Document");
            key.SetValue("URL Protocol", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\Opera.PORTABLE\\DefaultIcon");
            key.SetValue(default, applicationPath + @"\" + instDir + @"\Launcher.exe,0");
            key.Close();
			key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\Opera.PORTABLE\\Application");
            key.SetValue("AppUserModelId", "Opera.PORTABLE");
            key.SetValue("ApplicationIcon", applicationPath + @"\" + instDir + @"\Launcher.exe,0");
            key.SetValue("ApplicationName", instDir + @" Portable");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\Opera.PORTABLE\\shell\\open\\command");
            key.SetValue(default, "\"" + applicationPath + @"\" + instDir + @" Launcher.exe"" -noautoupdate ""%1""");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\RegisteredApplications");
            key.SetValue("Opera.PORTABLE", @"Software\Clients\StartMenuInternet\Opera.PORTABLE\Capabilities");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE");
            key.SetValue(default, instDir + @" Portable");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities");
            key.SetValue("ApplicationDescription", "Do more on the web");
            key.SetValue("ApplicationIcon", applicationPath + @"\" + instDir + @"\Launcher.exe,0");
            key.SetValue("ApplicationName", instDir + @" Portable");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities\\FileAssociations");
            key.SetValue(".crx", "Opera.PORTABLE");
			key.SetValue(".nex", "Opera.PORTABLE");
			key.SetValue(".htm", "Opera.PORTABLE");
            key.SetValue(".html", "Opera.PORTABLE");
            key.SetValue(".shtml", "Opera.PORTABLE");
            key.SetValue(".opdownload", "Opera.PORTABLE");
            key.SetValue(".xht", "Opera.PORTABLE");
            key.SetValue(".xhtml", "Opera.PORTABLE");
            key.SetValue(".webp", "Opera.PORTABLE");
            key.SetValue(".pdf", "Opera.PORTABLE");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities\\Startmenu");
            key.SetValue("StartMenuInternet", "Opera.PORTABLE");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities\\URLAssociations");
            key.SetValue("ftp", "Opera.PORTABLE");
            key.SetValue("http", "Opera.PORTABLE");
            key.SetValue("https", "Opera.PORTABLE");
            key.SetValue("mailto", "Opera.PORTABLE");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\DefaultIcon");
            key.SetValue("ApplicationIcon", applicationPath + @"\" + instDir + @"\Launcher.exe,0");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\InstallInfo");
            key.SetValue("ReinstallCommand", "\"" + applicationPath + @"\\" + instDir + @" Launcher.exe"" --make-default-browser");
            key.SetValue("HideIconsCommand", "\"" + applicationPath + @"\\" + instDir + @" Launcher.exe"" --hide-icons");
            key.SetValue("ShowIconsCommand", "\"" + applicationPath + @"\\" + instDir + @" Launcher.exe"" --show-icons");
            key.SetValue("IconsVisible", 1, Microsoft.Win32.RegistryValueKind.DWord);
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\shell\\open\\command");
            key.SetValue(default, "\"" + applicationPath + @"\" + instDir + @" Launcher.exe"" -noautoupdate ""%1""");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.xhtml\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.xht\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.webp\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.shtml\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.pdf\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.html\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\.htm\\OpenWithProgids");
            key.SetValue("Opera.PORTABLE", "(NULL!)");
            key.Close();
			key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\Applications\\opera.exe\\shell\\open\\command");
            key.SetValue(default, "\"" + applicationPath + @"\" + instDir + @" Launcher.exe"" -noautoupdate ""%1""");
            key.SetValue("Path", applicationPath);
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\opera.exe");
            key.SetValue(default, "\"" + applicationPath + @"\" + instDir + @" Launcher.exe"" -noautoupdate ""%1""");
            key.SetValue("Path", applicationPath);
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ApplicationAssociationToasts");
            key.SetValue("Opera.PORTABLE_http", 0, Microsoft.Win32.RegistryValueKind.DWord);
			key.SetValue("Opera.PORTABLE_https", 0, Microsoft.Win32.RegistryValueKind.DWord);
			key.SetValue("Opera.PORTABLE_.htm", 0, Microsoft.Win32.RegistryValueKind.DWord);
			key.SetValue("Opera.PORTABLE_.html", 0, Microsoft.Win32.RegistryValueKind.DWord);
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice");
            key.SetValue("ProgId", "Opera.PORTABLE");
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice");
            key.SetValue("ProgId", "Opera.PORTABLE");
            key.Close();
            try
            {
                key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false);
                if (key.GetValue("ProductName").ToString().Contains("Windows 10"))
                {
                    key.Close();
                    Process process = new Process();
                    process.StartInfo.FileName = "ms-settings:defaultapps";
                    process.Start();
                }
                else
                {
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void RegDel()
        {
            try
            {
                Microsoft.Win32.RegistryKey key;
				
				Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Applications\\opera.exe\\shell\\open\\command", false);
				Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Applications\\opera.exe\\shell\\open", false);
				Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Applications\\opera.exe\\shell", false);
				Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Applications", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.pdf\\UserChoice", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities\\FileAssociations", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\shell\\open\\command", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\shell\\open", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\shell", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\DefaultIcon", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\InstallInfo", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities\\Startmenu", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE\\Capabilities\\URLAssociations", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Clients\\StartMenuInternet\\Opera.PORTABLE", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Opera.PORTABLE\\shell\\open\\command", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Opera.PORTABLE\\shell\\open", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Opera.PORTABLE\\shell", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Opera.PORTABLE\\DefaultIcon", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Opera.PORTABLE\\Application", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Opera.PORTABLE", false);
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\opera.exe", false);
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\RegisteredApplications", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.xhtml\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.xht\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.webp\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.shtml\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.pdf\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.html\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\.htm\\OpenWithProgids", true);
                key.DeleteValue("Opera.PORTABLE", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ApplicationAssociationToasts", true);
                key.DeleteValue("Opera.PORTABLE_http", false);
				key.DeleteValue("Opera.PORTABLE_https", false);
				key.DeleteValue("Opera.PORTABLE_.htm", false);
				key.DeleteValue("Opera.PORTABLE_.html", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice", true);
                key.DeleteValue("Hash", false);
                key.DeleteValue("ProgId", false);
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice", true);                
                key.DeleteValue("Hash", false);
                key.DeleteValue("ProgId", false);
                key.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
