using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication
{
    class ChangePath
    {
        public static Dictionary<string, string> MatchDevices(List<string> deviceIDs)
        {
            Dictionary<string, string> match = new Dictionary<string, string>();
            for (int i = 0; i < deviceIDs.Count; i++)
            {
                string disk = GetRealPath(deviceIDs[i]);
                match.Add(disk, deviceIDs[i]);
            }
            return match;
        }

        public static List<string> GetRemovableDeviceID()
        {
            List<string> deviceIDs = new List<string>();
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT  *  From  Win32_LogicalDisk ");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {

                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case (int)DriveType.Removable:   //可以移动磁盘       
                        deviceIDs.Add(mo["DeviceID"].ToString());
                        break;
                    case (int)DriveType.Fixed:   //本地磁盘         
                        deviceIDs.Add(mo["DeviceID"].ToString());
                        break;
                    case (int)DriveType.CDRom:   //CD   rom   drives       
                        break;
                    case (int)DriveType.Network:   //网络驱动      
                        break;
                    case (int)DriveType.Ram:
                        break;
                    case (int)DriveType.NoRootDirectory:
                        break;
                    default:
                        break;
                }

            }
            return deviceIDs;
        }

        public static string GetRealPath(string path)
        {

            string realPath = path;
            StringBuilder pathInformation = new StringBuilder(250);
            string driveLetter = System.IO.Path.GetPathRoot(realPath).Replace("\\", "");
            CFunction.QueryDosDevice(driveLetter, pathInformation, 250);

            // If drive is substed, the result will be in the format of "\??\C:\RealPath\".

            // Strip the \??\ prefix.
            string realRoot = pathInformation.ToString();

            //Combine the paths.
            realPath = System.IO.Path.Combine(realRoot, realPath.Replace(System.IO.Path.GetPathRoot(realPath), ""));


            return realPath;
        }

        public static StringBuilder ChangePaths(StringBuilder path)
        {
            string str = "";
            string[] parts = path.ToString().Split('\\');

            string devicePath = "\\" + parts[1] + "\\" + parts[2];
            str = MatchDevices(GetRemovableDeviceID())[devicePath];
            for (int i = 3; i < parts.Length; i++)
            {
                str += "\\";
                str += parts[i];
            }
            return new StringBuilder(str);
        }
    }
}
