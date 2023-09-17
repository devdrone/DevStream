using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace DataScraper
{
    public static class DBUtil
    {
        public static bool Insert(DataScrapContext context, object data)
        {
            try
            {

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
