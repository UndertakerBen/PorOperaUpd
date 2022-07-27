using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace Portable_Opera_Updater
{
    public partial class Form1 : Form
    {
        private static readonly string[] splitRing = new string[8] { "Opera_GX_", "Opera_Developer_", "Opera_beta_", "Opera_", "Opera_GX_", "Opera_Developer_", "Opera_beta_", "Opera_" };
        private static readonly string[,] product = new string[8, 3] { { "v4", "gx/Stable", "i386" }, { "v2", "Developer", "i386" }, { "v2", "Beta", "i386" }, { "v2", "Stable", "i386" }, { "v4", "gx/Stable", "x64" }, { "v2", "Developer", "x64" }, { "v2", "Beta", "x64" }, { "v2", "Stable", "x64" } };
        private static readonly string[,] urlbase = new string[8, 2] { { "opera_gx", "" }, { "opera-developer", "" }, { "opera-beta", "" }, { "opera/desktop", "" }, { "opera_gx", "_x64" }, { "opera-developer", "_x64" }, { "opera-beta", "_x64" }, { "opera/desktop", "_x64" } };
        private static readonly string[] ring = new string[8] { "Opera GX", "Developer", "Beta", "Stable", "Opera GX", "Developer", "Beta", "Stable" };
        private static readonly string[] buildVersion = new string[8];
        private static readonly string[] url = new string[8];
        private static readonly string[] instDir = new string[9] { "Opera GX x86", "Opera Dev x86", "Opera Beta x86", "Opera Stable x86", "Opera GX x64", "Opera Dev x64", "Opera Beta x64", "Opera Stable x64", "Opera" };
        private static readonly string[] arch = new string[2] { "x86", "x64" };
        private readonly CultureInfo culture1 = CultureInfo.CurrentUICulture;
        private readonly string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private readonly string applicationPath = Application.StartupPath;
        private readonly string[] CommandLineArgs = Environment.GetCommandLineArgs();
        private readonly ToolTip toolTip = new ToolTip();
        public Form1()
        {
            try
            {
                for (int i = 0; i <= 7; i++)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string postData = "NzI2ODkyZmJiODE4NTA0MmE1YzY3Y2MyNDhmMTZhNzQ2Yjc0OGYyYjE0NGY1YzRhZjJkM2RiOTU5YzQ1ZmRiMDp7ImNvdW50cnkiOiJERSIsImh0dHBfcmVmZXJyZXIiOiJodHRwczovL3d3dy5vcGVyYS5jb20vZGUvY29tcHV0ZXIvdGhhbmtzP25pPWVhcGd4Jm9zPXdpbmRvd3MiLCJpbnN0YWxsZXJfbmFtZSI6Ik9wZXJhR1hTZXR1cC5leGUiLCJwcm9kdWN0Ijoib3BlcmFfZ3giLCJxdWVyeSI6Ii9vcGVyYV9neC9zdGFibGUvd2luZG93cz91dG1fdHJ5YWdhaW49eWVzJnV0bV9zb3VyY2U9Z29vZ2xlX3ZpYV9vcGVyYV9jb20mdXRtX21lZGl1bT1vc2UmdXRtX2NhbXBhaWduPShub25lKV92aWFfb3BlcmFfY29tX2h0dHBzJmh0dHBfcmVmZXJyZXI9aHR0cHMlM0ElMkYlMkZ3d3cuZ29vZ2xlLmNvbSUyRiZ1dG1fc2l0ZT1vcGVyYV9jb20mdXRtX2xhc3RwYWdlPW9wZXJhLmNvbS9neCZkbF90b2tlbj0zNDkyNzQxNSIsInRpbWVzdGFtcCI6IjE2MTQ0MTg0NzcuNzExMCIsInVzZXJhZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIEFwcGxlV2ViS2l0LzUzNy4zNiAoS0hUTUwsIGxpa2UgR2Vja28pIENocm9tZS84OC4wLjQzMjQuMTUwIFNhZmFyaS81MzcuMzYgT1BSLzc0LjAuMzkxMS4xNjAiLCJ1dG0iOnsiY2FtcGFpZ24iOiIobm9uZSlfdmlhX29wZXJhX2NvbV9odHRwcyIsImxhc3RwYWdlIjoib3BlcmEuY29tL2d4IiwibWVkaXVtIjoib3NlIiwic2l0ZSI6Im9wZXJhX2NvbSIsInNvdXJjZSI6Imdvb2dsZV92aWFfb3BlcmFfY29tIiwidHJ5YWdhaW4iOiJ5ZXMifSwidXVpZCI6IjJjZTM4MDM0LWNlYzYtNDdlOS1iZDE4LTg0MmFlNTM4MjJhZSJ9";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://autoupdate.geo.opera.com/{product[i, 0]}/netinstaller/{product[i, 1]}/windows/{product[i, 2]}");
                    request.Method = "POST";
                    //request.UserAgent = "Opera NetInstaller/74.0.3911.160";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (dataStream = request.GetResponse().GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        string version = responseFromServer.Substring(responseFromServer.IndexOf("installer_filename\": \"")).Replace("installer_filename\": \"", "").Split(new char[] { ',' }, 2)[0].Replace(splitRing[i], "").Split(new char[] { '_' }, 2)[0];
                        url[i] = responseFromServer.Substring(responseFromServer.IndexOf("installer\": \"https")).Replace("installer\": \"", "").Split(new char[] { ',' }, 2)[0];
                        buildVersion[i] = version;
                        reader.Close();
                        dataStream.Close();
                    }
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
                if (File.Exists($"{applicationPath}\\{instDir[4]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[5]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[6]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[7]}\\launcher.exe"))
                {
                    checkBox2.Enabled = false;
                }
                if (File.Exists($"{applicationPath}\\{instDir[0]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[1]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[2]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[3]}\\launcher.exe"))
                {
                    checkBox1.Enabled = false;
                }
                if (File.Exists($"{applicationPath}\\{instDir[0]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[1]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[2]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[3]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[4]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[5]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[6]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[7]}\\launcher.exe"))
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

                    if (File.Exists(applicationPath + "\\Opera\\launcher.exe"))
                    {
                        CheckButtonSingle();
                    }
                }
            }
            else if (IntPtr.Size != 8)
            {
                if (File.Exists($"{applicationPath}\\{instDir[0]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[1]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[2]}\\launcher.exe") || File.Exists($"{applicationPath}\\{instDir[3]}\\launcher.exe"))
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
                    if (File.Exists($"{applicationPath}\\Opera\\launcher.exe"))
                    {
                        CheckButtonSingle();
                    }
                }
            }
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Equals("opera"))
                {
                    MessageBox.Show(Langfile.Texts("MeassageRunning"), "Portable Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            _ = TestCheck();
        }
        private async Task TestCheck()
        {
            await CheckUpdate();
            for (int i = 0; i < CommandLineArgs.GetLength(0); i++)
            {
                if (CommandLineArgs[i].ToLower().Equals("-updateall"))
                {
                    await UpdateAll();
                    await Task.Delay(2000);
                    Application.Exit();
                }
            }
        }
        private async void Button1_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(0, 0, 1, 0);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(0, 0, 1, 8);
            }
        }
        private async void Button2_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(1, 0, 2, 1);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(1, 0, 2, 8);
            }
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(2, 0, 3, 2);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(2, 0, 3, 8);
            }
        }
        private async void Button4_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(3, 0, 4, 3);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(3, 0, 4, 8);
            }
        }
        private async void Button5_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(4, 1, 5, 4);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(4, 1, 5, 8);
            }
        }
        private async void Button6_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(5, 1, 6, 5);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(5, 1, 6, 8);
            }
        }
        private async void Button7_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(6, 1, 7, 6);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(6, 1, 7, 8);
            }
        }
        private async void Button8_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                NewMethod4(7, 1, 8, 7);
            }
            else if (!checkBox3.Checked)
            {
                await NewMethod5(7, 1, 8, 8);
            }
        }
        private async void Button9_Click(object sender, EventArgs e)
        {
            await Testing();
        }
        private async Task Testing()
        {
            if ((!Directory.Exists($"{applicationPath}\\{instDir[0]}")) && (!Directory.Exists($"{applicationPath}\\{instDir[1]}")) && (!Directory.Exists($"{applicationPath}\\{instDir[2]}")) && (!Directory.Exists($"{applicationPath}\\{instDir[3]}")) && checkBox1.Checked)
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
                if ((!Directory.Exists($"{applicationPath}\\{instDir[4]}")) && (!Directory.Exists($"{applicationPath}\\{instDir[5]}")) && (!Directory.Exists($"{applicationPath}\\{instDir[6]}")) && (!Directory.Exists($"{applicationPath}\\{instDir[7]}")) && checkBox2.Checked)
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
        private async Task UpdateAll()
        {
            if (Directory.Exists($"{applicationPath}\\Opera"))
            {
                if (File.Exists($"{applicationPath}\\Opera\\updates\\Version.log"))
                {
                    string[] instVersion = File.ReadAllText($"{applicationPath}\\Opera\\updates\\Version.log").Split(new char[] { '|' });
                    if (instVersion[1] == "Opera GX" & instVersion[2] == "x86")
                    {
                        if (new Version(buildVersion[0]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(0, 0, 1, 8);
                        }
                    }
                    if (instVersion[1] == "Developer" & instVersion[2] == "x86")
                    {
                        if (new Version(buildVersion[1]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(1, 0, 2, 8);
                        }
                    }
                    if (instVersion[1] == "Beta" & instVersion[2] == "x86")
                    {
                        if (new Version(buildVersion[2]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(2, 0, 3, 8);
                        }
                    }
                    if (instVersion[1] == "Stable" & instVersion[2] == "x86")
                    {
                        if (new Version(buildVersion[3]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(3, 0, 4, 8);
                        }
                    }
                    if (instVersion[1] == "Opera GX" & instVersion[2] == "x64")
                    {
                        if (new Version(buildVersion[4]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(4, 1, 5, 8);
                        }
                    }
                    if (instVersion[1] == "Developer" & instVersion[2] == "x64")
                    {
                        if (new Version(buildVersion[5]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(5, 1, 6, 8);
                        }
                    }
                    if (instVersion[1] == "Beta" & instVersion[2] == "x64")
                    {
                        if (new Version(buildVersion[6]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(6, 1, 7, 8);
                        }
                    }
                    if (instVersion[1] == "Stable" & instVersion[2] == "x64")
                    {
                        if (new Version(buildVersion[7]) > new Version(instVersion[0]))
                        {
                            await NewMethod5(7, 1, 8, 8);
                        }
                    }
                }
            }
            await Testing();
            await Task.WhenAll();
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
                Text = $"Opera {ring[a]} {buildVersion[a]} {arch[b]}",
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
                            downloadLabel.Text = Langfile.Texts("downUnpstart");
                            string arguments = $" x \"{applicationPath}\\Opera_{buildVersion[a]}_{ring[a]}_{arch[b]}.exe\" -o\"{applicationPath}\\Update\\{instDir[d]}\" -y";
                            Process process = new Process();
                            process.StartInfo.FileName = $"{applicationPath}\\Bin\\7zr.exe";
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            process.StartInfo.Arguments = arguments;
                            process.Start();
                            process.WaitForExit();
                            if ((File.Exists($"{applicationPath}\\Update\\{instDir[d]}\\launcher.exe")) && (File.Exists($"{applicationPath}\\{instDir[d]}\\updates\\Version.log")))
                            {
                                string instTVersion = File.ReadAllText($"{applicationPath}\\{instDir[d]}\\updates\\Version.log");
                                string[] instVersion = File.ReadAllText($"{applicationPath}\\{instDir[d]}\\updates\\Version.log").Split(new char[] { '|' });
                                FileVersionInfo testm = FileVersionInfo.GetVersionInfo($"{applicationPath}\\Update\\{instDir[d]}\\launcher.exe");
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
                                FileVersionInfo testm = FileVersionInfo.GetVersionInfo($"{applicationPath}\\Update\\{instDir[d]}\\launcher.exe");
                                NewMethod6(a, b, d, testm);
                            }
                            if (checkBox5.Checked)
                            {
                                if (!File.Exists($"{deskDir}\\{instDir[d]}.lnk"))
                                {
                                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                                    IWshRuntimeLibrary.IWshShortcut link = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut($"{deskDir}\\{instDir[d]}.lnk");
                                    link.IconLocation = $"{applicationPath}\\{instDir[d]}\\launcher.exe,0";
                                    link.WorkingDirectory = applicationPath;
                                    link.TargetPath = $"{applicationPath}\\{instDir[d]} Launcher.exe";
                                    link.Save();
                                }
                            }
                            if (!File.Exists($"{applicationPath}\\{instDir[d]} Launcher.exe"))
                            {
                                File.Copy($"{@applicationPath}\\Bin\\Launcher\\{instDir[d]} Launcher.exe", $"{applicationPath}\\{instDir[d]} Launcher.exe");
                            }
                            if (File.Exists($"{applicationPath}\\Opera_{buildVersion[a]}_{ring[a]}_{arch[b]}.exe"))
                            {
                                File.Delete($"{applicationPath}\\Opera_{buildVersion[a]}_{ring[a]}_{arch[b]}.exe");
                            }
                            downloadLabel.Text = Langfile.Texts("downUnpfine");
                        }
                    };
                    try
                    {
                        var task = myWebClient.DownloadFileTaskAsync(uri, $"{applicationPath}\\Opera_{buildVersion[a]}_{ring[a]}_{arch[b]}.exe");
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
            MessageBox.Show(Langfile.Texts("MeassageVersion"), "Portabel Opera Updater", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void CheckButton()
        {
            NewMethod1();
            for (int i = 0; i <= 7; i++)
            {
                if (File.Exists($"{applicationPath}\\{instDir[i]}\\updates\\Version.log"))
                {
                    var buttons = Controls.Find("button" + (i + 1), true);
                    string[] instVersion = File.ReadAllText($"{applicationPath}\\{instDir[i]}\\updates\\Version.log").Split(new char[] { '|' });
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
                        button9.Text = Langfile.Texts("Button9UAll");
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
            if (File.Exists($"{applicationPath}\\Opera\\updates\\Version.log"))
            {
                string[] instVersion = File.ReadAllText($"{applicationPath}\\Opera\\updates\\Version.log").Split(new char[] { '|' });
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
                checkBox2.Enabled = !File.Exists($"{applicationPath}\\{instDir[4]}\\launcher.exe") && !File.Exists($"{applicationPath}\\{instDir[5]}\\launcher.exe") && !File.Exists($"{applicationPath}\\{instDir[6]}\\launcher.exe") && !File.Exists($"{applicationPath}\\{instDir[7]}\\launcher.exe");
                checkBox1.Enabled = !File.Exists($"{applicationPath}\\{instDir[0]}\\launcher.exe") && !File.Exists($"{applicationPath}\\{instDir[1]}\\launcher.exe") && !File.Exists($"{applicationPath}\\{instDir[2]}\\launcher.exe") && !File.Exists($"{applicationPath}\\{instDir[3]}\\launcher.exe");
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
            if (Directory.Exists($"{applicationPath}\\Update"))
            {
                Directory.Delete($"{applicationPath}\\Update", true);
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
            if (Directory.Exists($"{applicationPath}\\{instDir[d]}\\Assets"))
            {
                Directory.Delete($"{applicationPath}\\{instDir[d]}\\Assets", true);
            }
            if (Directory.Exists($"{applicationPath}\\{instDir[d]}\\{instVersion[0]}"))
            {
                Directory.Delete($"{applicationPath}\\{instDir[d]}\\{instVersion[0]}", true);
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
                switch (instVersion[2])
                {
                    case "x86":
                        {
                            if (buttons.Length > 0)
                            {
                                var button = (Button)buttons[0];
                                button.BackColor = Color.Green;
                            }
                            break;
                        }
                    case "x64":
                        {
                            if (buttons2.Length > 0)
                            {
                                var button = (Button)buttons2[0];
                                button.BackColor = Color.Green;
                            }
                            break;
                        }
                }
            }
            else if (instVersion[0] != buildVersion[c])
            {
                switch (instVersion[2])
                {
                    case "x86":
                        {
                            if (buttons.Length > 0)
                            {
                                var button = (Button)buttons[0];
                                button.BackColor = Color.Red;
                            }
                            break;
                        }
                    case "x64":
                        {
                            if (buttons2.Length > 0)
                            {
                                var button = (Button)buttons2[0];
                                button.BackColor = Color.Red;
                            }
                            break;
                        }
                }
            }
        }
        private async void NewMethod4(int a, int b, int c, int d)
        {
            if (File.Exists($"{applicationPath}\\{instDir[a]}\\updates\\Version.log"))
            {
                string[] instVersion = File.ReadAllText($"{applicationPath}\\{instDir[a]}\\updates\\Version.log").Split(new char[] { '|' });
                if (instVersion[0] == buildVersion[a])
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
        private async Task NewMethod5(int a, int b, int c, int d)
        {
            if (File.Exists($"{applicationPath}\\Opera\\updates\\Version.log"))
            {
                string[] instVersion = File.ReadAllText($"{applicationPath}\\Opera\\updates\\Version.log").Split(new char[] { '|' });
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
            if (!Directory.Exists($"{applicationPath}\\{instDir[d]}"))
            {
                Directory.CreateDirectory($"{applicationPath}\\{instDir[d]}");
            }
            if (!Directory.Exists($"{applicationPath}\\{instDir[d]}\\updates"))
            {
                Directory.CreateDirectory($"{applicationPath}\\{instDir[d]}\\updates");
            }
            Thread.Sleep(500);
            File.Copy($"{applicationPath}\\Update\\{instDir[d]}\\launcher.exe", $"{applicationPath}\\{instDir[d]}\\launcher.exe", true);
            File.Copy($"{applicationPath}\\Update\\{instDir[d]}\\launcher.visualelementsmanifest.xml", $"{applicationPath}\\{instDir[d]}\\launcher.visualelementsmanifest.xml", true);
            File.Copy($"{applicationPath}\\Update\\{instDir[d]}\\Resources.pri", $"{applicationPath}\\{instDir[d]}\\Resources.pri", true);
            File.Delete($"{applicationPath}\\Update\\{instDir[d]}\\launcher.exe");
            File.Delete($"{applicationPath}\\Update\\{instDir[d]}\\launcher.visualelementsmanifest.xml");
            File.Delete($"{applicationPath}\\Update\\{instDir[d]}\\Resources.pri");
            Directory.Move($"{applicationPath}\\Update\\{instDir[d]}\\Assets", $"{applicationPath}\\{instDir[d]}\\Assets");
            Directory.Move($"{applicationPath}\\Update\\{instDir[d]}", $"{applicationPath}\\{instDir[d]}\\{testm.FileVersion}");
            File.WriteAllText($"{applicationPath}\\{instDir[d]}\\updates\\Version.log", testm.FileVersion + "|" + ring[a] + "|" + arch[b]);
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
            if (Directory.Exists($"{applicationPath}\\{instDir[a]}") && File.Exists($"{applicationPath}\\{instDir[a]}\\updates\\Version.log"))
            {
                string[] instVersion = File.ReadAllText($"{applicationPath}\\{instDir[a]}\\updates\\Version.log").Split(new char[] { '|' });
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
                if (File.Exists($"{applicationPath}\\Opera\\updates\\Version.log"))
                {
                    string[] instVersion = File.ReadAllText($"{applicationPath}\\Opera\\updates\\Version.log").Split(new char[] { '|' });
                    NewMethod9(a, arch, button, instVersion);
                }
            }
            if (checkBox3.Checked)
            {
                if (File.Exists($"{applicationPath}\\{instDir[a]}\\updates\\Version.log"))
                {
                    string[] instVersion = File.ReadAllText($"{applicationPath}\\{instDir[a]}\\updates\\Version.log").Split(new char[] { '|' });
                    NewMethod9(a, arch, button, instVersion);
                }
            }
        }
        private void NewMethod9(int a, string arch, Button button, string[] instVersion)
        {
            if ((instVersion[1] == ring[a]) && (instVersion[2] == arch))
            {
                toolTip.SetToolTip(button, instVersion[0]);
                toolTip.IsBalloon = true;
            }
            else
            {
                toolTip.SetToolTip(button, String.Empty);
            }
        }
        private async Task CheckUpdate()
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
            infoLabel.Text = Langfile.Texts("infoLabel");
            laterButton.Text = Langfile.Texts("laterButton");
            updateButton.Text = Langfile.Texts("updateButton");
            downLabel.Text = Langfile.Texts("downLabel");
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
                    Version version = new Version(reader.ReadToEnd());
                    Version testm = new Version(FileVersionInfo.GetVersionInfo(applicationPath + "\\Portable Opera Updater.exe").FileVersion);
                    versionLabel.Text = testm + "  >>> " + version;
                    if (version > testm)
                    {
                        for (int i = 0; i < CommandLineArgs.GetLength(0); i++)
                        {
                            if (CommandLineArgs[i].ToLower().Equals("-updateall"))
                            {
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                using (WebClient myWebClient2 = new WebClient())
                                {
                                    myWebClient2.DownloadFile($"https://github.com/UndertakerBen/PorOperaUpd/releases/download/v{version}/Portable.Opera.Updater.v{version}.7z", $"{applicationPath}\\Portable.Opera.Updater.v{version}.7z");
                                }
                                File.AppendAllText(applicationPath + "\\Update.cmd", "@echo off" + "\r\n" +
                                    "timeout /t 5 /nobreak" + "\r\n" +
                                    "\"" + applicationPath + "\\Bin\\7zr.exe\" e \"" + applicationPath + "\\Portable.Opera.Updater.v" + version + ".7z\" -o\"" + applicationPath + "\" \"Portable Opera Updater.exe\"" + " -y\r\n" +
                                    "call cmd /c Start /b \"\" " + "\"" + applicationPath + "\\Portable Opera Updater.exe\" -UpdateAll\r\n" +
                                    "del /f /q \"" + applicationPath + "\\Portable.Opera.Updater.v" + version + ".7z\"\r\n" +
                                    "del /f /q \"" + applicationPath + "\\Update.cmd\" && exit\r\n" +
                                    "exit\r\n");

                                string arguments = " /c call \"" + applicationPath + "\\Update.cmd\"";
                                Process process = new Process();
                                process.StartInfo.FileName = "cmd.exe";
                                process.StartInfo.Arguments = arguments;
                                process.Start();
                                Close();
                                await Task.Delay(2000);
                            }
                        }
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
                        myWebClient2.DownloadFile($"https://github.com/UndertakerBen/PorOperaUpd/releases/download/v{version}/Portable.Opera.Updater.v{version}.7z", $"{applicationPath}\\Portable.Opera.Updater.v{version}.7z");
                    }
                    File.AppendAllText(applicationPath + "\\Update.cmd", "@echo off" + "\r\n" +
                        "timeout /t 1 /nobreak" + "\r\n" +
                        "\"" + applicationPath + "\\Bin\\7zr.exe\" e \"" + applicationPath + "\\Portable.Opera.Updater.v" + version + ".7z\" -o\"" + applicationPath + "\" \"Portable Opera Updater.exe\"" + " -y\r\n" +
                        "call cmd /c Start /b \"\" " + "\"" + applicationPath + "\\Portable Opera Updater.exe\"\r\n" +
                        "del /f /q \"" + applicationPath + "\\Portable.Opera.Updater.v" + version + ".7z\"\r\n" +
                        "del /f /q \"" + applicationPath + "\\Update.cmd\" && exit\r\n" +
                        "exit\r\n");

                    string arguments = " /c call \"" + applicationPath + "\\Update.cmd\"";
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
                                myWebClient2.DownloadFile("https://github.com/UndertakerBen/PorOperaUpd/raw/master/Launcher/Launcher.7z", applicationPath + "\\Launcher.7z");
                            }
                            string arguments = " x \"" + applicationPath + "\\Launcher.7z" + " -o\"" + applicationPath + "\\Bin\\Launcher\" -y";
                            Process process = new Process();
                            process.StartInfo.FileName = applicationPath + "\\Bin\\7zr.exe";
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            process.StartInfo.Arguments = arguments;
                            process.Start();
                            process.WaitForExit();
                            File.Delete(@"Launcher.7z");
                            foreach (string launcher in instDir)
                            {
                                if (File.Exists(applicationPath + "\\" + launcher + " Launcher.exe"))
                                {
                                    Version binLauncher = new Version(FileVersionInfo.GetVersionInfo(applicationPath + "\\Bin\\Launcher\\" + launcher + " Launcher.exe").FileVersion);
                                    Version istLauncher = new Version(FileVersionInfo.GetVersionInfo(applicationPath + "\\" + launcher + " Launcher.exe").FileVersion);
                                    if (binLauncher > istLauncher)
                                    {
                                        File.Copy(applicationPath + "\\bin\\Launcher\\" + launcher + " Launcher.exe", applicationPath + "\\" + launcher + " Launcher.exe", true);
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
            await Task.WhenAll();
        }
        private void VersionsInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version updVersion = new Version(FileVersionInfo.GetVersionInfo(applicationPath + "\\Portable Opera Updater.exe").FileVersion);
            Version launcherVersion = new Version(FileVersionInfo.GetVersionInfo(applicationPath + "\\Bin\\Launcher\\Opera Launcher.exe").FileVersion);
            MessageBox.Show("Updater Version - " + updVersion + "\r\nLauncher Version - " + launcherVersion, "Version Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void RegistrierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[8]);
        }
        private void RegistrierenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[3]);
        }
        private void RegistrierenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[7]);
        }
        private void RegistrierenToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[2]);
        }
        private void RegistrierenToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[6]);
        }
        private void RegistrierenToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[1]);
        }
        private void RegistrierenToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[5]);
        }
        private void RegistrierenToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[0]);
        }
        private void RegistrierenToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Regfile.RegCreate(applicationPath, instDir[4]);
        }
        private void EntfernenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem1.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem2.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem3.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem4.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem5.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem6.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem7.Enabled = true;
            Regfile.RegDel();
        }
        private void EntfernenToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            registrierenToolStripMenuItem8.Enabled = true;
            Regfile.RegDel();
        }
        private void ExtrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey key;
                if (Microsoft.Win32.Registry.GetValue("HKEY_Current_User\\Software\\Clients\\StartMenuInternet\\Opera.PORTABLE", default, null) != null)
                {
                    key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Clients\\StartMenuInternet\\Opera.PORTABLE", false);
                    switch (key.GetValue(default).ToString())
                    {
                        case "Opera Portable":
                            key.Close();
                            registrierenToolStripMenuItem.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera Stable x86 Portable":
                            key.Close();
                            registrierenToolStripMenuItem1.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera Stable x64 Portable":
                            key.Close();
                            registrierenToolStripMenuItem2.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera Beta x86 Portable":
                            key.Close();
                            registrierenToolStripMenuItem3.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera Beta x64 Portable":
                            key.Close();
                            registrierenToolStripMenuItem4.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera Dev x86 Portable":
                            key.Close();
                            registrierenToolStripMenuItem5.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera Dev x64 Portable":
                            key.Close();
                            registrierenToolStripMenuItem6.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera GX x86 Portable":
                            key.Close();
                            registrierenToolStripMenuItem7.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            break;
                        case "Opera GX x64 Portable":
                            key.Close();
                            registrierenToolStripMenuItem8.Enabled = false;
                            operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                            operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                            break;
                    }
                }
                else
                {
                    if (Directory.Exists(@"Opera"))
                    {
                        operaAlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaAlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera Stable x86"))
                    {
                        operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaStableX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera Stable x64"))
                    {
                        operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaStableX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera Beta x86"))
                    {
                        operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaBetaX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera Beta x64"))
                    {
                        operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaBetaX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera Dev x86"))
                    {
                        operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaDeveloperX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera Dev x64"))
                    {
                        operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaDeveloperX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera GX x86"))
                    {
                        operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaGXX86AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                    if (Directory.Exists(@"Opera GX x64"))
                    {
                        operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        operaGXX64AlsStandardbrowserToolStripMenuItem.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
          