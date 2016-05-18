using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using IAMEntityDAL.EHRDAL;
using System.Data.OleDb;
using System.Web.Services.Protocols;
using BaseDataAccess;


namespace IAM
{
    public partial class DEMo : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // WebReferenceTCNew.Shactcservice _autoServices = new WebReferenceTCNew.Shactcservice();
               // _autoServices.Timeout = 1000 * 60 * 1;
               // _autoServices.Url = "http://10.124.88.149:8989/axis2/services/Shactcservice.ShactcserviceHttpSoap11Endpoint/";
               // //var xxf = _autoServices.maina();
               // //gridview1.DataSource = myData.getData(xxf);
               // //gridview1.DataBind();
               // int ok = 0;

               // var xxxf = string.Join("", _autoServices.showinfo()[0].groups);
               // Response.Write(xxxf);
               //// Response.Write(string.Join("", xxf[0].groups));
               // int allcount, okcount;
                //new BLL.TcSyncServices().SyncUserInfo(out allcount, out okcount);//同步Tc 用户信息接口
                //new BLL.TcSyncServices().SyncRole(out allcount,out okcount);//同步Tc 组 角色信息接口
                //new BLL.ADSyncServices().SyncComputer(out allcount,out okcount);
                //Response.Write(string.Format("成功:{0}<br/>共:{1}<br/>失败:{2}", okcount, allcount, allcount - okcount));

                string stm = "1017%5E0001A1100000000002GA%5EHR_01%5E%E7%B3%BB%E7%BB%9F%E7%AE%A1%E7%90%86%E5%91%98%5E%E5%AE%81%E6%B3%A2%E6%9D%AD%E5%B7%9E%E6%B9%BE%E6%B1%87%E4%BC%97%E6%B1%BD%E8%BD%A6%E5%BA%95%E7%9B%98%E7%B3%BB%E7%BB%9F%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8%5E3%3B";
                string rul = Context.Server.UrlDecode(stm);
                Response.Write(rul);
            }
        }
    }

    //public class Tree2
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public List<Tree2> childs { get; set; }
    //}

    public class myData
    {
        public int id { get; set; }
        /// <summary>
        /// 组（例：cae.pe)
        /// </summary>
        public string group { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        private static string groupname = "group";
        private static string roleName = "role";
        public static List<myData> getData(WebReferenceNewTC. Tree2 tree)
        {
            List<myData> list = new List<myData>();
            if (tree.type == groupname)
            {//是组
                string gtmp = tree.title;
                getData2(tree, ref gtmp, list);
            }
            if (tree.type == roleName)
            {//为角色
                list.Add(new myData() { id = (tree.title).GetHashCode(), Role = tree.title, group = "" });
            }

            return list;
        }

        private static void getData2(WebReferenceNewTC.Tree2 tree, ref string group, List<myData> list)
        {
            foreach (var item in tree.childs)
            {
                if (item.type == roleName)
                {//为角色，最后一级
                    list.Add(new myData() { id = (group + item.title).GetHashCode(), Role = item.title, group = group });
                }
                if (item.type == groupname)
                {//为组，继续查找子组
                    string grouptmp = (item.title + "." + group);
                    getData2(item, ref grouptmp, list);
                }
            }
        }
    }

    
}