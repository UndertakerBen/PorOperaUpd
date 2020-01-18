using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Globalization;

namespace Portable_Opera_Updater
{
    public partial class Form1 : Form
    {
        private static readonly string[] product = new string[8] { "=www&opsys=Windows&product=Opera+GX", "=www&opsys=Windows&product=Opera developer", "=www&opsys=Windows&product=Opera beta", "=www&opsys=Windows", "=www&opsys=Windows&product=Opera+GX&arch=x64", "=www&opsys=Windows&product=Opera developer&arch=x64", "=www&opsys=Windows&product=Opera beta&arch=x64", "=www&opsys=Windows&arch=x64" };
        private static readonly string[] splitRing = new string[8] { "opera_gx", "opera-developer", "opera-beta", "desktop", "opera_gx", "opera-developer", "opera-beta", "desktop" };
        private static readonly string[] ring = new string[8] { "Opera GX", "Developer", "Beta", "Stable", "Opera GX", "Developer", "Beta", "Stable" };
        private static readonly string[] buildVersion = new string[8];
        private static readonly string[] url = new string[8];
        private static readonly string[] fileName = new string[8] { "Opera-GX-x86.exe", "Opera-Developer-x86.exe", "Opera-Beta-x86.exe", "Opera-Stable-x86.exe", "Opera-GX-x64.exe", "Opera-Developer-x64.exe", "Opera-Beta-x64.exe", "Opera-Stable-x64.exe" };
        private static readonly string[] instDir = new string[9] { "Opera GX x86", "Opera Dev x86", "Opera Beta x86", "Opera Stable x86", "Opera GX x64", "Opera Dev x64", "Opera Beta x64", "Opera Stable x64", "Opera" };
        private static readonly string[] arch = new string[2] { "x86", "x64" };
        private readonly CultureInfo culture1 = CultureInfo.CurrentUICulture;
        private readonly string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private readonly string applicationPath = Application.StartupPath;
        private readonly ToolTip toolTip = new ToolTip();
        public Form1()
        {
            for (int i = 0; i <= 7; i++)
            {
                WebRequest myWebRequest = WebRequest.Create("https://download.opera.com/download/get/?partner" + product[i]);
                WebResponse myWebResponse = myWebRequest.GetResponse();
                string resUrl = myWebResponse.ResponseUri.ToString();
                string sresUrl = resUrl.Substring(resUrl.IndexOf("=id="));
                string[] resid = sresUrl.Split(new char[] { '=', '&' });
                myWebResponse.Close();
                WebRequest myWebRequest2 = WebRequest.Create("https://download.opera.com/download/get/?id=" + resid[2] + "&amp;location=415&amp;nothanks=yes&amp;sub=marine&amp;utm_tryagain=yes");
                WebResponse myWebResponse2 = myWebRequest2.GetResponse();
                string resUrl2 = myWebResponse2.ResponseUri.ToString();
                string sresUrl2 = resUrl2.Substring(resUrl2.IndexOf(splitRing[i]));
                string[] iVersion = sresUrl2.Split(new char[] { '/' });
                buildVersion[i] = iVersion[1];
                url[i] = myWebResponse2.ResponseUri.ToString();
                myWebResponse2.Close();
            }
            InitializeComponent();
            label5.Text = buildVersion[0];
            label6.Text = buildVersion[1];
            label7.Text = buildVersion[2];
            label8.Text = buildVersion[3];
            if (culture1.Name != "de-DE")
            {
                button10.Text = "Quit";
                button9.Text = "Install all";
                label9.Text = "Install all x86 and or x64";
                checkBox4.Text = "Ignore version check";
                checkBox3.Text = "Create a Folder for each version";
                checkBox5.Text = "Create a shortcut on the desktop";
            }
            if (IntPtr.Size != 8)
            {
                button5.Visible = false;
                button6.Visible = false;
                button8.Visible = false;
                button8.Visible = false;
                checkBox2.Visible = false;
            }
            if (IntPtr.Size == 8)
            {
                if ((File.Exists(instDir[4]+ "\\launcher.exe")) || (File.Exists(instDir[5] + "\\launcher.exe")) || (File.Exists(instDir[6] + "\\launcher.exe")) || (File.Exists(instDir[7] + "\\launcher.exe")))
                {
                    checkBox2.Enabled = false;
                }
                if ((File.Exists(instDir[0] + "\\launcher.exe")) || (File.Exists(instDir[1] + "\\launcher.exe")) || (File.Exists(instDir[2] + "\\launcher.exe")) || (File.Exists(instDir[3] + "\\launcher.exe")))
                {
                    checkBox1.Enabled = false;
                }
                if ((File.Exists(instDir[0] + "\\launcher.exe")) || (File.Exists(instDir[1] + "\\launcher.exe")) || (File.Exists(instDir[2] + "\\launcher.exe")) || (File.Exists(instDir[3] + "\\launcher.exe")) || (File.Exists(instDir[4] + "\\launcher.exe")) || (File.Exists(instDir[5] + "\\launcher.exe")) || (File.Exists(instDir[6] + "\\launcher.exe")) || (File.Exists(instDir[7] + "\\launcher.exe")))
                {
                    checkBox3.Checked = true;
                    CheckButton();
                }
                if (!checkBox3.Checked)
                {
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    button9.Enabled = false;
                    button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
                }
                else if (File.Exists(@"Opera\launcher.exe"))
                {
                    CheckButtonSingle();
                }
            }
            else if (IntPtr.Size != 8)
            {
                if ((File.Exists(instDir[0] + "\\launcher.exe")) || (File.Exists(instDir[1] + "\\launcher.exe")) || (File.Exists(instDir[2] + "\\launcher.exe")) || (File.Exists(instDir[3] + "\\launcher.exe")))
                {
                    checkBox3.Checked = true;
                    checkBox1.Enabled = false;
                    CheckButton();
                }
                if (!checkBox3.Checked)
                {
                    checkBox1.Enabled = false;
                    button9.Enabled = false;
                    button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
                }
                else if (File.Exists(@"Opera\launcher.exe"))
                {
                    CheckButtonSingle();
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[0], 0, 0, 1, 10, 11);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[0], 0, 0, 1, 10, 11);
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[1], 1, 0, 2, 13, 12);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[1], 1, 0, 1, 10, 11);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[2], 2, 0, 3, 15, 14);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[2], 2, 0, 1, 10, 11);
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[3], 3, 0, 4, 17, 16);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[3], 3, 0, 1, 10, 11);
            }
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[4], 4, 1, 5, 19, 18);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[4], 4, 1, 1, 10, 11);
            }
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[5], 5, 1, 6, 21, 20);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[5], 5, 1, 1, 10, 11);
            }
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[6], 6, 1, 7, 23, 22);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[6], 6, 1, 1, 10, 11);
            }
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(instDir[7], 7, 1, 8, 25, 24);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(ring[7], 7, 1, 1, 10, 11);
            }
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            if ((!Directory.Exists(instDir[0])) && (!Directory.Exists(instDir[1])) && (!Directory.Exists(instDir[2])) && (!Directory.Exists(instDir[3])) && checkBox1.Checked)
            {
                DownloadFile(0, 0, 1, 10, 11);
                DownloadFile(1, 0, 2, 13, 12);
                DownloadFile(2, 0, 3, 15, 14);
                DownloadFile(3, 0, 4, 17, 16);
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
            }
            NewMethod8(0, 0, 1, 10, 11);
            NewMethod8(1, 0, 2, 13, 12);
            NewMethod8(2, 0, 3, 15, 14);
            NewMethod8(3, 0, 4, 17, 16);
            if (IntPtr.Size == 8)
            {
                if ((!Directory.Exists(instDir[4])) && (!Directory.Exists(instDir[5])) && (!Directory.Exists(instDir[6])) && (!Directory.Exists(instDir[7])) && checkBox2.Checked)
                {
                    DownloadFile(4, 1, 5, 19, 18);
                    DownloadFile(5, 1, 6, 21, 20);
                    DownloadFile(6, 1, 7, 23, 22);
                    DownloadFile(7, 1, 8, 25, 24);
                    checkBox2.Enabled = false;
                    checkBox2.Checked = false;
                }
                NewMethod8(4, 1, 5, 19, 18);
                NewMethod8(5, 1, 6, 21, 20);
                NewMethod8(6, 1, 7, 23, 22);
                NewMethod8(7, 1, 8, 25, 24);
            }
        }
        private void NewMethod8(int a, int b, int c , int d, int e)
        {
            if (Directory.Exists(instDir[a]) && File.Exists(instDir[a] + "\\updates\\Version.log"))
            {
                string instTVersion = File.ReadAllText(instDir[a] + "\\updates\\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                if (instVersion[0] != buildVersion[a])
                {
                    DownloadFile(a, b, c, d, e);
                }
            }
        }
        private void DownloadFile(int a, int b, int c, int d, int e)
        {
            if (checkBox3.Checked)
            {
                WebClient myWebClient = new WebClient();
                Uri uri = new Uri(url[a]);
                var fName = fileName[a];
                using (myWebClient = new WebClient())
                {
                    myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    try
                    {
                        myWebClient.DownloadFileAsync(uri, fName, c + "|" + d + "|" + e + "|" + fName + "|" + instDir[a] + "|" + arch[b] + "|" + ring[a]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (!checkBox3.Checked)
            {
                WebClient myWebClient = new WebClient();
                Uri uri = new Uri(url[a]);
                var fName = fileName[a];
                using (myWebClient = new WebClient())
                {
                    myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    try
                    {
                        myWebClient.DownloadFileAsync(uri, fName, c + "|" + d + "|" + e + "|" + fName + "|" + instDir[8] + "|" + arch[b] + "|" + ring[a]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void Message()
        {
            if (culture1.Name != "de-DE")
            {
                MessageBox.Show("The same version is already installed", "Portable Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                MessageBox.Show("Die selbe Version ist bereits installiert", "Portable Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string i = e.UserState.ToString();
            string[] i2 = i.Split(new char[] { '|' });
            var progressBars = Controls.Find("progressBar" + i2[0], true);
            if (checkBox3.Checked)
            {
                var buttons = Controls.Find("button" + i2[0], true);
                NewMethod6(buttons);
            }
            var label1 = Controls.Find("label" + i2[1], true);
            var label2 = Controls.Find("label" + i2[2], true);
            if (progressBars.Length > 0)
            {
                var progressBar = (ProgressBar)progressBars[0];
                progressBar.Visible = true;
                progressBar.Value = e.ProgressPercentage;
            }
            if (label1.Length > 0)
            {
                var label = (Label)label1[0];
                label.Visible = true;
                label.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            }
            if (label2.Length > 0)
            {
                var label3 = (Label)label2[0];
                label3.Visible = true;
                label3.Text = e.ProgressPercentage.ToString() + "%";
            }
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string i = e.UserState.ToString();
            string[] i2 = i.Split(new char[] { '|' });
            var labels = Controls.Find("label" + i2[1], true);
            if (e.Cancelled == true)
            {
                MessageBox.Show("Download has been canceled.");
            }
            else
            {
                if (labels.Length > 0)
                {
                    var label = (Label)labels[0];
                    if (culture1.Name != "de-DE")
                    {
                        label.Text = "Unpacking";
                    }
                    else
                    {
                        label.Text = "Entpacken";
                    }
                    label.Text = "Entpacken";
                    string arguments = " x " + "\"" + @i2[3] + "\"" + " -o" + "\"" + @"Update\" + i2[4] + "\"" + " -y";
                    Process process = new Process();
                    process.StartInfo.FileName = @"Bin\7zr.exe";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    process.StartInfo.Arguments = arguments;
                    process.Start();
                    process.WaitForExit();
                    if ((File.Exists(@"Update\" + i2[4] + "\\launcher.exe")) && (File.Exists(@i2[4] + "\\updates\\Version.log")))
                    {
                        string instTVersion = File.ReadAllText(i2[4] + "\\updates\\Version.log");
                        string[] instVersion = NewMethod(instTVersion);
                        FileVersionInfo testm = FileVersionInfo.GetVersionInfo(applicationPath + "\\Update\\" + i2[4] + "\\launcher.exe");
                        if (checkBox3.Checked)
                        {
                            if (testm.FileVersion != instVersion[0])
                            {
                                NewMethod2(i2, instVersion, testm);
                            }
                            else if ((testm.FileVersion == instVersion[0]) && (checkBox4.Checked))
                            {
                                NewMethod2(i2, instVersion, testm);
                            }
                        }
                        else if (!checkBox3.Checked)
                        {
                            NewMethod2(i2, instVersion, testm);
                        }
                    }
                    else
                    {
                        FileVersionInfo testm = FileVersionInfo.GetVersionInfo(applicationPath + "\\Update\\" + i2[4] + "\\launcher.exe");
                        NewMethod7(i2, testm);
                    }
                    if (checkBox5.Checked)
                    {
                        if (!File.Exists(deskDir + "\\" + i2[4] + ".lnk"))
                        {
                            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                            IWshRuntimeLibrary.IWshShortcut link = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(deskDir + "\\" + i2[4] + ".lnk");
                            link.IconLocation = applicationPath + "\\" + i2[4] + "\\launcher.exe,0";
                            link.WorkingDirectory = applicationPath;
                            link.TargetPath = applicationPath + "\\" + i2[4] + " Launcher.exe";
                            link.Save();
                        }
                    }
                    if (!File.Exists(applicationPath + "\\" + i2[4] + " Launcher.exe"))
                    {
                        File.Copy(@"Bin\Launcher\" + i2[4] + " Launcher.exe", applicationPath + "\\" + i2[4] + " Launcher.exe");
                    }
                    if (File.Exists(path: @i2[3]))
                    {
                        File.Delete(path: @i2[3]);
                    }
                    if (checkBox3.Checked)
                    {
                        CheckButton();
                    }
                    else if (!checkBox3.Checked)
                    {
                        CheckButtonSingle();
                    }
                    if (culture1.Name != "de-DE")
                    {
                        label.Text = "Unpacked";
                    }
                    else
                    {
                        label.Text = "Entpackt";
                    }
                }
            }
        }
        private static void NewMethod2(string[] i2, string[] instVersion, FileVersionInfo testm)
        {
            if (Directory.Exists(@i2[4] + "\\Assets"))
            {
                Directory.Delete(@i2[4] + "\\Assets", true);
            }
            if (Directory.Exists(i2[4] + "\\" + instVersion[0]))
            {
                Directory.Delete(i2[4] + "\\" + instVersion[0], true);
            }
            Thread.Sleep(500);
            NewMethod7(i2, testm);
        }
        private static void NewMethod7(string[] i2, FileVersionInfo testm)
        {
            if (!Directory.Exists(i2[4]))
            {
                Directory.CreateDirectory(i2[4]);
            }
            if (!Directory.Exists(i2[4] + "\\updates"))
            {
                Directory.CreateDirectory(i2[4] + "\\updates");
            }
            Thread.Sleep(500);
            File.Copy(@"Update\" + i2[4] + "\\launcher.exe", @i2[4] + "\\launcher.exe", true);
            File.Copy(@"Update\" + i2[4] + "\\launcher.visualelementsmanifest.xml", @i2[4] + "\\launcher.visualelementsmanifest.xml", true);
            File.Copy(@"Update\" + i2[4] + "\\Resources.pri", @i2[4] + "\\Resources.pri", true);
            File.Delete(@"Update\" + i2[4] + "\\launcher.exe");
            File.Delete(@"Update\" + i2[4] + "\\launcher.visualelementsmanifest.xml");
            File.Delete(@"Update\" + i2[4] + "\\Resources.pri");
            Directory.Move(@"Update\" + i2[4] + "\\Assets", @i2[4] + "\\Assets");
            Directory.Move(@"Update\" + i2[4], @i2[4] + "\\" + testm.FileVersion);
            File.WriteAllText(i2[4] + "\\updates\\Version.log", testm.FileVersion + "|" + i2[6] + "|" + i2[5]);
        }
        private void CheckButton()
        {
            NewMethod1();
            for (int i = 0; i <= 7; i++)
            {
                if (File.Exists(@instDir[i] + "\\updates\\Version.log"))
                {
                    var buttons = Controls.Find("button" + (i + 1), true);
                    string instTVersion = File.ReadAllText(@instDir[i] + "\\updates\\Version.log");
                    string[] instVersion = NewMethod(instTVersion);
                    if (buildVersion[i] == instVersion[0])
                    {
                        if (buttons.Length > 0)
                        {
                            var button = (Button)buttons[0];
                            button.BackColor = Color.Green;
                        }
                    }
                    else if (buildVersion[i] != instVersion[0])
                    {
                        if (culture1.Name != "de-DE")
                        {
                            button9.Text = "Update all";
                        }
                        else
                        {
                            button9.Text = "Alle Updaten";
                        }
                        button9.Enabled = true;
                        button9.BackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        if (buttons.Length > 0)
                        {
                            var button = (Button)buttons[0];
                            button.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
        private void CheckButtonSingle()
        {
            NewMethod1();
            if (File.Exists(@"Opera\updates\Version.log"))
            {
                string instTVersion = File.ReadAllText(@"Opera\updates\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                switch (instVersion[1])
                {
                    case "Opera GX":
                        NewMethod3(instVersion, 1, 5, 0);

                        break;
                    case "Developer":
                        NewMethod3(instVersion, 2, 6, 1);

                        break;
                    case "Beta":
                        NewMethod3(instVersion, 3, 7, 2);

                        break;
                    case "Stable":
                        NewMethod3(instVersion, 4, 8, 3);

                        break;
                }
            }
        }
        private void NewMethod1()
        {
            for (int i = 1; i <= 8; i++)
            {
                var buttons = Controls.Find("button" + i, true);
                if (buttons.Length > 0)
                {
                    var button = (Button)buttons[0];
                    button.BackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224))))); ;
                }
            }
        }
        private void NewMethod3(string[] instVersion, int a, int b, int c)
        {
            var buttons = Controls.Find("button" + a, true);
            var buttons2 = Controls.Find("button" + b, true);
            if (instVersion[0] == buildVersion[c])
            {
                if (instVersion[2] == "x86")
                {
                    if (buttons.Length > 0)
                    {
                        var button = (Button)buttons[0];
                        button.BackColor = Color.Green;
                    }
                }
                else if (instVersion[2] == "x64")
                {
                    if (buttons2.Length > 0)
                    {
                        var button = (Button)buttons2[0];
                        button.BackColor = Color.Green;
                    }
                }
            }
            else if (instVersion[0] != buildVersion[c])
            {
                if (instVersion[2] == "x86")
                {
                    if (buttons.Length > 0)
                    {
                        var button = (Button)buttons[0];
                        button.BackColor = Color.Red;
                    }
                }
                else if (instVersion[2] == "x64")
                {
                    if (buttons2.Length > 0)
                    {
                        var button = (Button)buttons2[0];
                        button.BackColor = Color.Red;
                    }
                }
            }
        }
        private static string[] NewMethod(string instTVersion)
        {
            return instTVersion.Split(new char[] { '|' });
        }
        private void NewMethod4(string instDir, int a, int b, int c, int d, int e)
        {
            if (File.Exists(@instDir + "\\updates\\Version.log"))
            {
                string instTVersion = File.ReadAllText(@instDir + "\\updates\\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                if (instVersion[0] == buildVersion[b])
                {
                    if (checkBox4.Checked)
                    {
                        DownloadFile(a, b, c, d, e);
                    }
                    else
                    {
                        Message();
                    }
                }
                else
                {
                    DownloadFile(a, b, c, d, e);
                }
            }
            else
            {
                DownloadFile(a, b, c, d, e);
            }
        }
        private void NewMethod5(string ring, int a, int b, int c, int d, int e)
        {
            var buttons = Controls.Find("button" + (a + 1), true);
            if (File.Exists(@"Opera\updates\Version.log"))
            {
                string instTVersion = File.ReadAllText(@"Opera\updates\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                if ((instVersion[0] == buildVersion[a]) && (instVersion[1] == ring) && (instVersion[2] == arch[b]))
                {
                    if (checkBox4.Checked)
                    {
                        DownloadFile(a, b, c, d, e);
                        NewMethod6(buttons);
                    }
                    else
                    {
                        Message();
                    }
                }
                else
                {
                    DownloadFile(a, b, c, d, e);
                    NewMethod6(buttons);
                }
            }
            else
            {
                DownloadFile(a, b, c, d, e);
                NewMethod6(buttons);
            }
        }
        private static void NewMethod6(Control[] buttons)
        {
            if (buttons.Length > 0)
            {
                var button = (Button)buttons[0];
                button.BackColor = Color.Orange;
            }
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                if ((File.Exists(instDir[4] + "\\launcher.exe")) || (File.Exists(instDir[5] + "\\launcher.exe")) || (File.Exists(instDir[6] + "\\launcher.exe")) || (File.Exists(instDir[7] + "\\launcher.exe")))
                {
                    checkBox2.Enabled = false;
                }
                else
                {
                    checkBox2.Enabled = true;
                }
                if ((File.Exists(instDir[0] + "\\launcher.exe")) || (File.Exists(instDir[1] + "\\launcher.exe")) || (File.Exists(instDir[2] + "\\launcher.exe")) || (File.Exists(instDir[3] + "\\launcher.exe")))
                {
                    checkBox1.Enabled = false;
                }
                else
                {
                    checkBox1.Enabled = true;
                }
                if (button9.Enabled)
                {
                    button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                }
                CheckButton();
            }
            if (!checkBox3.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                button9.Enabled = false;
                button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
                CheckButtonSingle();
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button9.Enabled = true;
                button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            }
            else if ((!checkBox1.Checked) && (!checkBox2.Checked))
            {
                button9.Enabled = false;
                button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            }
        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button9.Enabled = true;
                button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            }
            else if ((!checkBox1.Checked) && (!checkBox2.Checked))
            {
                button9.Enabled = false;
                button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            }
        }
        private void Button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(@"Update"))
            {
                Directory.Delete(@"Update", true);
            }
        }
        private void Button1_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(0, "x86");
        }
        private void Button2_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(1, "x86");
        }
        private void Button3_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(2, "x86");
        }
        private void Button4_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(3, "x86");
        }
        private void Button5_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(4, "x64");
        }
        private void Button6_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(5, "x64");
        }
        private void Button7_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(6, "x64");
        }
        private void Button8_MouseHover(object sender, EventArgs e)
        {
            NewMethod9(7, "x64");
        }
        private void NewMethod9(int a, string arch)
        {
            var buttons = Controls.Find("button" + (a + 1), true);
            var button = (Button)buttons[0];
            if (!checkBox3.Checked)
            {
                if (File.Exists(@"Opera\updates\Version.log"))
                {
                    string instTVersion = File.ReadAllText(@"Opera\updates\Version.log");
                    string[] instVersion = NewMethod(instTVersion);
                    NewMethod10(a, arch, button, instVersion);
                }
            }
            if (checkBox3.Checked)
            {
                if (File.Exists(instDir[a] + "\\updates\\Version.log"))
                {
                    string instTVersion = File.ReadAllText(instDir[a] + "\\updates\\Version.log");
                    string[] instVersion = NewMethod(instTVersion);
                    NewMethod10(a, arch, button, instVersion);
                }
            }
        }
        private void NewMethod10(int a, string arch, Button button, string[] instVersion)
        {
            if ((instVersion[1] == ring[a]) && (instVersion[2] == arch))
            {
                toolTip.SetToolTip(button, instVersion[0]);
            }
            else
            {
                toolTip.SetToolTip(button, String.Empty);
            }
        }
    }
}
          