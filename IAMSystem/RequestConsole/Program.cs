using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using IAMEntityDAL;
using BaseDataAccess;
using System.Data;
using System.Linq;
using System.Data.Entity;

namespace RequestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i=3;i<=10;i+=2)
            {
              
            }
        }

        static void GetByDepartMentTwoSpace()
        {
            List<AD_Department_WorkGroup> list = new AD_Department_WorkGroupDAL().GetList();
            System.Text.StringBuilder stb = new StringBuilder();
            int flag = 0;
            foreach (var x in list)
            {
                if (x.p2.IndexOf("  ") <= 0)
                {
                    stb.Append(string.Format("'{0}',",x.ID));
                    flag++;
                }
            }
            Console.WriteLine(stb.ToString().TrimEnd(','));
            Console.WriteLine("flag="+flag);
        }
        
    }


}
