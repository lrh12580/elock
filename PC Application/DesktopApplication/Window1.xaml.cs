using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopApplication
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private ulong temp;
        private StringBuilder path;
        private DateTime date;
        public Window1(StringBuilder path, ulong temp)
        {
            this.path = path;
            this.temp = temp;
            date = DateTime.Now;
            InitializeComponent();
            text1.Text = path.ToString();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (Post.SendCheck(path.ToString(), "1"))
            //{
            //    File.WriteAllText("a.txt", "success");
            //    date = DateTime.Now;
            //    CFunction.SimpleReplyMessage(temp, true);
            //    Thread.Sleep(TimeSpan.FromMilliseconds(500));
            //    MainWindow.permissions.Add(new Mypermission(path, true, date));
            //    this.Close();
            //}
            //else
            //{
            //    date = DateTime.Now;
            //    CFunction.SimpleReplyMessage(temp, false);
            //    Thread.Sleep(TimeSpan.FromMilliseconds(500));
            //    MainWindow.permissions.Add(new Mypermission(path, true, date));
            //    this.Close();
            //}           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //CFunction.SimpleReplyMessage(temp, false);

            //isPermit = false;
            //MainWindow.permissions.Add(new Mypermission(path, isPermit, date));
            //Thread.Sleep(TimeSpan.FromMilliseconds(500));
            //this.Close();
        }
    }
}
