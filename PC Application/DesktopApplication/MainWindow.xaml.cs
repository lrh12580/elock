using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;//ObservableCollection


using System.Web;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.Data;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using System.Management;
using System.Collections;
using System.Reflection;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Configuration;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Mypermission> permissions = new List<Mypermission>();
        WindowState wsl;
        NotifyIcon notifyIcon;
        Thread thread;
        
        public MainWindow()
        {
            MyKeys.MYDIRECTORY = AppDomain.CurrentDomain.BaseDirectory;
            InitializeComponent();
            init();
            icon();
            wsl = WindowState;
            HomeButton.Focus();
            requestMethod();
        }

        public MainWindow(string arg)
        {
            MyKeys.MYDIRECTORY = AppDomain.CurrentDomain.BaseDirectory;
            InitializeComponent();
            init();
            icon();
            wsl = WindowState;
            HomeButton.Focus();
            requestMethod();

            MyKeys.FILE_PATH = arg;

                if (System.Windows.MessageBox.Show("确定要添加该项管理吗？",
      "e-lock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (!ConfigAppSettings.GetValue("Account").Equals("") && !ConfigAppSettings.GetValue("Password").Equals(""))
                    {
                        Post.HttpLogin(ConfigAppSettings.GetValue("Account"), ConfigAppSettings.GetValue("Password"));
                        SiginButton.Content = ConfigAppSettings.GetValue("Account");
                        isSigined = true;

                        if (MainWindow.isSigined)
                        {
                            if (!MyKeys.FILE_PATH.Equals(""))
                                if(Post.SendAuthority(MyKeys.FILE_PATH.Replace("\\", "/"), "1"))
                                {
                                    //ObservableCollection<Authority> mainlist = getFileName(Post.GetAuthorityList());
                                    ObservableCollection<Authority> mainlist = Post.GetAuthorityList();
                                    foreach (Authority item in mainlist)
                                    {
                                        //item.File_Path.Replace("/", "\\");
                                        item.File_Path = item.File_Path.Replace("/", "\\");
                                        PageMain.thelist.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("您还未登录！",
                            "e-lock", MessageBoxButton.OK);
                        }  
                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
           
        }

        public static void init()
        {
            MyKeys.GUID = GetGUID();
            string[] key = MyEncrypt.GenerateKey();
            MyKeys.CLIENT_PUBLIC_KEY = key[0];
            MyKeys.CLIENT_PRIVATE_KEY = key[1];
            MyKeys.SERVER_PUBLIC_KEY = getServerKey();
        }
        
        public static string getServerKey()
        {
            string publicKey = "";
            if (!File.Exists(MyKeys.MYDIRECTORY + "serverPublicKeyString.txt"))
            {
                var thread = new Thread(() =>
                {
                    var request = (HttpWebRequest)WebRequest.Create(MyKeys.SENDURL);

                    var postData = "model=" + PostOptions.GETSERVERKEY;
                    postData += "&publicKey=" + Post.UrlEncode(MyKeys.CLIENT_PUBLIC_KEY, Encoding.UTF8);
                    postData += "&guid=" + Post.UrlEncode(MyKeys.GUID, Encoding.UTF8);
                    var data = Encoding.UTF8.GetBytes(postData);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var josnObj = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                    string status = josnObj["status"].ToString();
                    if (status.Equals("OK"))
                    {
                        publicKey = josnObj["publicKey"].ToString();
                        File.WriteAllText(MyKeys.MYDIRECTORY + "serverPublicKeyString.txt", publicKey, Encoding.UTF8);
                        Console.WriteLine("GET");
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                while (!File.Exists(MyKeys.MYDIRECTORY + "serverPublicKeyString.txt")) ;
                return publicKey;
            }
            else
            {
                publicKey = File.ReadAllText(MyKeys.MYDIRECTORY + "serverPublicKeyString.txt");
                return publicKey;
            }
        }
        public static string GetGUID()
        {
            //try
            //{
            //    string mac = "";
            //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //    ManagementObjectCollection moc = mc.GetInstances();
            //    foreach (ManagementObject mo in moc)
            //    {
            //        if ((bool)mo["IPEnabled"] == true)
            //        {
            //            mac = mo["MacAddress"].ToString();
            //            break;
            //        }
            //    }
            //    moc = null;
            //    mc = null;
            //    return mac;
            //}
            //catch
            //{
            //    return "";
            //}
            string guid;
            if (!File.Exists(MyKeys.MYDIRECTORY + "Guid.txt"))
            {
                guid = System.Guid.NewGuid().ToString();
                File.WriteAllText(MyKeys.MYDIRECTORY + "Guid.txt", guid, Encoding.UTF8);
            }
            else
            {
                guid = File.ReadAllText(MyKeys.MYDIRECTORY + "Guid.txt");
            }
            return guid;
        }

        public static bool isSigined = false;

        //PageMain pm = new PageMain();

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.myFrame.Source = new Uri("/PageHistory.xaml", UriKind.Relative);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            this.myFrame.Source = new Uri("/PageMain.xaml", UriKind.Relative);           
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            //目标
            this.contextMenu.PlacementTarget = this.SettingButton;
            //位置
            //this.contextMenu.Placement = PlacementMode.Top;
            //显示菜单
            this.contextMenu.IsOpen = true;
        }

        private void btnMenu_Initialized(object sender, EventArgs e)
        {
             //设置右键菜单为null
             this.SettingButton.ContextMenu = null;
        }

        private void SiginSigoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isSigined)
            {
                LoginWindow win = new LoginWindow();
                win.PassDataBetweenForm += new LoginWindow.PassDataBetweenFormHandler(Child_PassDataBetweenForm);
                win.ShowDialog();
                win.Activate();
            } else if (isSigined)
            {
                //注销代码
            }
            
        }

        //回调
        private void Child_PassDataBetweenForm(object sender, PassDataWinFormEventArgs e)
        {
            SiginButton.Content = e.Account;

            ConfigAppSettings.SetValue("Account", e.Account);
            ConfigAppSettings.SetValue("Password", e.Key);
            //ObservableCollection<Authority> authorities = Post.GetAuthorityList();
            //PageMain.thelist = getFileName(Post.GetAuthorityList());
            
            isSigined = true;

            if (MainWindow.isSigined)
            {
                //ObservableCollection<Authority> mainlist = getFileName(Post.GetAuthorityList());
                ObservableCollection<Authority> mainlist = Post.GetAuthorityList();
                foreach (Authority item in mainlist)
                {
                    item.File_Name = item.File_Path.Split('/').Last();
                    //item.File_Path.Replace("/", "\\");
                    item.File_Path = item.File_Path.Replace("/", "\\");
                    PageMain.thelist.Add(item);       
                }

                Console.WriteLine(DesktopApplication.Properties.Settings.Default.firstLogin);
            }

            //PageHistory.refreshchart();

            //Console.WriteLine("thelist.COUNT" + PageMain.thelist.Count + "");
        }
        
        //public ObservableCollection<Authority> getFileName(ObservableCollection<Authority> list)
        //{
        //    ObservableCollection<Authority> filenames = new ObservableCollection<Authority>();
        //    foreach (Authority a in list)
        //    {
        //        string[] filename = a.File_Path.Split('/');
        //        Authority one = new Authority()
        //        {
        //            File_Path = filename.Last(),
        //            Authority_Number = a.Authority_Number
        //        };
        //        filenames.Add(one);
        //    }
        //    return filenames;
        //}

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            thread.Abort();
            Environment.Exit(0);
        }
        
       
         /// <summary>
        /// 窗口移动事件
        /// </summary>
        private void TitleBar_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        int i = 0;
        /// <summary>
        /// 标题栏双击事件
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            i += 1;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };
            timer.IsEnabled = true;

            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                this.WindowState = this.WindowState == WindowState.Maximized ?
                              WindowState.Normal : WindowState.Maximized;
            }
        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; //设置窗口最小化
        }

        /// <summary>
        /// 窗口最大化与还原
        /// </summary>
        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal; //设置窗口还原
            }
            else
            {
                this.WindowState = WindowState.Maximized; //设置窗口最大化
            }
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void requestMethod()
        {
            thread = new Thread(() =>
            {
                ulong temp = 0;
                StringBuilder path = new StringBuilder(256);
                while (true)
                {
                    bool isGet = CFunction.SimpleGetMessage(ref temp, path, 256);
                    if (isGet == true && temp != 0)
                    {
                        StringBuilder devicepath = ChangePath.ChangePaths(path).Replace("\\", "/");
                        Dictionary<int, bool> dict = new Dictionary<int, bool>();
                        dict = isContain(devicepath);
                        if (dict[1])
                        {
                            if (dict[2])
                                CFunction.SimpleReplyMessage(temp, true);
                            else
                                CFunction.SimpleReplyMessage(temp, false);
                        }
                        else
                        {
                            //Window1 window = new Window1(devicepath, temp);
                            //window.Show();

                            //if (System.Windows.MessageBox.Show("请通过手机验证", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            if (createMessageBox(path))
                            {
                                if (MainWindow.isSigined)
                                {
                                    if (Post.SendCheck(devicepath.ToString(), "1"))
                                    {
                                        DateTime date = DateTime.Now;
                                        CFunction.SimpleReplyMessage(temp, true);
                                        permissions.Add(new Mypermission(devicepath, true, date));
                                    }
                                    else
                                    {
                                        DateTime date = DateTime.Now;
                                        CFunction.SimpleReplyMessage(temp, false);
                                        permissions.Add(new Mypermission(devicepath, false, date));
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("您还未登录！", "e-lock", MessageBoxButton.OK);
                                }                                
                            }

                            else
                            {
                                DateTime date = DateTime.Now;
                                CFunction.SimpleReplyMessage(temp, false);
                                permissions.Add(new Mypermission(devicepath, false, date));
                            }
                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    }
                }

            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private Dictionary<int, bool> isContain(StringBuilder permission)
        {
            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            for (int i = 0; i < permissions.Count; i++)
            {
                bool hasPath = permission.Equals(permissions[i].getPath());
                DateTime now = DateTime.Now;
                double ms = now.Subtract(permissions[i].getDate()).TotalMilliseconds;
                if (hasPath == true)
                {
                    if (ms > 10 * 1000)
                    {
                        permissions.RemoveAt(i);
                    }
                    else
                    {
                        dict.Add(1, true);
                        dict.Add(2, permissions[i].getIsPermit());
                        return dict;
                    }

                }
            }
            dict.Add(1, false);
            dict.Add(2, false);
            return dict;
        }

        private void icon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "e-lock"; //设置程序启动时显示的文本
            this.notifyIcon.Text = "e-lock";//最小化到托盘时，鼠标点击时显示的文本
            this.notifyIcon.Icon = new System.Drawing.Icon(MyKeys.MYDIRECTORY+"elock.ico");//程序图标
            this.notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;
            this.notifyIcon.ShowBalloonTip(1000);
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = wsl;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                e.Cancel = true;
                this.WindowState = WindowState.Minimized;
                this.Hide();          
        }

        //private void BigButton_Click1(object sender, RoutedEventArgs e)
        //{
        //    bool result = false;
        //   // var thread = new Thread(() =>
        //   //{
        //       Dispatcher.Invoke(new Action(() =>
        //       {
        //           if (CMessageBox.Show("请通过手机验证", "eeeee", CMessageBoxButton.YesNO, CMessageBoxImage.None, CMessageBoxDefaultButton.Yes) == CMessageBoxResult.Yes)
        //               result = true;
        //       }));
        //   //});
        //   // thread.Start();

        //       Console.WriteLine(result);
        //}

        private bool createMessageBox(StringBuilder path)
        {
            bool result = false;

            Dispatcher.Invoke(new Action(() =>  //BeginInvoke是异步操作，Invoke是同步操作。
            {
                if(CMessageBox.Show("请通过手机验证\n"+ChangePath.ChangePaths(path), "e-Lock消息", CMessageBoxButton.YesNO, CMessageBoxImage.None, CMessageBoxDefaultButton.Yes) == CMessageBoxResult.Yes)
                {
                    result =  true;
                }
                    
            }));

            return result;
        }
    }
}
