using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication
{
    public class Mypermission
    {
        private StringBuilder path;
        private bool isPermit;
        private DateTime date;

        public Mypermission(StringBuilder path, bool isPermit, DateTime date)
        {
            this.path = path;
            this.isPermit = isPermit;
            this.date = date;
        }

        public StringBuilder getPath()
        {
            return path;
        }
        public bool getIsPermit()
        {
            return isPermit;
        }
        public DateTime getDate()
        {
            return date;
        }
        public void setPath(StringBuilder path)
        {
            this.path = path;
        }
        public void setIsPermit(bool isPermit)
        {
            this.isPermit = isPermit;
        }
        public void setDate(DateTime date)
        {
            this.date = date;
        }
    }
}
