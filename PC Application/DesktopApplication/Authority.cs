using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;//INotifyPropertyChanged

namespace DesktopApplication
{
    public class Authority : INotifyPropertyChanged
    {
        private string file_path; //
        private string file_name;
        private string authority_number;//权限
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Authority()
        {
            
        }

        //public Authority(String file_path, String authority_number)
        //{
        //    this.file_path = file_path;
        //    this.authority_number = authority_number;
        //}

        public string File_Path
        {
            get
            {
                return file_path;
            }
            set
            {
                file_path = value;
            }
        }

        public string File_Name
        {
            get
            {
                return file_name;
            }
            set
            {
                //file_name = getFileName(File_Path);
                file_name = value;
            }
        }

        public string Authority_Number
        {
            get
            {
                return authority_number;
            }
            set
            {
                authority_number = value;
                //if (this.PropertyChanged != null)//激发事件，参数为Age属性    
                //{
                //    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                //}  
            }
        }

        public string getFileName(string File_Path)
        {
            string filename = File_Path.Split('/').Last(); 
            return filename;
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }
}
