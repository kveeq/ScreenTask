using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenTask
{
    public partial class frmMain : Form
    {
        private bool isWorking;
        private bool isTakingScreenshots;
        private bool isPrivateTask;
        private bool isPreview;
        private bool isMouseCapture;

        private object locker = new object();
        private ReaderWriterLock rwl = new ReaderWriterLock();
        private MemoryStream img;
        private List<Tuple<string, string>> _ips;
        HttpListener serv;
        public frmMain()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // For Visual Studio Debuging Only !
            serv = new HttpListener();
            serv.IgnoreWriteExceptions = true; // Seems Had No Effect :(
            img = new MemoryStream();
            isPrivateTask = false;
            isPreview = false;
            isMouseCapture = false;
        }

        private async void btnStartServer_Click(object sender, EventArgs e)
        {

            if (btnStartServer.Tag.ToString() != "start")
            {
                btnStartServer.Tag = "start";
                if (LanguageChoosing.Text == "русский (Россия)")
                    btnStartServer.Text = ServerStart_ru.SrartServerRU;
                else
                    btnStartServer.Text = ServerStart_en.StartServerEN;
                isWorking = false;
                isTakingScreenshots = false;
                Log("Server Stoped.");
                return;
            }

            try
            {
                serv.IgnoreWriteExceptions = true;
                isTakingScreenshots = true;
                isWorking = true;
                Log("Starting Server, Please Wait...");
                await AddFirewallRule((int)numPort.Value);
                Task.Factory.StartNew(() => CaptureScreenEvery((int)numShotEvery.Value)).Wait();
                btnStartServer.Tag = "stop";
                if (LanguageChoosing.Text == "русский (Россия)")
                    btnStartServer.Text = Server_ru.ServerRU;
                else
                    btnStartServer.Text = Server_en.ServerEN;
                await StartServer();

            }
            catch (ObjectDisposedException disObj)
            {
                serv = new HttpListener();
                serv.IgnoreWriteExceptions = true;
            }
            catch (Exception ex)
            {
                Log("Error! : " + ex.Message);
            }
        }
        private async Task StartServer()
        {
            //serv = serv??new HttpListener();
            var selectedIP = _ips.ElementAt(comboIPs.SelectedIndex).Item2;

            var url = string.Format("http://{0}:{1}", selectedIP, numPort.Value.ToString());
            txtURL.Text = url;
            serv.Prefixes.Clear();
            serv.Prefixes.Add("http://localhost:" + numPort.Value.ToString() + "/");
            //serv.Prefixes.Add("http://*:" + numPort.Value.ToString() + "/"); // Uncomment this to Allow Public IP Over Internet. [Commented for Security Reasons.]
            serv.Prefixes.Add(url + "/");
            serv.Start();
            Log("Server Started Successfuly!");
            Log("Private Network URL : " + url);
            Log("Localhost URL : " + "http://localhost:" + numPort.Value.ToString() + "/");
            while (isWorking)
            {
                var ctx = await serv.GetContextAsync();
                //Screenshot();
                var resPath = ctx.Request.Url.LocalPath;
                if (resPath == "/") // Route The Root Dir to the Index Page
                {
                    resPath += "index.html";

                }
                var page = Application.StartupPath + "/WebServer" + resPath;
                if (OnceTranslation.Checked)
                {
                    page = Application.StartupPath + "/OtherWebserver" + resPath;
                }
                else if (!OnceTranslation.Checked)
                {
                    page = Application.StartupPath + "/WebServer" + resPath;
                }
                bool fileExist;
                lock (locker)
                    fileExist = File.Exists(page);
                if (!fileExist)
                {
                    var errorPage = Encoding.UTF8.GetBytes("<h1 style=\"color:red\">Error 404 , File Not Found </h1><hr><a href=\".\\\">Back to Home</a>");
                    ctx.Response.ContentType = "text/html";
                    ctx.Response.StatusCode = 404;
                    try
                    {
                        await ctx.Response.OutputStream.WriteAsync(errorPage, 0, errorPage.Length);
                    }
                    catch (Exception ex)
                    {


                    }
                    ctx.Response.Close();
                    continue;
                }


                if (isPrivateTask)
                {
                    if (!ctx.Request.Headers.AllKeys.Contains("Authorization"))
                    {
                        ctx.Response.StatusCode = 401;
                        ctx.Response.AddHeader("WWW-Authenticate", "Basic realm=Screen Task Authentication : ");
                        ctx.Response.Close();
                        continue;
                    }
                    else
                    {
                        var auth1 = ctx.Request.Headers["Authorization"];
                        auth1 = auth1.Remove(0, 6); // Remove "Basic " From The Header Value
                        auth1 = Encoding.UTF8.GetString(Convert.FromBase64String(auth1));
                        var auth2 = string.Format("{0}:{1}", txtUser.Text, txtPassword.Text);
                        if (auth1 != auth2)
                        {
                            // MessageBox.Show(auth1+"\r\n"+auth2);
                            Log(string.Format("Bad Login from {0} using {1}", ctx.Request.RemoteEndPoint.Address.ToString(), auth1));
                            var errorPage = Encoding.UTF8.GetBytes("<h1 style=\"color:red\">Not Authorized !!! </h1><hr><a href=\"./\">Back to Home</a>");
                            ctx.Response.ContentType = "text/html";
                            ctx.Response.StatusCode = 401;
                            ctx.Response.AddHeader("WWW-Authenticate", "Basic realm=Screen Task Authentication : ");
                            try
                            {
                                await ctx.Response.OutputStream.WriteAsync(errorPage, 0, errorPage.Length);
                            }
                            catch (Exception ex)
                            {


                            }
                            ctx.Response.Close();
                            continue;
                        }

                    }
                }

                //Everything OK! ??? Then Read The File From HDD as Bytes and Send it to the Client 
                byte[] filedata;

                // Required for One-Time Access of the file {Reader\Writer Problem in OS}
                rwl.AcquireReaderLock(Timeout.Infinite);
                filedata = File.ReadAllBytes(page);
                rwl.ReleaseReaderLock();

                var fileinfo = new FileInfo(page);
                if (fileinfo.Extension == ".css") // important for IE -> Content-Type must be defiend for CSS files unless will ignored !!!
                    ctx.Response.ContentType = "text/css";
                else if (fileinfo.Extension == ".html" || fileinfo.Extension == ".htm")
                    ctx.Response.ContentType = "text/html"; // Important For Chrome Otherwise will display the HTML as plain text.



                ctx.Response.StatusCode = 200;
                try
                {
                    await ctx.Response.OutputStream.WriteAsync(filedata, 0, filedata.Length);
                }
                catch (Exception ex)
                {

                    /*
                        Do Nothing !!! this is the Only Effective Solution for this Exception : 
                        the specified network name is no longer available
                     */

                }

                ctx.Response.Close();
            }

        }
        private async Task CaptureScreenEvery(int msec)
        {
            while (isWorking)
            {
                if (isTakingScreenshots)
                {
                    TakeScreenshot(isMouseCapture);
                    msec = (int)numShotEvery.Value;
                    await Task.Delay(msec);
                }

            }
        }
        private void TakeScreenshot(bool captureMouse)
        {
            if (captureMouse)
            {
                var bmp = ScreenCapturePInvoke.CaptureFullScreen(true);
                rwl.AcquireWriterLock(Timeout.Infinite);
                if (OnceTranslation.Checked)
                {
                    bmp.Save(Application.StartupPath + "/OtherWebserver" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                }
                else if (!OnceTranslation.Checked)
                {
                    bmp.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                }
                rwl.ReleaseWriterLock();
                if (isPreview)
                {
                    img = new MemoryStream();
                    bmp.Save(img, ImageFormat.Jpeg);
                    imgPreview.Image = new Bitmap(img);
                }
                return;
            }
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                rwl.AcquireWriterLock(Timeout.Infinite);
                if (OnceTranslation.Checked)
                {
                    bitmap.Save(Application.StartupPath + "/OtherWebserver" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                }
                else if (!OnceTranslation.Checked)
                {
                    bitmap.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                }

                rwl.ReleaseWriterLock();

                if (isPreview)
                {
                    img = new MemoryStream();
                    bitmap.Save(img, ImageFormat.Jpeg);
                    imgPreview.Image = new Bitmap(img);
                }
            }
        }
        private string GetIPv4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
        private List<Tuple<string, string>> GetAllIPv4Addresses()
        {
            List<Tuple<string, string>> ipList = new List<Tuple<string, string>>();
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                foreach (var ua in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ua.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipList.Add(Tuple.Create(ni.Name, ua.Address.ToString()));
                    }
                }
            }
            return ipList;
        }
        private Task AddFirewallRule(int port)
        {
            return Task.Run(() =>
            {

                string cmd = RunCMD("netsh advfirewall firewall show rule \"Screen Task\"");
                if (cmd.StartsWith("\r\nNo rules match the specified criteria."))
                {
                    cmd = RunCMD("netsh advfirewall firewall add rule name=\"Screen Task\" dir=in action=allow remoteip=localsubnet protocol=tcp localport=" + port);
                    if (cmd.Contains("Ok."))
                    {
                        Log("Screen Task Rule added to your firewall");
                    }
                }
                else
                {
                    cmd = RunCMD("netsh advfirewall firewall delete rule name=\"Screen Task\"");
                    cmd = RunCMD("netsh advfirewall firewall add rule name=\"Screen Task\" dir=in action=allow remoteip=localsubnet protocol=tcp localport=" + port);
                    if (cmd.Contains("Ok."))
                    {
                        Log("Screen Task Rule updated to your firewall");
                    }
                }
            });

        }
        private string RunCMD(string cmd)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = "/C " + cmd;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.Start();
            string res = proc.StandardOutput.ReadToEnd();
            proc.StandardOutput.Close();

            proc.Close();
            return res;
        }
        private void Log(string text)
        {
            txtLog.Text += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " : " + text + "\r\n";

        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            isWorking = false;
            isTakingScreenshots = false;
            btnStartServer.Enabled = true;
            btnStopServer.Enabled = false;
            Log("Server Stoped.");

        }

        private void cbPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrivate.Checked == true)
            {
                txtUser.Enabled = true;
                txtPassword.Enabled = true;
                isPrivateTask = true;
            }
            else
            {
                txtUser.Enabled = false;
                txtPassword.Enabled = false;
                isPrivateTask = false;
            }
        }

        private void cbPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPreview.Checked == true)
            {
                isPreview = true;
            }
            else
            {
                isPreview = false;
                imgPreview.Image = imgPreview.InitialImage;
            }
        }

        private void cbCaptureMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCaptureMouse.Checked)
            {
                isMouseCapture = true;
            }
            else
            {
                isMouseCapture = false;
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LanguageChoosing.DataSource = new System.Globalization.CultureInfo[]
            {
                System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                System.Globalization.CultureInfo.GetCultureInfo("en-US")
            };
            LanguageChoosing.DisplayMember = "NativeName";
            LanguageChoosing.ValueMember = "Name";

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                LanguageChoosing.SelectedValue = Properties.Settings.Default.Language;
            }
            _ips = GetAllIPv4Addresses();
            foreach (var ip in _ips)
            {
                comboIPs.Items.Add(ip.Item2 + " - " + ip.Item1);
            }
            comboIPs.SelectedIndex = comboIPs.Items.Count - 1;
        }

        private void imgPreview_Click(object sender, EventArgs e)
        {
            if (imgPreview.Dock == DockStyle.None)
            {
                imgPreview.Dock = DockStyle.Fill;
            }
            else
            {
                imgPreview.Dock = DockStyle.None;
            }
        }

        private void cbScreenshotEvery_CheckedChanged(object sender, EventArgs e)
        {
            if (cbScreenshotEvery.Checked)
            {
                isTakingScreenshots = true;
            }
            else
            {
                isTakingScreenshots = false;
            }
        }

        private void lblWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://eslamx.com");
        }

        private void lblMe_Click(object sender, EventArgs e)
        {
            Process.Start("http://facebook.com/EslaMx7");
            Process.Start("http://twitter.com/EslaMx7");
        }

        private void lblGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/EslaMx7/ScreenTask");
        }


        private void isStoppingTranslation_CheckedChanged(object sender, EventArgs e)
        {
            if (isStoppingTranslation.Checked)
            {
                isTakingScreenshots = false;
                isPreview = false;

                //<-- of external source -->
                if (ImageChoosing.Text == "Of external source image" || ImageChoosing.Text == "изображение из внешнего источника")
                {
                    try
                    {
                        ImageWay.Visible = true;
                        Rooting.Visible = false;
                        ImageWay.Text = "";
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Load(ImageWay.Text);
                        imgPreview.Image = pictureBox.Image;
                        pictureBox.Image.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                        pictureBox.Image.Save(Application.StartupPath + "/OtherWebserver" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show(Resource1.MessageNullTextRoot);
                        isTakingScreenshots = true;
                        isPreview = true;
                        isStoppingTranslation.Checked = false;
                    }
                }


                //< --of local source -->
                else if (ImageChoosing.Text == "Local image" || ImageChoosing.Text == "Изображение из локального источника")
                {
                    try
                    {
                        ImageWay.Visible = true;
                        Rooting.Visible = true;
                        ImageWay.Text = "";
                        Bitmap bitmap = new Bitmap(ImageWay.Text);
                        imgPreview.Image = bitmap;
                        bitmap.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                        bitmap.Save(Application.StartupPath + "/OtherWebserver" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show(Resource1.MessageNullTextRoot);
                        isTakingScreenshots = true;
                        isPreview = true;
                        isStoppingTranslation.Checked = false;
                    }
                }

                //<-- Dinamic image of Text -->
                else if (ImageChoosing.Text == "Dynamic image" || ImageChoosing.Text == "Динамическое изображение")
                {

                    ImageWay.Visible = false;
                    Rooting.Visible = false;
                    Rectangle bounds = Screen.GetBounds(Point.Empty);
                    using (Bitmap bmp = new Bitmap(bounds.Width, bounds.Height))
                    {
                        Random rnd = new Random();
                        int a = 0, r = 0, g = 0, b = 0;

                        for (int y = 0; y < bounds.Height; y++)
                        {
                            for (int x = 0; x < bounds.Width; x++)
                            {
                                a = rnd.Next(64);
                                r = rnd.Next(64);
                                g = rnd.Next(a);
                                b = rnd.Next(r);

                                bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                            }
                        }

                        CreateGridImage(a, r, g, b, 30);
                    }
                }
            }
            else
            {
                isTakingScreenshots = true;
                isPreview = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Tree.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Tree.Visible = true;
                this.Hide();
            }
        }


        private void stopTranslationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stopTranslationToolStripMenuItem.Checked)
            {
                isStoppingTranslation.Checked = true;
            }
            else
            {
                isStoppingTranslation.Checked = false;
            }
        }

        public static Bitmap CreateGridImage(
            int maxXCells,
            int maxYCells,
            int cellXPosition,
            int cellYPosition,
            int boxSize)
        {
            using (var bmp = new System.Drawing.Bitmap(maxXCells * boxSize + 1, maxYCells * boxSize + 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Yellow);
                    Pen pen = new Pen(Color.Black);
                    pen.Width = 1;

                    //Draw red rectangle to go behind cross
                    Rectangle rect = new Rectangle(boxSize * (cellXPosition - 1), boxSize * (cellYPosition - 1), boxSize, boxSize);
                    g.FillRectangle(new SolidBrush(Color.Red), rect);

                    //Draw cross
                    g.DrawLine(pen, boxSize * (cellXPosition - 1), boxSize * (cellYPosition - 1), boxSize * cellXPosition, boxSize * cellYPosition);
                    g.DrawLine(pen, boxSize * (cellXPosition - 1), boxSize * cellYPosition, boxSize * cellXPosition, boxSize * (cellYPosition - 1));

                    //Draw horizontal lines
                    for (int i = 0; i <= maxXCells; i++)
                    {
                        g.DrawLine(pen, (i * boxSize), 0, i * boxSize, boxSize * maxYCells);
                    }

                    //Draw vertical lines            
                    for (int i = 0; i <= maxYCells; i++)
                    {
                        g.DrawLine(pen, 0, (i * boxSize), boxSize * maxXCells, i * boxSize);
                    }
                }

                var memStream = new MemoryStream();
                bmp.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                bmp.Save(Application.StartupPath + "/OtherWebserver" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                return bmp;
            }
        }

        private void Rooting_Click(object sender, EventArgs e)
        {
            var OFD = new System.Windows.Forms.OpenFileDialog();
            OFD.ShowDialog();
            ImageWay.Text = OFD.FileName;
        }

        private void ImageChoosing_SelectedIndexChanged(object sender, EventArgs e)
        {
            //<-- of external source -->
            if (ImageChoosing.Text == "Of external source image" || ImageChoosing.Text == "Изображение из внешнего источника")
            {
                ImageWay.Visible = true;
                Rooting.Visible = false;
            }

            //< --of local source -->
            else if (ImageChoosing.Text == "Local image" || ImageChoosing.Text == "Изображение из локального источника")
            {
                ImageWay.Visible = true;
                Rooting.Visible = true;
            }

            else
            {
                ImageWay.Visible = false;
                Rooting.Visible = false;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Language = LanguageChoosing.SelectedValue.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
