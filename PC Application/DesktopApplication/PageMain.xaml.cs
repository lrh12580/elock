using System;
using System.Collections;
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
using System.Windows.Forms;
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
using System.Reflection;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public static ObservableCollection<Authority> thelist;

        IEnumerable<Authority> SelectedFilelist;
       

        public PageMain()
        {
            InitializeComponent();
            
            FindTreeViewItem(allfilesItem).Focus();
            
            thelist = new ObservableCollection<Authority> {};
            this.mylistview.ItemsSource = thelist;

            if (MainWindow.isSigined)
            {
                //ObservableCollection<Authority> mainlist = getFileName(Post.GetAuthorityList());

                thelist.Clear();
                ObservableCollection<Authority> mainlist = Post.GetAuthorityList();
                foreach (Authority item in mainlist)
                {
                    item.File_Name = item.File_Path.Split('/').Last();
                    item.File_Path = item.File_Path.Replace("/", "\\");
                    thelist.Add(item);
                }
            }

            countText.Text = "共 " + thelist.Count() + " 项";
        }

        public ObservableCollection<Authority> getFileName(ObservableCollection<Authority> list)
        {
            ObservableCollection<Authority> filenames = new ObservableCollection<Authority>();
            foreach (Authority a in list)
            {
                string[] filename = a.File_Path.Split('/');
                Authority one = new Authority()
                {
                    File_Path = filename.Last(),
                    Authority_Number = a.Authority_Number
                };
                filenames.Add(one);
            }
            return filenames;
        }

        //选择文件夹
        private void button_runScript_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string m_Dir = m_Dialog.SelectedPath.Trim();
            //this.textblock_filepath.Text = m_Dir;

            if (MainWindow.isSigined)
                Post.SendAuthority(m_Dir.Replace("\\", "/"), "1");
            else
                System.Windows.MessageBox.Show("您还未登录！", "e-lock", MessageBoxButton.OK);
            thelist.Clear();
            if (MainWindow.isSigined)
            {
                ObservableCollection<Authority> mainlist = Post.GetAuthorityList();
                foreach (Authority item in mainlist)
                {
                    item.File_Name = item.File_Path.Split('/').Last();
                    item.File_Path = item.File_Path.Replace("/", "\\");
                    thelist.Add(item);
                }
            }
            countText.Text = "共 " + thelist.Count() + " 项";
        }

        //选择文件
        private void button_chose_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                //Filter = "Excel Files (*.sql)|*.sql|图片文件(*.jpg,*.png)|*.jpg;*.png"  
                Filter = "All Files|*.*"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                //this.textblock_filepath.Text = openFileDialog.FileName;
                //thelist.Add(new Authority { Name = openFileDialog.SafeFileName, Address = openFileDialog.FileName });
                //thelist[0].Address = "bbbbbbbb";
                //this.mylistview.Items.Add(new FileClass { Name = openFileDialog.SafeFileName, Address = openFileDialog.FileName });

                if(MainWindow.isSigined)
                    Post.SendAuthority(openFileDialog.FileName.Replace("\\","/"), "1");
                else
                    System.Windows.MessageBox.Show("您还未登录！", "e-lock", MessageBoxButton.OK);

                thelist.Clear();
                if (MainWindow.isSigined)
                {
                    ObservableCollection<Authority> mainlist = Post.GetAuthorityList();
                    foreach (Authority item in mainlist)
                    {
                        item.File_Name = item.File_Path.Split('/').Last();
                        item.File_Path = item.File_Path.Replace("/", "\\");
                        thelist.Add(item);
                    }
                }
            }
            countText.Text = "共 " + thelist.Count() + " 项";
        }

        private void btnMenu_Initialized(object sender, EventArgs e)
        {
            //设置右键菜单为null
            this.AddButton.ContextMenu = null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //目标
            this.contextMenu.PlacementTarget = this.AddButton;
            //位置
            //this.contextMenu.Placement = PlacementMode.Top;
            //显示菜单
            this.contextMenu.IsOpen = true;

            //this.textblock_filepath.Text = MyKeys.FILE_PATH;
        }
  
        private void treeview_Selected(object sender, RoutedEventArgs e)
        {
            
            TreeViewItem a = e.OriginalSource as TreeViewItem;

            switch(a.Name.ToString())
            {
                case "allfilesItem":
                    SelectedFilelist = thelist;
                    this.mylistview.ItemsSource = SelectedFilelist;
                    if(thelist!=null)
                        countText.Text = "共 " + thelist.Count() + " 项";
                    break;

                case "文档":
                    SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Split('.').Last() == "doc" || 
                              authority.File_Path.Split('.').Last() == "docx" || 
                              authority.File_Path.Split('.').Last() == "txt" || 
                              authority.File_Path.Split('.').Last() == "pdf" ||
                              authority.File_Path.Split('.').Last() == "ppt" ||
                              authority.File_Path.Split('.').Last() == "pptx"

                        select authority;
                  
                    this.mylistview.ItemsSource = SelectedFilelist;
                    countText.Text = "共 " + SelectedFilelist.Count() + " 项";
                    break;

                case "图片":
                    SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Split('.').Last() == "jpg" ||
                              authority.File_Path.Split('.').Last() == "png" ||
                              authority.File_Path.Split('.').Last() == "gif" 
                        select authority;
                    this.mylistview.ItemsSource = SelectedFilelist;
                    countText.Text = "共 " + SelectedFilelist.Count() + " 项";
                    break;

                case "视频":
                    SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Split('.').Last() == "mp4" ||
                              authority.File_Path.Split('.').Last() == "mkv" ||
                              authority.File_Path.Split('.').Last() == "avi" ||
                              authority.File_Path.Split('.').Last() == "swf" ||
                              authority.File_Path.Split('.').Last() == "dat" 
                        select authority;
                    this.mylistview.ItemsSource = SelectedFilelist;
                    countText.Text = "共 " + SelectedFilelist.Count() + " 项";
                    break;

                case "音频":
                    SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Split('.').Last() == "mp3" ||
                              authority.File_Path.Split('.').Last() == "m4a" ||
                              authority.File_Path.Split('.').Last() == "ape" ||
                              authority.File_Path.Split('.').Last() == "flac" ||
                              authority.File_Path.Split('.').Last() == "wav" 
                        select authority;
                  
                    this.mylistview.ItemsSource = SelectedFilelist;
                    countText.Text = "共 " + SelectedFilelist.Count() + " 项";
                    break;

                case "应用":
                    SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Split('.').Last() == "exe"
                        select authority;
                   
                    this.mylistview.ItemsSource = SelectedFilelist;
                    countText.Text = "共 " + SelectedFilelist.Count() + " 项";
                    break;

                case "其它":

                    SelectedFilelist =
                        from authority in thelist
                        where   authority.File_Path.Split('.').Last() != "exe" &&
                                authority.File_Path.Split('.').Last() != "wav" &&
                                authority.File_Path.Split('.').Last() != "flac" &&
                                authority.File_Path.Split('.').Last() != "ape" &&
                                authority.File_Path.Split('.').Last() != "m4a" &&
                                authority.File_Path.Split('.').Last() != "mp3" &&
                                authority.File_Path.Split('.').Last() != "dat" &&
                                authority.File_Path.Split('.').Last() != "swf" &&
                                authority.File_Path.Split('.').Last() != "avi" &&
                                authority.File_Path.Split('.').Last() != "mkv" &&
                                authority.File_Path.Split('.').Last() != "mp4" &&
                                authority.File_Path.Split('.').Last() != "gif" &&
                                authority.File_Path.Split('.').Last() != "png" &&
                                authority.File_Path.Split('.').Last() != "jpg" &&
                                authority.File_Path.Split('.').Last() != "pptx" &&
                                authority.File_Path.Split('.').Last() != "ppt" &&
                                authority.File_Path.Split('.').Last() != "pdf" &&
                                authority.File_Path.Split('.').Last() != "txt" &&
                                authority.File_Path.Split('.').Last() != "docx" &&
                                authority.File_Path.Split('.').Last() != "doc"
                        select authority;

                    this.mylistview.ItemsSource = SelectedFilelist;
                    countText.Text = "共 " + SelectedFilelist.Count() + " 项";
                    break;
            }
        }     

        //用于设置父节点自动展开选中
        public static TreeViewItem FindTreeViewItem(ItemsControl container)
        {
            if (null == container)
            {
                return null;
            }

            if (container.Name.Equals("allfilesItem"))
            {
                return container as TreeViewItem;
            }


            int count = container.Items.Count;
            for (int i = 0; i < count; i++)
            {
                TreeViewItem subContainer = (TreeViewItem)container.ItemContainerGenerator.ContainerFromIndex(i);

                if (null == subContainer)
                {
                    continue;
                }

                // Search the next level for the object.
                TreeViewItem resultContainer = FindTreeViewItem(subContainer);
                if (null != resultContainer)
                {
                    return resultContainer;
                }
            }

            return null;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Contains(textbox.Text)
                        select authority;
            this.mylistview.ItemsSource = SelectedFilelist;
 
        }

        private void textbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Contains(textbox.Text)
                        select authority;
            this.mylistview.ItemsSource = SelectedFilelist;
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectedFilelist =
                        from authority in thelist
                        where authority.File_Path.Contains(textbox.Text)
                        select authority;
            this.mylistview.ItemsSource = SelectedFilelist;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Authority emp = mylistview.SelectedItem as Authority;
            if (emp != null)
            {
                Dispatcher.Invoke(new Action(() =>  //BeginInvoke是异步操作，Invoke是同步操作。
                {
                    if (CMessageBox.Show("确认删除吗？", "e-Lock消息", CMessageBoxButton.YesNO, CMessageBoxImage.None, CMessageBoxDefaultButton.Yes) == CMessageBoxResult.Yes)
                    {
                        if(MainWindow.isSigined)
                        {
                              //网络请求
                            string filepath = emp.File_Path;
                            if (Post.SendCheck(filepath.Replace("\\", "/"), "0") == true)
                            {
                                string filelist = File.ReadAllText("C:\\Windows\\System32\\drivers\\elocklist.lock");
                                filelist = filelist.Replace("\r", "");
                                string[] array = filelist.Split('\n');
                                string result = "";
                                for (int i = 0; i < array.Length; i++)
                                {
                                    if (array[i].Equals(""))
                                        continue;
                                    else
                                    {
                                        if (array[i].Equals(filepath))
                                            continue;
                                        else
                                            result += array[i] + "\r\n";
                                    }
                                }
                                File.WriteAllText("C:\\Windows\\System32\\drivers\\elocklist.lock", result);
                                CFunction.UpdateFiles();

                                thelist.Clear();
                                ObservableCollection<Authority> mainlist = Post.GetAuthorityList();
                                foreach (Authority item in mainlist)
                                {
                                    item.File_Name = item.File_Path.Split('/').Last();
                                    item.File_Path = item.File_Path.Replace("/", "\\");
                                    thelist.Add(item);
                                }
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("您还未登录！", "e-lock", MessageBoxButton.OK);
                        }                                          
                    }
                }));
            }
           
            //DeleteGrid.Visibility = Visibility.Visible;
        }

    }
}
