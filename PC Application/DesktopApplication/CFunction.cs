using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication
{
    public class CFunction
    {
        [DllImport("DllForSimpleFilter.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        public extern static bool SimpleReplyMessage(ulong messageId, bool isAllowed);
        [DllImport("DllForSimpleFilter.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        public extern static bool UpdateFiles();
        [DllImport("DllForSimpleFilter.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public extern static bool SimpleGetMessage(ref ulong messageId, StringBuilder x, int size);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        public extern static uint QueryDosDevice(string str1, StringBuilder str2, int size);
    }
}
