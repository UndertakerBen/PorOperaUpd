using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;

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
            try
            {
                for (int i = 0; i <= 7; i++)
                {
                    WebRequest myWebRequest = WebRequest.Create("https://download.opera.com/download/get/?partner" + product[i]);
                    WebResponse myWebResponse = myWebRequest.GetResponse();
                    string resUrl = myWebResponse.ResponseUri.ToString();
                    string sresUrl = resUrl.Substring(resUrl.IndexOf("=id="));
                    string[] resid = sresUrl.Split(new char[] { '=', '&', '%' });
                    myWebResponse.Close();
                    WebRequest myWebRequest2 = WebRequest.Create("https://download.opera.com/download/get/?id=" + resid[2] + "&amp;location=415&amp;nothanks=yes");
                    WebResponse myWebResponse2 = myWebRequest2.GetResponse();
                    string resUrl2 = myWebResponse2.ResponseUri.ToString();
                    string sresUrl2 = resUrl2.Substring(resUrl2.IndexOf(splitRing[i]));
                    string[] iVersion = sresUrl2.Split(new char[] { '/' });
                    buildVersion[i] = iVersion[1];
                    url[i] = myWebResponse2.ResponseUri.ToString();
                    myWebResponse2.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
            label5.Text = buildVersion[0];
            label6.Text = buildVersion[1];
            label7.Text = buildVersion[2];
            label8.Text = buildVersion[3];
            switch (culture1.TwoLetterISOLanguageName)
            {
                case "ru":
                    button10.Text = "Выход";
                    button9.Text = "Установить все";
                    label9.Text = "Установить все версии x86 и/или x64";
                    checkBox4.Text = "Игнорировать проверку версии";
                    checkBox3.Text = "Разные версии в отдельных папках";
                    checkBox5.Text = "Создать ярлык на рабочем столе";
                    break;
                case "de":
                    button10.Text = "Beenden";
                    button9.Text = "Alle Installieren";
                    label9.Text = "Alle x86 und oder x64 installieren";
                    checkBox4.Text = "Versionkontrolle ignorieren";
                    checkBox3.Text = "Für jede Version einen eigenen Ordner";
                    checkBox5.Text = "Eine Verknüpfung auf dem Desktop erstellen";
                    break;
                default:
                    button10.Text = "Quit";
                    button9.Text = "Install all";
                    label9.Text = "Install all x86 and or x64";
                    checkBox4.Text = "Ignore version check";
                    checkBox3.Text = "Create a Folder for each version";
                    checkBox5.Text = "Create a shortcut on the desktop";
                    break;
            }
            if (IntPtr.Size != 8)
            {
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
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
                    button9.BackColor = Color.FromArgb(244, 244, 244);
                    CheckButton();
                }
                else if (!checkBox3.Checked)
                {
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    button9.Enabled = false;
                    button9.BackColor = Color.FromArgb(244, 244, 244);

                    if (File.Exists(@"Opera\launcher.exe"))
                    {
                        CheckButtonSingle();
                    }
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
                else if (!checkBox3.Checked)
                {
                    checkBox1.Enabled = false;
                    button9.Enabled = false;
                    button9.BackColor = Color.FromArgb(244, 244, 244);
                    if (File.Exists(@"Opera\launcher.exe"))
                    {
                        CheckButtonSingle();
                    }
                }
            }
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Equals("opera"))
                {
                    switch (culture1.TwoLetterISOLanguageName)
                    {
                        case "ru":
                            {
                                MessageBox.Show("Необходимо закрыть Opera перед обновлением.", "Portable Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        case "de":
                            {
                                MessageBox.Show("Bitte schließen Sie den laufenden Opera, bevor Sie den Browser aktualisieren.", "Portable Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        default:
                            {
                                MessageBox.Show("Please close the running Opera before updating the browser.", "Portable Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                    }
                }
            }
            CheckUpdate();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(0, 0, 1, 0);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(0, 0, 1, 8);
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(1, 0, 2, 1);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(1, 0, 2, 8);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(2, 0, 3, 2);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(2, 0, 3, 8);
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(3, 0, 4, 3);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(3, 0, 4, 8);
            }
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(4, 1, 5, 4);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(4, 1, 5, 8);
            }
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(5, 1, 6, 5);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(5, 1, 6, 8);
            }
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(6, 1, 7, 6);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(6, 1, 7, 8);
            }
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(7, 1, 8, 7);
            }
            else if (!checkBox3.Checked)
            {
                NewMethod5(7, 1, 8, 8);
            }
        }
        private async void Button9_Click(object sender, EventArgs e)
        {
            await Testing();
        }
        private async Task Testing()
        {
            if ((!Directory.Exists(instDir[0])) && (!Directory.Exists(instDir[1])) && (!Directory.Exists(instDir[2])) && (!Directory.Exists(instDir[3])) && checkBox1.Checked)
            {
                await DownloadFile(0, 0, 1, 0);
                await DownloadFile(1, 0, 2, 1);
                await DownloadFile(2, 0, 3, 2);
                await DownloadFile(3, 0, 4, 3);
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
            }
            await NewMethod7(0, 0, 1, 0);
            await NewMethod7(1, 0, 2, 1);
            await NewMethod7(2, 0, 3, 2);
            await NewMethod7(3, 0, 4, 3);
            if (IntPtr.Size == 8)
            {
                if ((!Directory.Exists(instDir[4])) && (!Directory.Exists(instDir[5])) && (!Directory.Exists(instDir[6])) && (!Directory.Exists(instDir[7])) && checkBox2.Checked)
                {
                    await DownloadFile(4, 1, 5, 4);
                    await DownloadFile(5, 1, 6, 5);
                    await DownloadFile(6, 1, 7, 6);
                    await DownloadFile(7, 1, 8, 7);
                    checkBox2.Enabled = false;
                    checkBox2.Checked = false;
                }
                await NewMethod7(4, 1, 5, 4);
                await NewMethod7(5, 1, 6, 5);
                await NewMethod7(6, 1, 7, 6);
                await NewMethod7(7, 1, 8, 7);
            }
        }
        private async Task DownloadFile(int a, int b, int c, int d)
        {
            GroupBox progressBox = new GroupBox
            {
                Location = new Point(10, button10.Location.Y + button10.Height + 5),
                Size = new Size(groupBox3.Width, 90),
                BackColor = Color.Lavender,
            };
            Label title = new Label
            {
                AutoSize = false,
                Location = new Point(2, 10),
                Size = new Size(progressBox.Width - 4, 25),
                Text = "Opera " + ring[a] + " " + buildVersion[a] + " " + arch[b],
                TextAlign = ContentAlignment.BottomCenter
            };
            title.Font = new Font(title.Font.Name, 9.25F, FontStyle.Bold);
            Label downloadLabel = new Label
            {
                AutoSize = false,
                Location = new Point(5, 35),
                Size = new Size(200, 25),
                TextAlign = ContentAlignment.BottomLeft
            };
            Label percLabel = new Label
            {
                AutoSize = false,
                Location = new Point(progressBox.Size.Width - 105, 35),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.BottomRight
            };
            ProgressBar progressBarneu = new ProgressBar
            {
                Location = new Point(5, 65),
                Size = new Size(progressBox.Size.Width - 10, 7)
            };
            progressBox.Controls.Add(title);
            progressBox.Controls.Add(downloadLabel);
            progressBox.Controls.Add(percLabel);
            progressBox.Controls.Add(progressBarneu);
            Controls.Add(progressBox);
            List<Task> list = new List<Task>();

            //if (checkBox3.Checked)
            //{
                WebClient myWebClient = new WebClient();
                Uri uri = new Uri(url[a]);
                ServicePoint sp = ServicePointManager.FindServicePoint(uri);
                sp.ConnectionLimit = 1;
                using (myWebClient = new WebClient())
                {
                    myWebClient.DownloadProgressChanged += (o, args) =>
                    {
                        Control[] buttons = Controls.Find("button" + c, true);
                        if (buttons.Length > 0)
                        {
                            Button button = (Button)buttons[0];
                            button.BackColor = Color.Orange;
                        }
                        progressBarneu.Value = args.ProgressPercentage;
                        downloadLabel.Text = string.Format("{0} MB's / {1} MB's",
                            (args.BytesReceived / 1024d / 1024d).ToString("0.00"),
                            (args.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
                        percLabel.Text = args.ProgressPercentage.ToString() + "%";
                    };
                    myWebClient.DownloadFileCompleted += (o, args) =>
                    {
                        if (args.Cancelled == true)
                        {
                            MessageBox.Show("Download has been canceled.");
                        }
                        else
                        {
                            switch (culture1.TwoLetterISOLanguageName)
                            {
                                case "ru":
                                    downloadLabel.Text = "Распаковка";
                                    break;
                                case "de":
                                    downloadLabel.Text = "Entpacken";
                                    break;
                                default:
                                    downloadLabel.Text = "Unpacking";
                                    break;
                            }
                            string arguments = " x " + "\"" + "Opera_" + buildVersion[a] + "_" + ring[a] + "_" + arch[b] + ".exe" + "\"" + " -o" + "\"" + @"Update\" + instDir[d] + "\"" + " -y";
                            Process process = new Process();
                            process.StartInfo.FileName = @"Bin\7zr.exe";
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            process.StartInfo.Arguments = arguments;
                            process.Start();
                            process.WaitForExit();
                            if ((File.Exists(@"Update\" + instDir[d] + "\\launcher.exe")) && (File.Exists(instDir[d] + "\\updates\\Version.log")))
                            {
                                string instTVersion = File.ReadAllText(instDir[d] + "\\updates\\Version.log");
                                string[] instVersion = NewMethod(instTVersion);
                                FileVersionInfo testm = FileVersionInfo.GetVersionInfo(applicationPath + "\\Update\\" + instDir[d] + "\\launcher.exe");
                                if (checkBox3.Checked)
                                {
                                    if (testm.FileVersion != instVersion[0])
                                    {
                                        NewMethod2(a, b, d, instVersion, testm);
                                    }
                                    else if ((testm.FileVersion == instVersion[0]) && (checkBox4.Checked))
                                    {
                                        NewMethod2(a, b, d, instVersion, testm);
                                    }
                                }
                                else if (!checkBox3.Checked)
                                {
                                    NewMethod2(a, b, d, instVersion, testm);
                                }
                            }
                            else
                            {
                                FileVersionInfo testm = FileVersionInfo.GetVersionInfo(applicationPath + "\\Update\\" + instDir[d] + "\\launcher.exe");
                                NewMethod6(a, b, d, testm);
                            }
                            if (checkBox5.Checked)
                            {
                                if (!File.Exists(deskDir + "\\" + instDir[d] + ".lnk"))
                                {
                                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                                    IWshRuntimeLibrary.IWshShortcut link = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(deskDir + "\\" + instDir[d] + ".lnk");
                                    link.IconLocation = applicationPath + "\\" + instDir[d] + "\\launcher.exe,0";
                                    link.WorkingDirectory = applicationPath;
                                    link.TargetPath = applicationPath + "\\" + instDir[d] + " Launcher.exe";
                                    link.Save();
                                }
                            }
                            if (!File.Exists(applicationPath + "\\" + instDir[d] + " Launcher.exe"))
                            {
                                File.Copy(@"Bin\Launcher\" + instDir[d] + " Launcher.exe", applicationPath + "\\" + instDir[d] + " Launcher.exe");
                            }
                            if (File.Exists(path: "Opera_" + buildVersion[a] + "_" + ring[a] + "_" + arch[b] + ".exe"))
                            {
                                File.Delete(path: "Opera_" + buildVersion[a] + "_" + ring[a] + "_" + arch[b] + ".exe");
                            }
                            switch (culture1.TwoLetterISOLanguageName)
                            {
                                case "ru":
                                    downloadLabel.Text = "Распакованный";
                                    break;
                                case "de":
                                    downloadLabel.Text = "Entpackt";
                                    break;
                                default:
                                    downloadLabel.Text = "Unpacked";
                                    break;
                            }
                        }
                    };
                    try
                    {
                        var task = myWebClient.DownloadFileTaskAsync(uri, "Opera_" + buildVersion[a] + "_" + ring[a] + "_" + arch[b] + ".exe");
                        list.Add(task);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            await Task.WhenAll(list);
            await Task.Delay(2000);
            Controls.Remove(progressBox);
            //}
        }
        private void Message()
        {
            switch (culture1.TwoLetterISOLanguageName)
            {
                case "ru":
                    MessageBox.Show("Данная версия уже установлена", "Portabel Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                case "de":
                    MessageBox.Show("Die selbe Version ist bereits installiert", "Portabel Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                default:
                    MessageBox.Show("The same version is already installed", "Portabel Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
            }
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
                        switch (culture1.TwoLetterISOLanguageName)
                        {
                            case "ru":
                                button9.Text = "Обновить все";
                                break;
                            case "de":
                                button9.Text = "Alle Updaten";
                                break;
                            default:
                                button9.Text = "Update all";
                                break;
                        }
                        button9.Enabled = true;
                        button9.BackColor = Color.FromArgb(224, 224, 224);
                        if (buttons.Length > 0)
                        {
                            var button = (Button)buttons[0];
                            button.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
        public void CheckButtonSingle()
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
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button9.Enabled = true;
                button9.BackColor = Color.FromArgb(224, 224, 224);
            }
            else if ((!checkBox1.Checked) && (!checkBox2.Checked))
            {
                button9.Enabled = false;
                button9.BackColor = Color.FromArgb(244, 244, 244);
            }
        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button9.Enabled = true;
                button9.BackColor = Color.FromArgb(224, 224, 224);
            }
            else if ((!checkBox1.Checked) && (!checkBox2.Checked))
            {
                button9.Enabled = false;
                button9.BackColor = Color.FromArgb(244, 244, 244);
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
                    button9.BackColor = Color.FromArgb(224, 224, 224);
                }
                CheckButton();
            }
            if (!checkBox3.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                button9.Enabled = false;
                button9.BackColor = Color.FromArgb(244, 244, 244);
                CheckButtonSingle();
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
            NewMethod8(0, "x86");
        }
        private void Button2_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(1, "x86");
        }
        private void Button3_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(2, "x86");
        }
        private void Button4_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(3, "x86");
        }
        private void Button5_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(4, "x64");
        }
        private void Button6_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(5, "x64");
        }
        private void Button7_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(6, "x64");
        }
        private void Button8_MouseHover(object sender, EventArgs e)
        {
            NewMethod8(7, "x64");
        }
        private static string[] NewMethod(string instTVersion)
        {
            return instTVersion.Split(new char[] { '|' });
        }
        private void NewMethod1()
        {
            for (int i = 1; i <= 8; i++)
            {
                var buttons = Controls.Find("button" + i, true);
                if (buttons.Length > 0)
                {
                    var button = (Button)buttons[0];
                    button.BackColor = Color.FromArgb(224, 224, 224); ;
                }
            }
        }
        private void NewMethod2(int a, int b, int d, string[] instVersion, FileVersionInfo testm)
        {
            if (Directory.Exists(instDir[d] + "\\Assets"))
            {
                Directory.Delete(instDir[d] + "\\Assets", true);
            }
            if (Directory.Exists(instDir[d] + "\\" + instVersion[0]))
            {
                Directory.Delete(instDir[d] + "\\" + instVersion[0], true);
            }
            Thread.Sleep(500);
            NewMethod6(a, b, d, testm);
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
        private async void NewMethod4(int a, int b, int c, int d)
        {
            if (File.Exists(@instDir + "\\updates\\Version.log"))
            {
                string instTVersion = File.ReadAllText(@instDir[a] + "\\updates\\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                if (instVersion[0] == buildVersion[b])
                {
                    if (checkBox4.Checked)
                    {
                        await DownloadFile(a, b, c, d);
                    }
                    else
                    {
                        Message();
                    }
                }
                else
                {
                    await DownloadFile(a, b, c, d);
                }
            }
            else
            {
                await DownloadFile(a, b, c, d);
            }
        }
        private async void NewMethod5(int a, int b, int c, int d)
        {
            var buttons = Controls.Find("button" + (a + 1), true);
            if (File.Exists(@"Opera\updates\Version.log"))
            {
                string instTVersion = File.ReadAllText(@"Opera\updates\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                if ((instVersion[0] == buildVersion[a]) && (instVersion[1] == ring[a]) && (instVersion[2] == arch[b]))
                {
                    if (checkBox4.Checked)
                    {
                        await DownloadFile(a, b, c, d);
                    }
                    else
                    {
                        Message();
                    }
                }
                else
                {
                    await DownloadFile(a, b, c, d);
                }
            }
            else
            {
                await DownloadFile(a, b, c, d);
            }
        }
        private void NewMethod6(int a, int b, int d, FileVersionInfo testm)
        {
            if (!Directory.Exists(instDir[d]))
            {
                Directory.CreateDirectory(instDir[d]);
            }
            if (!Directory.Exists(instDir[d] + "\\updates"))
            {
                Directory.CreateDirectory(instDir[d] + "\\updates");
            }
            Thread.Sleep(500);
            File.Copy(@"Update\" + instDir[d] + "\\launcher.exe", instDir[d] + "\\launcher.exe", true);
            File.Copy(@"Update\" + instDir[d] + "\\launcher.visualelementsmanifest.xml", instDir[d] + "\\launcher.visualelementsmanifest.xml", true);
            File.Copy(@"Update\" + instDir[d] + "\\Resources.pri", instDir[d] + "\\Resources.pri", true);
            File.Delete(@"Update\" + instDir[d] + "\\launcher.exe");
            File.Delete(@"Update\" + instDir[d] + "\\launcher.visualelementsmanifest.xml");
            File.Delete(@"Update\" + instDir[d] + "\\Resources.pri");
            Directory.Move(@"Update\" + instDir[d] + "\\Assets", instDir[d] + "\\Assets");
            Directory.Move(@"Update\" + instDir[d], instDir[d] + "\\" + testm.FileVersion);
            File.WriteAllText(instDir[d] + "\\updates\\Version.log", testm.FileVersion + "|" + ring[a] + "|" + arch[b]);
            if (checkBox3.Checked)
            {
                CheckButton();
            }
            else if (!checkBox3.Checked)
            {
                CheckButtonSingle();
            }
        }
        private async Task NewMethod7(int a, int b, int c, int d)
        {
            if (Directory.Exists(instDir[a]) && File.Exists(instDir[a] + "\\updates\\Version.log"))
            {
                string instTVersion = File.ReadAllText(instDir[a] + "\\updates\\Version.log");
                string[] instVersion = NewMethod(instTVersion);
                if (instVersion[0] != buildVersion[a])
                {
                    await DownloadFile(a, b, c, d);
                }
            }
        }
        private void NewMethod8(int a, string arch)
        {
            var buttons = Controls.Find("button" + (a + 1), true);
            var button = (Button)buttons[0];
            if (!checkBox3.Checked)
            {
                if (File.Exists(@"Opera\updates\Version.log"))
                {
                    string instTVersion = File.ReadAllText(@"Opera\updates\Version.log");
                    string[] instVersion = NewMethod(instTVersion);
                    NewMethod9(a, arch, button, instVersion);
                }
            }
            if (checkBox3.Checked)
            {
                if (File.Exists(instDir[a] + "\\updates\\Version.log"))
                {
                    string instTVersion = File.ReadAllText(instDir[a] + "\\updates\\Version.log");
                    string[] instVersion = NewMethod(instTVersion);
                    NewMethod9(a, arch, button, instVersion);
                }
            }
        }
        private void NewMethod9(int a, string arch, Button button, string[] instVersion)
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
        private void CheckUpdate()
        {
            GroupBox groupBoxupdate = new GroupBox
            {
                Location = new Point(groupBox3.Location.X, button10.Location.Y + button10.Size.Height + 5),
                Size = new Size(groupBox3.Width, 90),
                BackColor = Color.Aqua
            };
            Label versionLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.BottomCenter,
                Dock = DockStyle.None,
                Location = new Point(2, 30),
                Size = new Size(groupBoxupdate.Width - 4, 25),
            };
            versionLabel.Font = new Font(versionLabel.Font.Name, 10F, FontStyle.Bold);
            Label infoLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.BottomCenter,
                Dock = DockStyle.None,
                Location = new Point(2, 10),
                Size = new Size(groupBoxupdate.Width - 4, 20),
            };
            infoLabel.Font = new Font(infoLabel.Font.Name, 8.75F);
            Label downLabel = new Label
            {
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false,
                Size = new Size(100, 23),
            };
            Button laterButton = new Button
            {
                Size = new Size(40, 23),
                BackColor = Color.FromArgb(224, 224, 224)
            };
            Button updateButton = new Button
            {
                Location = new Point(groupBoxupdate.Width - Width - 10, 60),
                Size = new Size(40, 23),
                BackColor = Color.FromArgb(224, 224, 224)
            };
            updateButton.Location = new Point(groupBoxupdate.Width - updateButton.Width - 10, 60);
            laterButton.Location = new Point(updateButton.Location.X - laterButton.Width - 5, 60);
            downLabel.Location = new Point(laterButton.Location.X - downLabel.Width - 20, 60);
            groupBoxupdate.Controls.Add(updateButton);
            groupBoxupdate.Controls.Add(laterButton);
            groupBoxupdate.Controls.Add(downLabel);
            groupBoxupdate.Controls.Add(infoLabel);
            groupBoxupdate.Controls.Add(versionLabel);
            updateButton.Click += new EventHandler(UpdateButton_Click);
            laterButton.Click += new EventHandler(LaterButton_Click);
            switch (culture1.TwoLetterISOLanguageName)
            {
                case "ru":
                    infoLabel.Text = "Доступна новая версия";
                    laterButton.Text = "нет";
                    updateButton.Text = "Да";
                    downLabel.Text = "ОБНОВИТЬ";
                    break;
                case "de":
                    infoLabel.Text = "Eine neue Version ist verfügbar";
                    laterButton.Text = "Nein";
                    updateButton.Text = "Ja";
                    downLabel.Text = "Jetzt Updaten";
                    break;
                default:
                    infoLabel.Text = "A new version is available";
                    laterButton.Text = "No";
                    updateButton.Text = "Yes";
                    downLabel.Text = "Update now";
                    break;
            }
            void LaterButton_Click(object sender, EventArgs e)
            {
                groupBoxupdate.Dispose();
                Controls.Remove(groupBoxupdate);
                groupBox3.Enabled = true;
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                var request = (WebRequest)HttpWebRequest.Create("https://github.com/UndertakerBen/PorOperaUpd/raw/master/Version.txt");
                var response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var version = reader.ReadToEnd();
                    versionLabel.Text = version;
                    FileVersionInfo testm = FileVersionInfo.GetVersionInfo(applicationPath + "\\Portable Opera Updater.exe");
                    if (Convert.ToInt32(version.Replace(".", "")) > Convert.ToInt32(testm.FileVersion.Replace(".", "")))
                    {
                        Controls.Add(groupBoxupdate);
                        groupBox3.Enabled = false;
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {

            }
            void UpdateButton_Click(object sender, EventArgs e)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request2 = (WebRequest)HttpWebRequest.Create("https://github.com/UndertakerBen/PorOperaUpd/raw/master/Version.txt");
                var response2 = request2.GetResponse();
                using (StreamReader reader = new StreamReader(response2.GetResponseStream()))
                {
                    var version = reader.ReadToEnd();
                    reader.Close();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (WebClient myWebClient2 = new WebClient())
                    {
                        myWebClient2.DownloadFile($"https://github.com/UndertakerBen/PorOperaUpd/releases/download/v{version}/Portable.Opera.Updater.v{version}.7z", @"Portable.Opera.Updater.v" + version + ".7z");
                    }
                    File.AppendAllText(@"Update.cmd", "@echo off" + "\n" +
                        "timeout /t 1 /nobreak" + "\n" +
                        "\"" + applicationPath + "\\Bin\\7zr.exe\" e \"" + applicationPath + "\\Portable.Opera.Updater.v" + version + ".7z\" -o\"" + applicationPath + "\" \"Portable Opera Updater.exe\"" + " -y\n" +
                        "call cmd /c Start /b \"\" " + "\"" + applicationPath + "\\Portable Opera Updater.exe\"\n" +
                        "del /f /q \"" + applicationPath + "\\Portable.Opera.Updater.v" + version + ".7z\"\n" +
                        "del /f /q \"" + applicationPath + "\\Update.cmd\" && exit\n" +
                        "exit\n");

                    string arguments = " /c call Update.cmd";
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = arguments;
                    process.Start();
                    Close();
                }
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                var request = (WebRequest)HttpWebRequest.Create("https://github.com/UndertakerBen/PorOperaUpd/raw/master/Launcher/Version.txt");
                var response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var version = reader.ReadToEnd();
                    FileVersionInfo testm = FileVersionInfo.GetVersionInfo(applicationPath + "\\Bin\\Launcher\\Opera Launcher.exe");
                    if (Convert.ToInt32(version.Replace(".", "")) > Convert.ToInt32(testm.FileVersion.Replace(".", "")))
                    {
                        reader.Close();
                        try
                        {
                            using (WebClient myWebClient2 = new WebClient())
                            {
                                myWebClient2.DownloadFile("https://github.com/UndertakerBen/PorOperaUpd/raw/master/Launcher/Launcher.7z", @"Launcher.7z");
                            }
                            string arguments = " x " + @"Launcher.7z" + " -o" + @"Bin\\Launcher" + " -y";
                            Process process = new Process();
                            process.StartInfo.FileName = @"Bin\7zr.exe";
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            process.StartInfo.Arguments = arguments;
                            process.Start();
                            process.WaitForExit();
                            File.Delete(@"Launcher.7z");
                            foreach (string launcher in instDir)
                            {
                                if (File.Exists(launcher + " Launcher.exe"))
                                {
                                    FileVersionInfo binLauncher = FileVersionInfo.GetVersionInfo(applicationPath + "\\Bin\\Launcher\\" + launcher + " Launcher.exe");
                                    FileVersionInfo istLauncher = FileVersionInfo.GetVersionInfo(applicationPath + "\\" + launcher + " Launcher.exe");
                                    if (Convert.ToDecimal(binLauncher.FileVersion) > Convert.ToDecimal(istLauncher.FileVersion))
                                    {
                                        File.Copy(@"bin\\Launcher\\" + launcher + " Launcher.exe", launcher + " Launcher.exe", true);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
          