using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    //private static TestDBContext tc = new TestDBContext();
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {         
            Process[] p = Process.GetProcessesByName("elock");
            if(p.Length > 1)
            {
                if(System.Windows.MessageBox.Show("e-lock已在后台运行", "e-lock", MessageBoxButton.OK) == MessageBoxResult.OK)
                    p[0].Kill();
                else
                    p[0].Kill();
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "firstlogin.txt")) //登陆过
            {
                Simplefilter();
            }
            else
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "firstlogin.txt", "0");
                WirteToRegister();
                Simplefilter();
                DesktopApplication.Properties.Settings.Default.firstLogin = true;
            }
                        
            if (e.Args.Length > 0)
            {
                MainWindow wnd = new MainWindow(e.Args[0]);
                wnd.Show();
            }
            else
            {
                MainWindow wnd = new MainWindow();
                wnd.Show();
            }
        }

        public static string Simplefilter()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "simplefilter.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            //p.StandardInput.WriteLine(str + "&exit");

            //p.StandardInput.AutoFlush = true;
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();

            return output;
        }

        public static void WirteToRegister()
        {
            RegistryKey rkClassRoot = Registry.ClassesRoot;
            RegistryKey shell = rkClassRoot.OpenSubKey("AllFilesystemObjects\\shell", true);
            shell.CreateSubKey("elock");
            RegistryKey test = shell.OpenSubKey("elock", true);
            test.CreateSubKey("command");
            RegistryKey command = test.OpenSubKey("command", true);
            command.SetValue("", AppDomain.CurrentDomain.BaseDirectory + "elock.exe %1");
        }

        public static void WirteToRegister2()
        {
            RegistryKey rkClassRoot = Registry.LocalMachine;
            RegistryKey shell = rkClassRoot.OpenSubKey("SOFTWARE", true);
            shell.CreateSubKey("elock");
            RegistryKey path = shell.OpenSubKey("elock", true);
            path.SetValue("path", MyKeys.MYDIRECTORY);//不要忘記改應用名字
        }
    }

    //public static TestDBContext TestDBContext
    //{
    //    get {return TestDBContext;}
    //}
}
